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
    public partial class Categories : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Categories()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                int categoryId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["CategoryID"].Value);

                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn danh mục này?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string deleteProductQuery = $"UPDATE Categories SET Deleted = 1 WHERE CategoryID = {categoryId}";
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

        }

        private void Categories_Load(object sender, EventArgs e)
        {
            string query = "SELECT CategoryID, CategoryName, Description FROM Categories WHERE Deleted = 0";
            DataTable dataTable = dbConnection.getData(query);

            dataGridView1.DataSource = dataTable;

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
}
