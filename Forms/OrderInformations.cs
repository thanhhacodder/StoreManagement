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
using System.IO;

namespace StoreManagement.Forms
{
    public partial class OrderInformations : Form
    {
        private int orderId;
        DatabaseConnection dbConnection = new DatabaseConnection();
        public OrderInformations(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
        }

        private void OrderInformations_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Pending");
            comboBox1.Items.Add("Confirmed");
            comboBox1.Items.Add("Delivered");
            comboBox1.Items.Add("Completed");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            string query = $@"SELECT Orders.CustomerID, Orders.OrderDate, Orders.TotalAmount, Orders.Status, CONCAT(Customers.LastName, ' ', Customers.FirstName) AS FullName, Customers.Phone, OrderDetails.ShippingAddress
                              FROM Orders INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerId
                              INNER JOIN OrderDetails ON OrderDetails.OrderID = Orders.OrderID
                              WHERE Orders.OrderID = {orderId}";
            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = orderId.ToString();
            textBox4.Text = dataTable.Rows[0]["FullName"].ToString();
            textBox2.Text = dataTable.Rows[0]["Phone"].ToString();
            textBox3.Text = dataTable.Rows[0]["ShippingAddress"].ToString();
            textBox5.Text = dataTable.Rows[0]["TotalAmount"].ToString();
            textBox6.Text = dataTable.Rows[0]["OrderDate"].ToString();


            string getOrderItemsQuery = $@"SELECT Products.ProductName, Products.UnitPrice, OrderItems.Quantity
                                           FROM Products INNER JOIN OrderItems
                                           ON Products.ProductID = OrderItems.ProductID
                                           WHERE OrderItems.OrderId = {orderId}";
            DataTable dataTableOrderItems = dbConnection.getData(getOrderItemsQuery);
            dataGridView1.DataSource = dataTableOrderItems;

            if(dataTableOrderItems != null)
            {
                dataGridView1.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dataGridView1.Columns["UnitPrice"].HeaderText = "Giá sản phẩm";
                dataGridView1.Columns["Quantity"].HeaderText = "Số lượng";

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = comboBox1.SelectedItem.ToString();
            string query = $"UPDATE Orders SET Status = '{status}' WHERE OrderID = {orderId}";
            bool isExecuteSuccess = dbConnection.isExecuteSuccess(query);
            if (isExecuteSuccess)
            {
                MessageBox.Show("Cập nhật trạng thái thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WarehouseDispatch warehouseDispatch = new WarehouseDispatch(orderId);
            warehouseDispatch.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder invoice = new StringBuilder();
            invoice.AppendLine("Thông tin hoá đơn");
            invoice.AppendLine($"Mã đơn hàng: {orderId}");
            invoice.AppendLine($"Họ và tên khách hàng: {textBox4.Text}");
            invoice.AppendLine($"Số điện thoại: {textBox2.Text}");
            invoice.AppendLine($"Địa chỉ nhận hàng: {textBox3.Text}");
            invoice.AppendLine($"Ngày đặt hàng: {textBox6.Text}");
            invoice.AppendLine($"Thành tiền: {textBox5.Text}");
            invoice.AppendLine();
            invoice.AppendLine("Danh sách mặt hàng:");

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                invoice.AppendLine($"Tên sản phẩm: {row.Cells["ProductName"].Value}\tĐơn giá: {row.Cells["UnitPrice"].Value}\tSố lượng: {row.Cells["Quantity"].Value}");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Lưu hoá đơn";
            saveFileDialog.FileName = $"hoa_don_{orderId}.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
            {
                File.WriteAllText(saveFileDialog.FileName, invoice.ToString());
                MessageBox.Show("Xuất hoá đơn thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
