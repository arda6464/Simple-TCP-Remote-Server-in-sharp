namespace adminpanel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            rgblbl = new Label();
            kullanıcıtb = new TextBox();
            sifretb = new TextBox();
            loginbtn = new Button();
            rgbtimer = new System.Windows.Forms.Timer(components);
            hglb = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            panel2 = new Panel();
            panel3 = new Panel();
            kptbtn = new Button();
            kucultbtn = new Button();
            cercevepnl = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // rgblbl
            // 
            rgblbl.BackColor = Color.FromArgb(18, 18, 18);
            rgblbl.Font = new Font("Segoe UI", 15F);
            rgblbl.ForeColor = Color.Lime;
            rgblbl.Location = new Point(126, 171);
            rgblbl.Name = "rgblbl";
            rgblbl.Size = new Size(209, 46);
            rgblbl.TabIndex = 0;
            rgblbl.Text = "Admin Panel V1";
            rgblbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // kullanıcıtb
            // 
            kullanıcıtb.BackColor = Color.FromArgb(18, 18, 18);
            kullanıcıtb.BorderStyle = BorderStyle.None;
            kullanıcıtb.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            kullanıcıtb.ForeColor = Color.Lime;
            kullanıcıtb.Location = new Point(126, 283);
            kullanıcıtb.Name = "kullanıcıtb";
            kullanıcıtb.PlaceholderText = "Kullanıcı Adı...";
            kullanıcıtb.Size = new Size(191, 20);
            kullanıcıtb.TabIndex = 3;
            kullanıcıtb.Click += kullanıcıtb_Click;
            // 
            // sifretb
            // 
            sifretb.BackColor = Color.FromArgb(18, 18, 18);
            sifretb.BorderStyle = BorderStyle.None;
            sifretb.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            sifretb.ForeColor = Color.Lime;
            sifretb.Location = new Point(126, 360);
            sifretb.Name = "sifretb";
            sifretb.PlaceholderText = "Şifre...";
            sifretb.Size = new Size(160, 20);
            sifretb.TabIndex = 4;
            sifretb.UseSystemPasswordChar = true;
            sifretb.Click += sifretb_Click;
            // 
            // loginbtn
            // 
            loginbtn.BackColor = Color.FromArgb(30, 30, 30);
            loginbtn.FlatStyle = FlatStyle.Flat;
            loginbtn.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            loginbtn.ForeColor = Color.Lime;
            loginbtn.Location = new Point(57, 442);
            loginbtn.Name = "loginbtn";
            loginbtn.RightToLeft = RightToLeft.Yes;
            loginbtn.Size = new Size(317, 42);
            loginbtn.TabIndex = 5;
            loginbtn.Text = "Giriş Yap";
            loginbtn.UseVisualStyleBackColor = false;
            loginbtn.Click += loginbtn_Click;
            loginbtn.MouseEnter += loginbtn_MouseEnter;
            loginbtn.MouseLeave += loginbtn_MouseLeave;
            // 
            // rgbtimer
            // 
            rgbtimer.Enabled = true;
            rgbtimer.Interval = 50;
            // 
            // hglb
            // 
            hglb.AutoSize = true;
            hglb.BackColor = Color.FromArgb(18, 18, 18);
            hglb.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            hglb.ForeColor = Color.Lime;
            hglb.Location = new Point(141, 217);
            hglb.Name = "hglb";
            hglb.Size = new Size(0, 19);
            hglb.TabIndex = 6;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(141, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(176, 140);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(57, 254);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(51, 61);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(57, 344);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(51, 49);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonFace;
            panel2.Location = new Point(57, 321);
            panel2.Name = "panel2";
            panel2.Size = new Size(317, 1);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.BackColor = Color.AliceBlue;
            panel3.Location = new Point(57, 399);
            panel3.Name = "panel3";
            panel3.Size = new Size(317, 1);
            panel3.TabIndex = 12;
            // 
            // kptbtn
            // 
            kptbtn.FlatStyle = FlatStyle.Flat;
            kptbtn.Location = new Point(445, -1);
            kptbtn.Name = "kptbtn";
            kptbtn.Size = new Size(25, 29);
            kptbtn.TabIndex = 13;
            kptbtn.Text = "X";
            kptbtn.UseVisualStyleBackColor = true;
            kptbtn.Click += kptbtn_Click;
            // 
            // kucultbtn
            // 
            kucultbtn.Location = new Point(414, -1);
            kucultbtn.Name = "kucultbtn";
            kucultbtn.Size = new Size(25, 29);
            kucultbtn.TabIndex = 14;
            kucultbtn.Text = "--";
            kucultbtn.UseVisualStyleBackColor = true;
            kucultbtn.Click += kucultbtn_Click;
            // 
            // cercevepnl
            // 
            cercevepnl.Location = new Point(3, 3);
            cercevepnl.Name = "cercevepnl";
            cercevepnl.Size = new Size(416, 25);
            cercevepnl.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(471, 593);
            Controls.Add(cercevepnl);
            Controls.Add(kucultbtn);
            Controls.Add(kptbtn);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(hglb);
            Controls.Add(loginbtn);
            Controls.Add(sifretb);
            Controls.Add(kullanıcıtb);
            Controls.Add(rgblbl);
            ForeColor = Color.FromArgb(34, 36, 49);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Admin Panel";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label rgblbl;
        private TextBox kullanıcıtb;
        private TextBox sifretb;
        private Button loginbtn;
        private System.Windows.Forms.Timer rgbtimer;
        private Label hglb;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Panel panel2;
        private Panel panel3;
        private Button kptbtn;
        private Button kucultbtn;
        private Panel cercevepnl;
    }
}
