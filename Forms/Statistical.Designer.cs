namespace StoreManagement.Forms
{
    partial class Statistical
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.thốngKêTheoNămToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêTổngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.thốngKêTrongNgàyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêTrongThángToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tổng số đơn hàng";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(51, 109);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(322, 34);
            this.textBox1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thốngKêTrongNgàyToolStripMenuItem,
            this.thốngKêTrongThángToolStripMenuItem,
            this.thốngKêTheoNămToolStripMenuItem,
            this.thốngKêTổngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1158, 46);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // thốngKêTheoNămToolStripMenuItem
            // 
            this.thốngKêTheoNămToolStripMenuItem.Image = global::StoreManagement.Properties.Resources.icon22;
            this.thốngKêTheoNămToolStripMenuItem.Name = "thốngKêTheoNămToolStripMenuItem";
            this.thốngKêTheoNămToolStripMenuItem.Size = new System.Drawing.Size(235, 44);
            this.thốngKêTheoNămToolStripMenuItem.Text = "Thống kê theo năm";
            this.thốngKêTheoNămToolStripMenuItem.Click += new System.EventHandler(this.thốngKêTheoNămToolStripMenuItem_Click);
            // 
            // thốngKêTổngToolStripMenuItem
            // 
            this.thốngKêTổngToolStripMenuItem.Image = global::StoreManagement.Properties.Resources.icon23;
            this.thốngKêTổngToolStripMenuItem.Name = "thốngKêTổngToolStripMenuItem";
            this.thốngKêTổngToolStripMenuItem.Size = new System.Drawing.Size(192, 44);
            this.thốngKêTổngToolStripMenuItem.Text = "Thống kê tổng";
            this.thốngKêTổngToolStripMenuItem.Click += new System.EventHandler(this.thốngKêTổngToolStripMenuItem_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(444, 109);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(322, 34);
            this.textBox2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(439, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tổng doanh thu";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 202);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(1158, 247);
            this.dataGridView1.TabIndex = 5;
            // 
            // thốngKêTrongNgàyToolStripMenuItem
            // 
            this.thốngKêTrongNgàyToolStripMenuItem.Image = global::StoreManagement.Properties.Resources.icon22;
            this.thốngKêTrongNgàyToolStripMenuItem.Name = "thốngKêTrongNgàyToolStripMenuItem";
            this.thốngKêTrongNgàyToolStripMenuItem.Size = new System.Drawing.Size(246, 44);
            this.thốngKêTrongNgàyToolStripMenuItem.Text = "Thống kê trong ngày";
            this.thốngKêTrongNgàyToolStripMenuItem.Click += new System.EventHandler(this.thốngKêTrongNgàyToolStripMenuItem_Click);
            // 
            // thốngKêTrongThángToolStripMenuItem
            // 
            this.thốngKêTrongThángToolStripMenuItem.Image = global::StoreManagement.Properties.Resources.icon221;
            this.thốngKêTrongThángToolStripMenuItem.Name = "thốngKêTrongThángToolStripMenuItem";
            this.thốngKêTrongThángToolStripMenuItem.Size = new System.Drawing.Size(246, 44);
            this.thốngKêTrongThángToolStripMenuItem.Text = "Thống kê theo tháng";
            this.thốngKêTrongThángToolStripMenuItem.Click += new System.EventHandler(this.thốngKêTrongThángToolStripMenuItem_Click);
            // 
            // Statistical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1158, 448);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Statistical";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống Kê";
            this.Load += new System.EventHandler(this.Statistical_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem thốngKêTrongNgàyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thốngKêTrongThángToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thốngKêTheoNămToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thốngKêTổngToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}