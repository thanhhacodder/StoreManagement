using System;
using System.Collections;
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
    public partial class Admin : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Admin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }


        private void Admin_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Employees";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dgvEmployee.DataSource = dataTable;

                dgvEmployee.Columns["EmployeeID"].HeaderText = "Mã nhân viên";
                dgvEmployee.Columns["FirstName"].HeaderText = "Tên";
                dgvEmployee.Columns["LastName"].HeaderText = "Họ";
                dgvEmployee.Columns["Email"].HeaderText = "Email";
                dgvEmployee.Columns["Phone"].HeaderText = "Số điện thoại";
                dgvEmployee.Columns["Address"].HeaderText = "Địa chỉ";
                dgvEmployee.Columns["Position"].HeaderText = "Chức vụ";
                dgvEmployee.Columns["Salary"].HeaderText = "Lương";
                dgvEmployee.Columns["HireDate"].HeaderText = "Ngày bắt đầu";
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void search_button_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            string query = $@"
        SELECT EmployeeID, FirstName, LastName, Email, Phone, Address, Position, Salary, HireDate
        FROM Employees
        WHERE CONCAT(EmployeeID, ' ', FirstName, ' ', LastName) LIKE '%{searchText}%'
           OR Email LIKE '%{searchText}%'
           OR Phone LIKE '%{searchText}%'
           OR Address LIKE '%{searchText}%'
           OR Position LIKE '%{searchText}%'
           OR CONVERT(varchar, Salary) LIKE '%{searchText}%'
           OR CONVERT(varchar, HireDate, 103) LIKE '%{searchText}%'";

            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dgvEmployee.DataSource = dataTable;

                dgvEmployee.Columns["EmployeeID"].HeaderText = "Mã nhân viên";
                dgvEmployee.Columns["FirstName"].HeaderText = "Tên";
                dgvEmployee.Columns["LastName"].HeaderText = "Họ";
                dgvEmployee.Columns["Email"].HeaderText = "Email";
                dgvEmployee.Columns["Phone"].HeaderText = "Số điện thoại";
                dgvEmployee.Columns["Address"].HeaderText = "Địa chỉ";
                dgvEmployee.Columns["Position"].HeaderText = "Chức vụ";
                dgvEmployee.Columns["Salary"].HeaderText = "Lương";
                dgvEmployee.Columns["HireDate"].HeaderText = "Ngày bắt đầu";
            }
            else
            {
                MessageBox.Show("Không có kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void thêmNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEmployeeForm addEmployeeForm = new AddEmployeeForm();
            addEmployeeForm.Show();
        }

        private void Reload()
        {
            string query = $"SELECT * FROM Employees";
            DataTable dataTable = dbConnection.getData(query);

            dgvEmployee.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            Reload();
            
        }

        private void danhSáchNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
        }

        private void tạoTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateAccountForm createAccountForm = new CreateAccountForm();  
            createAccountForm.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (confirm == DialogResult.OK)
            {
               Login login = new Login();
                login.Show();
                this.Close();
            }
            return;
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
