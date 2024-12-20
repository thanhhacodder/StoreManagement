using StoreManagement.Helpers;
using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreManagement.Forms
{
    public partial class WarehouseReceipt : Form
    {
        private BindingList<ProductWarehouseReceipt> cart = new BindingList<ProductWarehouseReceipt>();
        DatabaseConnection dbConnection = new DatabaseConnection();
        public WarehouseReceipt()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ProductLoad()
        {
            string query = @"SELECT Products.ProductID, Products.ProductName, Products.Description, Providers.ProviderName, Categories.CategoryName, Products.UnitPrice, Products.StockQuantity 
                             FROM Products INNER JOIN Providers ON Products.ProviderID = Providers.ProviderID 
                             INNER JOIN Categories ON Categories.CategoryID = Products.CategoryID WHERE Products.Deleted = 0";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;

                if (dataGridView1.Columns["WarehouseReceipt"] == null)
                {
                    DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                    editColumn.Name = "WarehouseReceipt";
                    editColumn.HeaderText = "";
                    editColumn.Text = "Nhập kho";
                    editColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(editColumn);
                }

                dataGridView1.Columns["ProductID"].HeaderText = "Mã sản phẩm";
                dataGridView1.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dataGridView1.Columns["Description"].HeaderText = "Mô tả";
                dataGridView1.Columns["CategoryName"].HeaderText = "Danh mục";
                dataGridView1.Columns["ProviderName"].HeaderText = "Nhà cung cấp";
                dataGridView1.Columns["UnitPrice"].HeaderText = "Đơn giá";
                dataGridView1.Columns["StockQuantity"].HeaderText = "Số lượng";

            }

           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["WarehouseReceipt"].Index && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
                string productName = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                int quantity = 1;
                using (CustomInputDialog inputDialog = new CustomInputDialog())
                {
                    if (inputDialog.ShowDialog() == DialogResult.OK)
                    {
                        string userInput = inputDialog.UserInput;

                        if (!string.IsNullOrEmpty(userInput) && int.TryParse(userInput, out int parsedQuantity))
                        {
                            quantity = parsedQuantity;
                        }

                    }
                    else
                    {
                        return;
                    }
                }

                ProductWarehouseReceipt existingItem = cart.FirstOrDefault(item => item.ProductID == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    ProductWarehouseReceipt product = new ProductWarehouseReceipt(productId, productName, quantity);
                    cart.Add(product);
                }

                
                dataGridView3.DataSource = null;
                dataGridView3.DataSource = cart;

                if (dataGridView3.Columns["DeleteColumn"] == null)
                {

                    DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
                    deleteColumn.Name = "DeleteColumn";
                    deleteColumn.HeaderText = "";
                    deleteColumn.Text = "Xoá";
                    deleteColumn.UseColumnTextForButtonValue = true;
                    dataGridView3.Columns.Add(deleteColumn);
                }

                dataGridView3.Columns["DeleteColumn"].DisplayIndex = dataGridView3.Columns.Count - 1;
                UpdateTotalQuantity();
            }
        }

        private void WarehouseDishpatch_Load(object sender, EventArgs e)
        {
            ProductLoad();
        }

        private void UpdateTotalQuantity()
        {
            int quantity = cart.Sum(item => item.Quantity);
            textBox1.Text = quantity.ToString();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView3.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                cart.RemoveAt(e.RowIndex);
                UpdateTotalQuantity();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int warehouseReceiptId = generateId.Generate("WarehouseReceipts");
            string insertWarehouseReceiptQuery = $"INSERT INTO WarehouseReceipts (WarehouseReceiptID, ReceiptDate, TotalQuantity) VALUES ({warehouseReceiptId}, GETDATE(), {cart.Sum(item => item.Quantity)})";
            bool isInsertWarehouseReceiptSuccess = dbConnection.isExecuteSuccess(insertWarehouseReceiptQuery);

            if (isInsertWarehouseReceiptSuccess)
            {

                foreach (var item in cart)
                {
                    int warehouseReceiptItemId = generateId.Generate("WarehouseReceiptItems");
                    string insertReceiptItemQuery = $@"INSERT INTO WarehouseReceiptItems (WarehouseReceiptItemID, WarehouseReceiptID, ProductID, Quantity) 
                                                     VALUES ({warehouseReceiptItemId},{warehouseReceiptId}, {item.ProductID}, {item.Quantity})";
                    bool isInsertWarehouseReceiptItemSuccess = dbConnection.isExecuteSuccess(insertReceiptItemQuery);

                    if(!isInsertWarehouseReceiptItemSuccess)
                    {
                        MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string updateStockQuery = $"UPDATE Products SET StockQuantity = StockQuantity + {item.Quantity} WHERE ProductID = {item.ProductID}";
                    bool isUpdateStockSuccess = dbConnection.isExecuteSuccess(updateStockQuery);

                    if (!isUpdateStockSuccess)
                    {
                        MessageBox.Show($"Không thể cập nhật số lượng tồn kho cho sản phẩm {item.ProductID}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(cart.Count == 0)
            {
                return;
            }


            MessageBox.Show("Nhập hàng thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProductLoad();
            cart.Clear();
        }

        private void search_button_Click(object sender, EventArgs e)
        {

        }
    }
}
