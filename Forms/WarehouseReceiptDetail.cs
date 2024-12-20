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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreManagement.Forms
{
    public partial class WarehouseReceiptDetail : Form
    {
        private int warehouseReceiptId;
        DatabaseConnection dbConnection = new DatabaseConnection();

        public WarehouseReceiptDetail(int warehouseReceiptId)
        {
            InitializeComponent();
            this.warehouseReceiptId = warehouseReceiptId;
        }

        private void WarehouseReceiptDetail_Load(object sender, EventArgs e)
        {
            string query = $@"SELECT * FROM WarehouseReceipts
                            WHERE WarehouseReceiptID = {warehouseReceiptId}";

            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = warehouseReceiptId.ToString();
            textBox2.Text = dataTable.Rows[0]["ReceiptDate"].ToString();
            textBox3.Text = dataTable.Rows[0]["TotalQuantity"].ToString();

            string getItemQuery = $@"SELECT Products.ProductID, Products.ProductName, Products.Description, Products.UnitPrice, WarehouseReceiptItems.Quantity  
                                     FROM WarehouseReceiptItems INNER JOIN Products ON Products.ProductID =  WarehouseReceiptItems.ProductID
                                     WHERE WarehouseReceiptID = {warehouseReceiptId}";
            DataTable dataTable1 = dbConnection.getData(getItemQuery);

            dataGridView1.DataSource = dataTable1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
