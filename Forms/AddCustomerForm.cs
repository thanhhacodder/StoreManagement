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
    public partial class AddCustomerForm : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int customerId = generateId.Generate("Customers");

            string lastname = textBox1.Text;
            string firstName = textBox10.Text;
            string email = textBox8.Text;
            string phone = textBox5.Text;
            string address = textBox4.Text;

            if (string.IsNullOrEmpty(lastname) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $@"INSERT INTO Customers (CustomerID, FirstName, LastName, Email, Phone, Address) 
                    VALUES ({customerId}, '{firstName}', '{lastname}', '{email}', '{phone}', '{address}')";

            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
