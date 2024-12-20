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
    public partial class CreateAccountForm : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public CreateAccountForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (textBox2.PasswordChar == '*')
                {
                    textBox2.PasswordChar = '\0';
                    textBox3.PasswordChar = '\0';
                }
                else
                {
                    textBox2.PasswordChar = '*';
                    textBox3.PasswordChar = '*';
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAccountForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
               string.IsNullOrEmpty(textBox4.Text) ||
               string.IsNullOrEmpty(textBox2.Text) ||
               string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GenerateId generateId = new GenerateId();
            int accountId = generateId.Generate("Accounts");
            int employeeId = Convert.ToInt32(textBox1.Text);
            string username = textBox4.Text;
            string password = textBox2.Text;
            string retypePassword = textBox3.Text;
            string role = checkBox2.Checked ? "Admin" : "User";


            if(password != retypePassword)
            {
                MessageBox.Show("Nhập lại mật khẩu không trùng khớp!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(password.Length < 6)
            {
                MessageBox.Show("Mật khẩu tối thiểu 6 ký tự!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string checkEmployeeQuery = $"SELECT * FROM Employees WHERE EmployeeID = '{employeeId}'";
            bool hasEmployee = dbConnection.getData(checkEmployeeQuery).Rows.Count > 0;

            string checkExistAccountQuery = $"SELECT * FROM Accounts WHERE Username = '{username}'";
            bool isExistAccount = dbConnection.getData(checkExistAccountQuery).Rows.Count > 0;

            if (isExistAccount)
            {
                MessageBox.Show("Tên tài khoản đã tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hasEmployee)
            {
                string insertQuery = $"INSERT INTO Accounts (AccountID, EmployeeID, Username, Password, Role) VALUES ({accountId}, {employeeId}, '{username}', '{password}', '{role}')";
                bool isSuccess = dbConnection.isExecuteSuccess(insertQuery);

                if (isSuccess)
                {
                    MessageBox.Show("Tạo tài khoản thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    checkBox2.Checked = false;
                }
                else
                {
                    MessageBox.Show("Tạo tài khoản thất bại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                MessageBox.Show("Mã nhân viên không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
