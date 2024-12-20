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
    public partial class WarehouseDispatchHistory : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public WarehouseDispatchHistory()
        {
            InitializeComponent();
        }

        private void WarehouseDispatchHistory_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM WarehouseDispatchs ORDER BY WarehouseDispatchID DESC";
            DataTable dataTable = dbConnection.getData(query);


            if (dataGridView1.Columns["Details"] == null)
            {
                DataGridViewButtonColumn inforColumn = new DataGridViewButtonColumn();
                inforColumn.Name = "Details";
                inforColumn.HeaderText = "Thông tin";
                inforColumn.Text = "Chi tiết";
                inforColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(inforColumn);
            }

            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["Details"].DisplayIndex = dataGridView1.Columns.Count - 1;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int warehouseDispatchId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["WarehouseDispatchID"].Value);

            if (e.ColumnIndex == dataGridView1.Columns["Details"].Index && e.RowIndex >= 0)
            {
                WarehouseDispatchDetails warehouseDispatchDetails = new WarehouseDispatchDetails(warehouseDispatchId);
                warehouseDispatchDetails.Show();
            }
        }
    }
}
