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
    public partial class EditEmployeeForm : Form
    {
        private int employeeId;
        DatabaseConnection dbConnection = new DatabaseConnection();
        public EditEmployeeForm(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditEmployeeForm_Load(object sender, EventArgs e)
        {
            string query = $"SElECT * FROM Employees WHERE EmployeeID = {employeeId}";
            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                textBox1.Text = dataTable.Rows[0]["LastName"].ToString();
                textBox9.Text = dataTable.Rows[0]["FirstName"].ToString();
                textBox3.Text = dataTable.Rows[0]["Email"].ToString();
                textBox2.Text = dataTable.Rows[0]["Phone"].ToString();
                textBox6.Text = dataTable.Rows[0]["Address"].ToString();
                textBox5.Text = dataTable.Rows[0]["Position"].ToString();
                textBox4.Text = dataTable.Rows[0]["Salary"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataTable.Rows[0]["HireDate"]);
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

        private void Button_Click(object sender, EventArgs e)
        {
            string lastName = textBox1.Text;
            string firstName = textBox9.Text;
            string email = textBox3.Text;
            string phone = textBox2.Text;
            string address = textBox6.Text;
            string position = textBox5.Text;
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

            string query = $@"UPDATE Employees 
                              SET LastName = '{lastName}', FirstName = '{firstName}', Email = '{email}', Phone = '{phone}', Address = '{address}', Position = '{position}', Salary = {salary}, HireDate = '{hireDate.ToString("yyyy-MM-dd")}' 
                              WHERE EmployeeID = {employeeId}";

            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Chỉnh sửa nhân viên thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
