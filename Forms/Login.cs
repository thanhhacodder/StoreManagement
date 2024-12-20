using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoreManagement.Helpers;

namespace StoreManagement.Forms
{
    public partial class Login : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }


    private bool Authentication(string inputUsername, string inputPassword)
    {
        if (inputUsername == "admin" && inputPassword == "admin") {
            return true;
        }

        string query = $"SELECT * FROM Accounts WHERE Username = '{inputUsername}'";
        DataTable dataTable = dbConnection.getData(query);

        if (dataTable.Rows.Count > 0)
        {
            string password = dataTable.Rows[0]["Password"].ToString();

            if (inputPassword == password)
            {
                MessageBox.Show("Đăng nhập thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không chính xác!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
                MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không chính xác!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return false;
    }


    private bool isAdmin(string username)
        {
            if (username == "admin")
            {
                return true;
            }

            string query = $"SELECT * FROM Accounts WHERE Username = '{username}'";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                string role = dataTable.Rows[0]["Role"].ToString();
                if (role == "Admin")
                {
                    return true;
                }
            }

            return false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            bool isAuth = Authentication(username, password);
            if(isAuth)
            {
                if (isAdmin(username))
                {
                    Admin admin = new Admin();
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    Home home = new Home(username);
                    home.Show();
                    this.Hide();
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
