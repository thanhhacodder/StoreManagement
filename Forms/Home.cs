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
    public partial class Home : Form
    {
        private string username;
        public Home(string username)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.username = username;
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistical statistical = new Statistical();
            statistical.Show();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OrderList orderList = new OrderList();
            orderList.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Providers providers = new Providers();
            providers.Show();
        }


        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void nhậpKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseReceipt receipt = new WarehouseReceipt();
            receipt.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void xuấtKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseDispatch warehouseDispatch = new WarehouseDispatch();
            warehouseDispatch.Show();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(username);
            changePassword.Show();
        }

        private void lịchSửNhậpKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseReceiptHistory warehouseReceiptHistory = new WarehouseReceiptHistory();
            warehouseReceiptHistory.Show();
        }

        private void lịchSửXuấtKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseDispatchHistory warehouseDispatchHistory = new WarehouseDispatchHistory();
            warehouseDispatchHistory.Show();
        }

        private void danhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
        }

        private void thêmDanhMụcSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCategoryForm addCategoryForm = new AddCategoryForm();
            addCategoryForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Statistical statistical = new Statistical();
            statistical.Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
