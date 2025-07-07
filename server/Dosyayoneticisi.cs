using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adminpanel
{
    public partial class Dosyayoneticisi : Form
    {
        private TcpClient hedefclient;
        private string clientip;
        private string gelenliste;
        public Dosyayoneticisi(TcpClient client, string ip, string gelenliste)
        {
            InitializeComponent();
            this.hedefclient = client;
            this.clientip = ip;
            this.gelenliste = gelenliste;

            dosyalb.Items.AddRange(gelenliste.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)); // adrange(hepsini bir eklemek)
        }

       


        private void Dosyayoneticisi_Load(object sender, EventArgs e)
        {
            Text = $"Dosya Yöneticisi - {clientip} "; //uygulama pornosu
        }

        private void kopyalaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(dosyalb.SelectedItem != null)
            {
               string sa = dosyalb.SelectedItem.ToString();
               string kk = $"{sa}/";
                KopyalaClipboard(kk);
               MessageBox.Show(kk);

            }
            else
            {
                MessageBox.Show("malmısın amk satır seçsene");
            }
        }
        private void KopyalaClipboard(string metin)
        {
            Thread staThread = new Thread(() =>
            {
                try
                {
                    
                    Clipboard.SetText(metin);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Clipboard hatası: " + ex.Message);
                }
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

    }
}
