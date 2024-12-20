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
    public partial class Employees : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Employees()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void dataGridViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewEmployee.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                int employeeId = Convert.ToInt32(dataGridViewEmployee.Rows[e.RowIndex].Cells["EmployeeID"].Value);

                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xoá nhân viên này?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string deleteAccountQuery = $"DELETE FROM Accounts WHERE EmployeeID = {employeeId}";
                    dbConnection.isExecuteSuccess(deleteAccountQuery);
                    string deleteEmployeeQuery = $"DELETE FROM Employees WHERE EmployeeID = {employeeId}";
                    bool isEmployeeDeleted = dbConnection.isExecuteSuccess(deleteEmployeeQuery);

                    if (isEmployeeDeleted)
                    {
                        dataGridViewEmployee.Rows.RemoveAt(e.RowIndex);
                        MessageBox.Show("Xoá nhân viên thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (e.ColumnIndex == dataGridViewEmployee.Columns["EditColumn"].Index && e.RowIndex >= 0)
            {
                int employeeId = Convert.ToInt32(dataGridViewEmployee.Rows[e.RowIndex].Cells["EmployeeID"].Value);
                EditEmployeeForm editEmployeeForm = new EditEmployeeForm(employeeId);
                editEmployeeForm.Show();
            }
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string query = $@"
        SELECT EmployeeID, FirstName, LastName, Email, Phone, Address, Position, Salary, HireDate
        FROM Employees
        WHERE CONCAT(EmployeeID, ' ', FirstName, ' ', LastName) LIKE '%{textBox2.Text}%'
           OR Email LIKE '%{textBox2.Text}%'
           OR Phone LIKE '%{textBox2.Text}%'
           OR Address LIKE '%{textBox2.Text}%'
           OR Position LIKE '%{textBox2.Text}%'
           OR CONVERT(varchar, Salary) LIKE '%{textBox2.Text}%'
           OR CONVERT(varchar, HireDate, 103) LIKE '%{textBox2.Text}%'";

            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                dataGridViewEmployee.DataSource = dataTable;
                SetEmployeeGridHeaders();
                HideFullNameColumn();
            }
            else
            {
                MessageBox.Show("Không có kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            string query = "SELECT * FROM Employees";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridViewEmployee.DataSource = dataTable;
                SetEmployeeGridHeaders();
                AddEditAndDeleteColumns();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetEmployeeGridHeaders()
        {
            dataGridViewEmployee.Columns["EmployeeID"].HeaderText = "Mã nhân viên";
            dataGridViewEmployee.Columns["FirstName"].HeaderText = "Tên";
            dataGridViewEmployee.Columns["LastName"].HeaderText = "Họ";
            dataGridViewEmployee.Columns["Email"].HeaderText = "Email";
            dataGridViewEmployee.Columns["Phone"].HeaderText = "Số điện thoại";
            dataGridViewEmployee.Columns["Address"].HeaderText = "Địa chỉ";
            dataGridViewEmployee.Columns["Position"].HeaderText = "Chức vụ";
            dataGridViewEmployee.Columns["Salary"].HeaderText = "Lương";
            dataGridViewEmployee.Columns["HireDate"].HeaderText = "Ngày bắt đầu";
        }

        private void AddEditAndDeleteColumns()
        {
            if (dataGridViewEmployee.Columns["EditColumn"] == null)
            {
                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                editColumn.Name = "EditColumn";
                editColumn.HeaderText = "";
                editColumn.Text = "Edit";
                editColumn.UseColumnTextForButtonValue = true;
                dataGridViewEmployee.Columns.Add(editColumn);
            }

            if (dataGridViewEmployee.Columns["DeleteColumn"] == null)
            {
                DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
                deleteColumn.Name = "DeleteColumn";
                deleteColumn.HeaderText = "";
                deleteColumn.Text = "Delete";
                deleteColumn.UseColumnTextForButtonValue = true;
                dataGridViewEmployee.Columns.Add(deleteColumn);
            }

            dataGridViewEmployee.Columns["DeleteColumn"].DisplayIndex = dataGridViewEmployee.Columns.Count - 1;
            dataGridViewEmployee.Columns["EditColumn"].DisplayIndex = dataGridViewEmployee.Columns.Count - 2;
        }

        private void HideFullNameColumn()
        {
            if (dataGridViewEmployee.Columns.Contains("FullName"))
            {
                dataGridViewEmployee.Columns["FullName"].Visible = false;
            }
        }

    }
}
