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
    public partial class Statistical : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public Statistical()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Statistical_Load(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) AS OrderCount, SUM(TotalAmount) AS TotalAmount FROM Orders;";
            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = dataTable.Rows[0]["OrderCount"].ToString();
            textBox2.Text = dataTable.Rows[0]["TotalAmount"].ToString();

            string getAllOrdersQuery = "SELECT * FROM Orders;";
            DataTable getAllOrdersData = dbConnection.getData(getAllOrdersQuery);
            dataGridView1.DataSource = getAllOrdersData;
        }

        private void thốngKêTrongNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) AS OrderCount, SUM(TotalAmount) AS TotalAmount  FROM Orders WHERE YEAR(OrderDate) = YEAR(GETDATE()) AND MONTH(OrderDate) = MONTH(GETDATE()) AND DAY(OrderDate) = DAY(GETDATE());";
            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = dataTable.Rows[0]["OrderCount"].ToString();
            textBox2.Text = dataTable.Rows[0]["TotalAmount"].ToString();

            string getAllOrdersQuery = "SELECT * FROM Orders WHERE YEAR(OrderDate) = YEAR(GETDATE()) AND MONTH(OrderDate) = MONTH(GETDATE()) AND DAY(OrderDate) = DAY(GETDATE());";
            DataTable getAllOrdersData = dbConnection.getData(getAllOrdersQuery);
            dataGridView1.DataSource = getAllOrdersData;
        }

        private void thốngKêTrongThángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) AS OrderCount, SUM(TotalAmount) AS TotalAmount  FROM Orders WHERE YEAR(OrderDate) = YEAR(GETDATE()) AND MONTH(OrderDate) = MONTH(GETDATE());";
            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = dataTable.Rows[0]["OrderCount"].ToString();
            textBox2.Text = dataTable.Rows[0]["TotalAmount"].ToString();

            string getAllOrdersQuery = "SELECT * FROM Orders WHERE YEAR(OrderDate) = YEAR(GETDATE()) AND MONTH(OrderDate) = MONTH(GETDATE());";
            DataTable getAllOrdersData = dbConnection.getData(getAllOrdersQuery);
            dataGridView1.DataSource = getAllOrdersData;
        }

        private void thốngKêTheoNămToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) AS OrderCount, SUM(TotalAmount) AS TotalAmount  FROM Orders WHERE YEAR(OrderDate) = YEAR(GETDATE());";
            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = dataTable.Rows[0]["OrderCount"].ToString();
            textBox2.Text = dataTable.Rows[0]["TotalAmount"].ToString();

            string getAllOrdersQuery = "SELECT * FROM Orders WHERE YEAR(OrderDate) = YEAR(GETDATE());";
            DataTable getAllOrdersData = dbConnection.getData(getAllOrdersQuery);
            dataGridView1.DataSource = getAllOrdersData;
        }

        private void thốngKêTổngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) AS OrderCount, SUM(TotalAmount) AS TotalAmount FROM Orders;";
            DataTable dataTable = dbConnection.getData(query);
            textBox1.Text = dataTable.Rows[0]["OrderCount"].ToString();
            textBox2.Text = dataTable.Rows[0]["TotalAmount"].ToString();

            string getAllOrdersQuery = "SELECT * FROM Orders;";
            DataTable getAllOrdersData = dbConnection.getData(getAllOrdersQuery);
            dataGridView1.DataSource = getAllOrdersData;
        }
    }
}
