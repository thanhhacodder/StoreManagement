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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace StoreManagement.Forms
{
    public partial class WarehouseDispatch : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        private BindingList<OrderItem> cart = new BindingList<OrderItem>();
        private int orderId;
        public WarehouseDispatch(int? orderId = null)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.orderId = orderId ?? -1;
            LoadOrderItems();
        }

        private void LoadOrderItems()
        {
            string getOrderItemsQuery = $@"SELECT Products.ProductID, Products.ProductName, Products.UnitPrice, OrderItems.Quantity
                                       FROM Products INNER JOIN OrderItems
                                       ON Products.ProductID = OrderItems.ProductID
                                       WHERE OrderItems.OrderId = {orderId}";

            DataTable dataTableOrderItems = dbConnection.getData(getOrderItemsQuery);

            cart.Clear();

            foreach (DataRow row in dataTableOrderItems.Rows)
            {
                int productId = Convert.ToInt32(row["ProductID"]);
                string productName = row["ProductName"].ToString();
                float unitPrice = Convert.ToSingle(row["UnitPrice"]);
                int quantity = Convert.ToInt32(row["Quantity"]);

                OrderItem orderItem = new OrderItem(productId,productName, quantity, unitPrice);
                cart.Add(orderItem);
            }

            dataGridView2.DataSource = cart;
            UpdateTotalQuantity();
            UpdateTotalPrice();
        }

        private void UpdateTotalQuantity()
        {
            float totalQuantity = cart.Sum(item => item.Quantity);
            textBox1.Text = totalQuantity.ToString();
        }

        private void UpdateTotalPrice()
        {
            float totalPrice = cart.Sum(item => item.Quantity * item.UnitPrice);
            textBox3.Text = totalPrice.ToString();
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

                if (dataGridView1.Columns["WarehouseDispatch"] == null)
                {
                    DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                    editColumn.Name = "WarehouseDispatch";
                    editColumn.HeaderText = "";
                    editColumn.Text = "Xuất kho";
                    editColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(editColumn);
                }

            }

            dataGridView1.Columns["ProductID"].HeaderText = "Mã sản phẩm";
            dataGridView1.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            dataGridView1.Columns["Description"].HeaderText = "Mô tả";
            dataGridView1.Columns["CategoryName"].HeaderText = "Danh mục";
            dataGridView1.Columns["ProviderName"].HeaderText = "Nhà cung cấp";
            dataGridView1.Columns["UnitPrice"].HeaderText = "Đơn giá";
            dataGridView1.Columns["StockQuantity"].HeaderText = "Số lượng";
        }

        private void WarehouseReceipt_Load(object sender, EventArgs e)
        {
            ProductLoad();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["WarehouseDispatch"].Index && e.RowIndex >= 0)
            {
                float price = float.Parse(dataGridView1.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString());
                string productName = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
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


                OrderItem existingItem = cart.FirstOrDefault(item => item.ProductID == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    OrderItem orderItem = new OrderItem(productId, productName, quantity, price);
                    cart.Add(orderItem);
                }

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = cart;

                if (dataGridView2.Columns["DeleteColumn"] == null)
                {

                    DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
                    deleteColumn.Name = "DeleteColumn";
                    deleteColumn.HeaderText = "";
                    deleteColumn.Text = "Xoá";
                    deleteColumn.UseColumnTextForButtonValue = true;
                    dataGridView2.Columns.Add(deleteColumn);
                }

                dataGridView2.Columns["DeleteColumn"].DisplayIndex = dataGridView2.Columns.Count - 1;
                UpdateTotalPrice();
                UpdateTotalQuantity();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int warehouseDispatchId = generateId.Generate("WarehouseDispatchs");
            float totalAmount = 0;

            if (textBox3.Text != "")
            {
                totalAmount = float.Parse(textBox3.Text);
                if (totalAmount <= 0) return;
            }
            else
            {
                return;
            }

            string insertDispatchQuery = $@"INSERT INTO WarehouseDispatchs (WarehouseDispatchID, DispatchDate, TotalQuantity, TotalAmount)
                                                VALUES ({warehouseDispatchId}, GETDATE(), {textBox1.Text}, {totalAmount})";
            bool isInsertDispatchQuerySuccess = dbConnection.isExecuteSuccess(insertDispatchQuery);

            if(isInsertDispatchQuerySuccess)
            {
                foreach (var item in cart)
                {
                    int warehouseDispatchItemId = generateId.Generate("WarehouseDispatchItems");

                    string insertDispatchItemQuery = $@"INSERT INTO WarehouseDispatchItems (WarehouseDispatchItemID, WarehouseDispatchID, ProductID, Quantity, UnitPrice)
                                                       VALUES ({warehouseDispatchItemId}, {warehouseDispatchId}, {item.ProductID}, {item.Quantity}, {item.UnitPrice})";
                    bool isInsertDispatchItemQueryySuccess = dbConnection.isExecuteSuccess(insertDispatchItemQuery);
                    if (!isInsertDispatchItemQueryySuccess)
                    {
                        MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string updateStockQuery = $"UPDATE Products SET StockQuantity = StockQuantity - {item.Quantity} WHERE ProductID = {item.ProductID}";
                    bool isUpdateStockSuccess = dbConnection.isExecuteSuccess(updateStockQuery);

                    if (!isUpdateStockSuccess)
                    {
                        MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Xuất kho thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cart.Clear();
            ProductLoad();
            textBox1.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                cart.RemoveAt(e.RowIndex);
                UpdateTotalPrice();
                UpdateTotalQuantity();
            }
        }
    }
}
