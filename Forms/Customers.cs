using StoreManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement.Forms
{
    public partial class Customers : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Customers()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Customers";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;

                if (dataGridView1.Columns["EditColumn"] == null)
                {
                    DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                    editColumn.Name = "EditColumn";
                    editColumn.HeaderText = "";
                    editColumn.Text = "Edit";
                    editColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(editColumn);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            string query = $"SELECT * FROM Customers";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
               /* dataGridView1.Columns["EditColumn"].DisplayIndex = dataGridView1.Columns.Count - 1;*/
            }
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM (SELECT CONCAT(CustomerID, FirstName, LastName, Email, Phone, Address) AS Infor, * FROM Customers) AS Subquery WHERE Infor LIKE '%{textBox1.Text}%'";

            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["Infor"].Visible = false;
            }
            else
            {
                MessageBox.Show("Không có kết quả phù hợp!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["EditColumn"].Index && e.RowIndex >= 0)
            {
                int customerId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["CustomerID"].Value);
                EditCustomerForm editCustomerForm = new EditCustomerForm(customerId);
                editCustomerForm.ShowDialog();
            }
        }

        private void thêmKháchHàngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddCustomerForm addCustomersForm = new AddCustomerForm();
            addCustomersForm.ShowDialog();
        }
    }
}
