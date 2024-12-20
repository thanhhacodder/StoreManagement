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
    public partial class OrderList : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public OrderList()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void getOrdersData()
        {
            string query = $@"SELECT Orders.OrderId, Orders.CustomerId, CONCAT(Customers.LastName, ' ', Customers.FirstName) as FullName, Orders.OrderDate, Customers.Phone, Customers.Address
                              FROM Orders INNER 
                              JOIN Customers 
                              ON Orders.CustomerID = Customers.CustomerID 
                              ORDER BY Orders.OrderID DESC";

            DataTable dataTable = dbConnection.getData(query);

            if (dataGridView1.Columns["OrderDetails"] == null)
            {
                DataGridViewButtonColumn inforColumn = new DataGridViewButtonColumn();
                inforColumn.Name = "OrderDetails";
                inforColumn.HeaderText = "Thông tin";
                inforColumn.Text = "Chi tiết";
                inforColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(inforColumn);
            }
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["OrderDetails"].DisplayIndex = dataGridView1.Columns.Count - 1;

            dataGridView1.Columns["OrderId"].HeaderText = "Mã đơn hàng";
            dataGridView1.Columns["CustomerId"].HeaderText = "Mã khách hàng";
            dataGridView1.Columns["FullName"].HeaderText = "Họ tên khách hàng";
            dataGridView1.Columns["OrderDate"].HeaderText = "Ngày đặt hàng";
            dataGridView1.Columns["Phone"].HeaderText = "Số điện thoại";
            dataGridView1.Columns["Address"].HeaderText = "Địa chỉ";

        }

        private void search_button_Click(object sender, EventArgs e)
        {
            string query = $@"SELECT * 
                              FROM (
                                SELECT CONCAT(Orders.OrderId, Orders.CustomerId,Customers.LastName,Customers.FirstName, Orders.OrderDate, Customers.Phone, Customers.Address) AS OrderInfo,
                                Orders.OrderId, Orders.CustomerId, CONCAT(Customers.LastName, ' ', Customers.FirstName) as FullName, Orders.OrderDate, Customers.Phone, Customers.Address
                                 FROM Orders INNER JOIN Customers 
                                 ON Orders.CustomerID = Customers.CustomerID) 
                                 AS Subquery WHERE OrderInfo LIKE '%{textBox1.Text}%';";

            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["OrderInfo"].Visible = false;
            }
            else
            {
                MessageBox.Show("Không có kết quả phù hợp!");
            }
            dataGridView1.Columns["OrderDetails"].DisplayIndex = dataGridView1.Columns.Count - 1;
        }

        private void OrdersList_Load(object sender, EventArgs e)
        {
            getOrdersData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value);

            if (e.ColumnIndex == dataGridView1.Columns["OrderDetails"].Index && e.RowIndex >= 0)
            {
                OrderInformations orderInformations = new OrderInformations(orderId);
                orderInformations.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getOrdersData();
            textBox1.Text = string.Empty;
        }
    }
}
