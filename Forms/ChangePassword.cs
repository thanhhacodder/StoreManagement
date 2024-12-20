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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreManagement.Forms
{
    public partial class ChangePassword : Form
    {
        private string username;
        private DatabaseConnection dbConnection = new DatabaseConnection();
        public ChangePassword(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            {
                textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
                textBox3.PasswordChar = checkBox1.Checked ? '\0' : '*';
                textBox1.PasswordChar = checkBox1.Checked ? '\0' : '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
               string.IsNullOrEmpty(textBox2.Text) ||
               string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = textBox2.Text;
            string newPassword = textBox1.Text;
            string retypeNewPassword = textBox3.Text;


            if (newPassword != retypeNewPassword)
            {
                MessageBox.Show("Nhập lại mật khẩu không trùng khớp!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (newPassword.Length < 6)
            {
                MessageBox.Show("Mật khẩu tối thiểu 6 ký tự!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $"SELECT Password FROM Accounts WHERE Username = '{username}'";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count == 1)
            {
                string storedPassword = dataTable.Rows[0]["Password"].ToString();

                if (password != storedPassword)
                {
                    MessageBox.Show("Mật khẩu không chính xác!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string updateQuery = $"UPDATE Accounts SET Password = '{newPassword}' WHERE Username = '{username}'";
            bool isSuccess = dbConnection.isExecuteSuccess(updateQuery);

            if (isSuccess)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
