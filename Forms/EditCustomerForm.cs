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
    public partial class EditCustomerForm : Form
    {
        private int customerId;
        DatabaseConnection dbConnection = new DatabaseConnection();
        public EditCustomerForm(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
        }

        private void EditCustomerForm_Load(object sender, EventArgs e)
        {
            string query = $"SElECT * FROM Customers WHERE CustomerID = {customerId}";
            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                textBox1.Text = dataTable.Rows[0]["LastName"].ToString();
                textBox9.Text = dataTable.Rows[0]["FirstName"].ToString();
                textBox7.Text = dataTable.Rows[0]["Email"].ToString();
                textBox6.Text = dataTable.Rows[0]["Phone"].ToString();
                textBox2.Text = dataTable.Rows[0]["Address"].ToString();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string lastName = textBox1.Text;
            string firstName = textBox9.Text;
            string email = textBox7.Text;
            string phone = textBox6.Text;
            string address = textBox2.Text;

            if (string.IsNullOrEmpty(lastName) ||
            string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $@"UPDATE Customers 
                              SET LastName = '{lastName}', FirstName = '{firstName}', Email = '{email}', Phone = '{phone}', Address = '{address}' 
                              WHERE CustomerID = {customerId}";

            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Chỉnh sửa khách hàng thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
