using StoreManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement.Forms
{
    public partial class Products : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Products()
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

                dataGridView1.Columns["ProductID"].HeaderText = "Mã sản phẩm";
                dataGridView1.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dataGridView1.Columns["Description"].HeaderText = "Mô tả";
                dataGridView1.Columns["CategoryName"].HeaderText = "Danh mục";
                dataGridView1.Columns["ProviderName"].HeaderText = "Nhà cung cấp";
                dataGridView1.Columns["UnitPrice"].HeaderText = "Đơn giá";
                dataGridView1.Columns["StockQuantity"].HeaderText = "Số lượng";

                if (dataGridView1.Columns["EditColumn"] == null)
                {
                    DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                    editColumn.Name = "EditColumn";
                    editColumn.HeaderText = "";
                    editColumn.Text = "Sửa";
                    editColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(editColumn);
                }

                if (dataGridView1.Columns["DeleteColumn"] == null)
                {
                    DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
                    deleteColumn.Name = "DeleteColumn";
                    deleteColumn.HeaderText = "";
                    deleteColumn.Text = "Xoá";
                    deleteColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(deleteColumn);
                }
            }

           
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            string query = $@"
                SELECT Products.ProductID, Products.ProductName, Products.Description, Providers.ProviderName, Categories.CategoryName, Products.UnitPrice, Products.StockQuantity 
                FROM Products 
                INNER JOIN Providers ON Products.ProviderID = Providers.ProviderID 
                INNER JOIN Categories ON Categories.CategoryID = Products.CategoryID 
                WHERE Products.Deleted = 0 AND 
                      (Products.ProductID LIKE '%{searchText}%' OR 
                       Products.ProductName LIKE '%{searchText}%' OR 
                       Products.Description LIKE '%{searchText}%' OR 
                       Providers.ProviderName LIKE '%{searchText}%' OR 
                       Categories.CategoryName LIKE '%{searchText}%')";

            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;

                if (dataGridView1.Columns["EditColumn"] == null)
                {
                    DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                    editColumn.Name = "EditColumn";
                    editColumn.HeaderText = "";
                    editColumn.Text = "Sửa";
                    editColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(editColumn);
                }

                if (dataGridView1.Columns["DeleteColumn"] == null)
                {
                    DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
                    deleteColumn.Name = "DeleteColumn";
                    deleteColumn.HeaderText = "";
                    deleteColumn.Text = "Xoá";
                    deleteColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(deleteColumn);
                }

                dataGridView1.Columns["ProductID"].HeaderText = "Mã sản phẩm";
                dataGridView1.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dataGridView1.Columns["Description"].HeaderText = "Mô tả";
                dataGridView1.Columns["CategoryName"].HeaderText = "Danh mục";
                dataGridView1.Columns["ProviderName"].HeaderText = "Nhà cung cấp";
                dataGridView1.Columns["UnitPrice"].HeaderText = "Đơn giá";
                dataGridView1.Columns["StockQuantity"].HeaderText = "Số lượng";
            }
            else
            {
                MessageBox.Show("Không có kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);

                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xoá sản phẩm?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string deleteProductQuery = $"UPDATE Products SET Deleted = 1 WHERE ProductID = {productId}";
                    bool isProductDeleted = dbConnection.isExecuteSuccess(deleteProductQuery);

                    if (isProductDeleted)
                    {
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        MessageBox.Show("Xoá sản phẩm thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (e.ColumnIndex == dataGridView1.Columns["EditColumn"].Index && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
                EditProductForm editProductForm = new EditProductForm(productId);    
                editProductForm.Show();
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            ProductLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductLoad();
            textBox1.Text = string.Empty;
        }

        private void thêmSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm();
            addProductForm.Show();
        }
    }
}
