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
using StoreManagement.Helpers;

namespace StoreManagement.Forms
{
    public partial class AddEmployeeForm : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void AddEmployeeButton_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int employeeId = generateId.Generate("Employees");
            string lastName = textBox5.Text;
            string firstName = textBox9.Text;
            string email = textBox8.Text;
            string phone = textBox7.Text;
            string address = textBox6.Text;
            string position = textBox1.Text;
            DateTime hireDate = dateTimePicker1.Value;

            if (string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(position) ||
                string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float salary = float.Parse(textBox4.Text);

            string query = $@"INSERT INTO Employees (EmployeeID, LastName, FirstName, Email, Phone, Address, Position, Salary, HireDate) 
                            VALUES ({employeeId}, '{lastName}', '{firstName}', '{email}', '{phone}', '{address}', '{position}', {salary}, '{hireDate.ToString("yyyy-MM-dd")}')";


            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Thêm nhân viên thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEmployeeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
