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
    public partial class Providers : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Providers()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void thêmNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProviderForm addProviderForm = new AddProviderForm();
            addProviderForm.ShowDialog();   
        }

        private void Providers_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Providers";
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

        private void search_button_Click(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM (SELECT CONCAT(ProviderID, ProviderName, ContactPerson, Email, Phone, Address) AS Infor, * FROM Providers) AS Subquery WHERE Infor LIKE '%{textBox1.Text}%'";

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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            string query = $"SELECT * FROM Providers";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
               /* dataGridView1.Columns["EditColumn"].DisplayIndex = dataGridView1.Columns.Count - 1;*/
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["EditColumn"].Index && e.RowIndex >= 0)
            {
                int providerId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProviderID"].Value);
                EditProviderForm editProviderForm = new EditProviderForm(providerId);
                editProviderForm.ShowDialog();
            }
        }
    }
}
