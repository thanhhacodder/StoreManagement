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
    public partial class WarehouseDispatchDetails : Form
    {
        private int warehouseDispatchId;
        DatabaseConnection dbConnection = new DatabaseConnection();
        public WarehouseDispatchDetails(int warehouseDispatchId)
        {
            InitializeComponent();
            this.warehouseDispatchId = warehouseDispatchId;
        }

        private void WarehouseDispatchDetails_Load(object sender, EventArgs e)
        {
            string query = $@"SELECT * FROM WarehouseDispatchs
                            WHERE WarehouseDispatchID = {warehouseDispatchId}";

            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = warehouseDispatchId.ToString();
            textBox2.Text = dataTable.Rows[0]["DispatchDate"].ToString();
            textBox3.Text = dataTable.Rows[0]["TotalQuantity"].ToString();
            textBox4.Text = dataTable.Rows[0]["TotalAmount"].ToString();

            string getItemQuery = $@"SELECT Products.ProductID, Products.ProductName, Products.Description, Products.UnitPrice, WarehouseDispatchItems.Quantity  
                                     FROM WarehouseDispatchItems INNER JOIN Products ON Products.ProductID =  WarehouseDispatchItems.ProductID
                                     WHERE WarehouseDispatchID = {warehouseDispatchId}";
            DataTable dataTable1 = dbConnection.getData(getItemQuery);

            dataGridView1.DataSource = dataTable1;

        }
    }
}
