
namespace database
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tk = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.btdn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataSet1 = new database.DataSet1();
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tài khoản";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mật khẩu";
            // 
            // tk
            // 
            this.tk.BackColor = System.Drawing.SystemColors.Info;
            this.tk.Location = new System.Drawing.Point(159, 74);
            this.tk.Name = "tk";
            this.tk.Size = new System.Drawing.Size(123, 20);
            this.tk.TabIndex = 3;
            this.tk.Text = "a";
            this.tk.TextChanged += new System.EventHandler(this.tk_TextChanged);
            // 
            // pass
            // 
            this.pass.BackColor = System.Drawing.SystemColors.Info;
            this.pass.Location = new System.Drawing.Point(159, 140);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(123, 20);
            this.pass.TabIndex = 4;
            this.pass.Text = "a";
            this.pass.TextChanged += new System.EventHandler(this.pass_TextChanged);
            // 
            // btdn
            // 
            this.btdn.Location = new System.Drawing.Point(159, 195);
            this.btdn.Name = "btdn";
            this.btdn.Size = new System.Drawing.Size(123, 30);
            this.btdn.TabIndex = 5;
            this.btdn.Text = "Đăng nhập";
            this.btdn.UseVisualStyleBackColor = true;
            this.btdn.Click += new System.EventHandler(this.btdn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(544, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 48);
            this.button1.TabIndex = 6;
            this.button1.Text = "thoát";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataSet1BindingSource
            // 
            this.dataSet1BindingSource.DataSource = this.dataSet1;
            this.dataSet1BindingSource.Position = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btdn);
            this.panel1.Controls.Add(this.pass);
            this.panel1.Controls.Add(this.tk);
            this.panel1.Location = new System.Drawing.Point(150, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 272);
            this.panel1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(659, 427);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tk;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Button btdn;
        private System.Windows.Forms.Button button1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private System.Windows.Forms.Panel panel1;
    }
}