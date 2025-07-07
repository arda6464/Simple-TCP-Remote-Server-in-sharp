using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adminpanel
{
    public partial class modal : Form
    {
        public modal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Size = new Size(250, 100);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;

            Label mesaj = new Label();
            mesaj.Text = "✔ İşlem Başarılı";
            mesaj.ForeColor = Color.Lime;
            mesaj.Font = new Font("Consolas", 10, FontStyle.Bold);
            mesaj.TextAlign = ContentAlignment.MiddleCenter;
            mesaj.Dock = DockStyle.Fill;

            this.Controls.Add(mesaj);
        }

        private void modal_Load(object sender, EventArgs e)
        {

        }
    }
}
