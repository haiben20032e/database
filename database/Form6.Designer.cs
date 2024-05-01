namespace database
{
    partial class frm6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm6));
            this.label1 = new System.Windows.Forms.Label();
            this.idphieu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.idhanghoa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sl = new System.Windows.Forms.TextBox();
            this.kieudon = new System.Windows.Forms.TextBox();
            this.bt_nhap = new System.Windows.Forms.Button();
            this.bt_xuat = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ngaynhap = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_in = new System.Windows.Forms.Button();
            this.bt_thoat = new System.Windows.Forms.Button();
            this.bt_xoa = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID phiếu";
            // 
            // idphieu
            // 
            this.idphieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.idphieu.Location = new System.Drawing.Point(108, 18);
            this.idphieu.Name = "idphieu";
            this.idphieu.Size = new System.Drawing.Size(100, 20);
            this.idphieu.TabIndex = 1;
            this.idphieu.TextChanged += new System.EventHandler(this.idphieu_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(228, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kiểu dơn";
            // 
            // idhanghoa
            // 
            this.idhanghoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.idhanghoa.Location = new System.Drawing.Point(108, 83);
            this.idhanghoa.Name = "idhanghoa";
            this.idhanghoa.Size = new System.Drawing.Size(100, 20);
            this.idhanghoa.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(228, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ngày xuất nhập";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "ID hàng hóa";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(567, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Số lượng";
            // 
            // sl
            // 
            this.sl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sl.Location = new System.Drawing.Point(634, 16);
            this.sl.Name = "sl";
            this.sl.Size = new System.Drawing.Size(100, 20);
            this.sl.TabIndex = 7;
            // 
            // kieudon
            // 
            this.kieudon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.kieudon.Location = new System.Drawing.Point(333, 81);
            this.kieudon.Name = "kieudon";
            this.kieudon.Size = new System.Drawing.Size(100, 20);
            this.kieudon.TabIndex = 8;
            // 
            // bt_nhap
            // 
            this.bt_nhap.BackColor = System.Drawing.Color.Violet;
            this.bt_nhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_nhap.Location = new System.Drawing.Point(24, 19);
            this.bt_nhap.Name = "bt_nhap";
            this.bt_nhap.Size = new System.Drawing.Size(121, 45);
            this.bt_nhap.TabIndex = 10;
            this.bt_nhap.Text = "Nhập hàng";
            this.bt_nhap.UseVisualStyleBackColor = false;
            this.bt_nhap.Click += new System.EventHandler(this.bt_nhap_Click);
            // 
            // bt_xuat
            // 
            this.bt_xuat.BackColor = System.Drawing.Color.Violet;
            this.bt_xuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_xuat.Location = new System.Drawing.Point(172, 19);
            this.bt_xuat.Name = "bt_xuat";
            this.bt_xuat.Size = new System.Drawing.Size(121, 45);
            this.bt_xuat.TabIndex = 11;
            this.bt_xuat.Text = "Xuất hàng";
            this.bt_xuat.UseVisualStyleBackColor = false;
            this.bt_xuat.Click += new System.EventHandler(this.bt_xuat_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 209);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(756, 184);
            this.dataGridView1.TabIndex = 12;
            // 
            // ngaynhap
            // 
            this.ngaynhap.Location = new System.Drawing.Point(333, 20);
            this.ngaynhap.Name = "ngaynhap";
            this.ngaynhap.Size = new System.Drawing.Size(201, 20);
            this.ngaynhap.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ngaynhap);
            this.panel1.Controls.Add(this.idphieu);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.sl);
            this.panel1.Controls.Add(this.kieudon);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.idhanghoa);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 143);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel2.Controls.Add(this.bt_in);
            this.panel2.Controls.Add(this.bt_thoat);
            this.panel2.Controls.Add(this.bt_xuat);
            this.panel2.Controls.Add(this.bt_nhap);
            this.panel2.Controls.Add(this.bt_xoa);
            this.panel2.Location = new System.Drawing.Point(3, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 84);
            this.panel2.TabIndex = 15;
            // 
            // bt_in
            // 
            this.bt_in.BackColor = System.Drawing.Color.Violet;
            this.bt_in.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_in.Image = ((System.Drawing.Image)(resources.GetObject("bt_in.Image")));
            this.bt_in.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_in.Location = new System.Drawing.Point(467, 17);
            this.bt_in.Name = "bt_in";
            this.bt_in.Size = new System.Drawing.Size(121, 45);
            this.bt_in.TabIndex = 10;
            this.bt_in.Text = "IN PDF";
            this.bt_in.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_in.UseVisualStyleBackColor = false;
            // 
            // bt_thoat
            // 
            this.bt_thoat.BackColor = System.Drawing.Color.Violet;
            this.bt_thoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_thoat.Image = ((System.Drawing.Image)(resources.GetObject("bt_thoat.Image")));
            this.bt_thoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_thoat.Location = new System.Drawing.Point(613, 17);
            this.bt_thoat.Name = "bt_thoat";
            this.bt_thoat.Size = new System.Drawing.Size(121, 45);
            this.bt_thoat.TabIndex = 9;
            this.bt_thoat.Text = "Thoát";
            this.bt_thoat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_thoat.UseVisualStyleBackColor = false;
            // 
            // bt_xoa
            // 
            this.bt_xoa.BackColor = System.Drawing.Color.Violet;
            this.bt_xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_xoa.Image = ((System.Drawing.Image)(resources.GetObject("bt_xoa.Image")));
            this.bt_xoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_xoa.Location = new System.Drawing.Point(312, 17);
            this.bt_xoa.Name = "bt_xoa";
            this.bt_xoa.Size = new System.Drawing.Size(121, 45);
            this.bt_xoa.TabIndex = 7;
            this.bt_xoa.Text = "Xóa";
            this.bt_xoa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_xoa.UseVisualStyleBackColor = false;
            this.bt_xoa.Click += new System.EventHandler(this.bt_xoa_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(247, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(256, 34);
            this.label6.TabIndex = 16;
            this.label6.Text = "Nhập xuất hàng hóa";
            // 
            // frm6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGreen;
            this.ClientSize = new System.Drawing.Size(766, 500);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frm6";
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.Form6_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idphieu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idhanghoa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sl;
        private System.Windows.Forms.TextBox kieudon;
        private System.Windows.Forms.Button bt_nhap;
        private System.Windows.Forms.Button bt_xuat;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker ngaynhap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bt_in;
        private System.Windows.Forms.Button bt_thoat;
        private System.Windows.Forms.Button bt_xoa;
        private System.Windows.Forms.Label label6;
    }
}