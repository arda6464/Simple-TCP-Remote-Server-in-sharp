namespace adminpanel
{
    partial class Dosyayoneticisi
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
            dosyalb = new ListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            kopyalaToolStripMenuItem = new ToolStripMenuItem();
            kopyalamenu = new ContextMenuStrip(components);
            kopyalaToolStripMenuItem1 = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            kopyalamenu.SuspendLayout();
            SuspendLayout();
            // 
            // dosyalb
            // 
            dosyalb.FormattingEnabled = true;
            dosyalb.Location = new Point(42, 36);
            dosyalb.Name = "dosyalb";
            dosyalb.Size = new Size(710, 404);
            dosyalb.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { kopyalaToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(133, 28);
            // 
            // kopyalaToolStripMenuItem
            // 
            kopyalaToolStripMenuItem.Name = "kopyalaToolStripMenuItem";
            kopyalaToolStripMenuItem.Size = new Size(132, 24);
            kopyalaToolStripMenuItem.Text = "Kopyala";
            // 
            // kopyalamenu
            // 
            kopyalamenu.ImageScalingSize = new Size(20, 20);
            kopyalamenu.Items.AddRange(new ToolStripItem[] { kopyalaToolStripMenuItem1 });
            kopyalamenu.Name = "kopyalamenu";
            kopyalamenu.Size = new Size(133, 28);
            // 
            // kopyalaToolStripMenuItem1
            // 
            kopyalaToolStripMenuItem1.Name = "kopyalaToolStripMenuItem1";
            kopyalaToolStripMenuItem1.Size = new Size(210, 24);
            kopyalaToolStripMenuItem1.Text = "Kopyala";
            kopyalaToolStripMenuItem1.Click += kopyalaToolStripMenuItem1_Click;
            // 
            // Dosyayoneticisi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ContextMenuStrip = kopyalamenu;
            Controls.Add(dosyalb);
            Name = "Dosyayoneticisi";
            Text = "Dosyayoneticisi";
            Load += Dosyayoneticisi_Load;
            contextMenuStrip1.ResumeLayout(false);
            kopyalamenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox dosyalb;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem kopyalaToolStripMenuItem;
        private ContextMenuStrip kopyalamenu;
        private ToolStripMenuItem kopyalaToolStripMenuItem1;
    }
}