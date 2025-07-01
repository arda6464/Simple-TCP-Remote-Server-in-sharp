
namespace adminpanel

{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            cercevepnl.MouseDown += new MouseEventHandler(Form_MouseDown);
            cercevepnl.MouseMove += new MouseEventHandler(Form_MouseMove);
            cercevepnl.MouseUp += new MouseEventHandler(Form_MouseUp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // this.BackColor = Color.FromArgb(18, 18, 18); // Arka plan koyu
            rgbtimer.Tick += rgbtimer_Tick;
            hglb.Text = $"Ho�geldiniz {Environment.UserName}.";





            //loginbtn.BackColor = Color.FromArgb(30, 30, 30);
            //loginbtn.ForeColor = Color.Lime;

            // Ortak tasar�m
            // Kapat butonu: canl� mavi, beyaz yaz�
            kptbtn.FlatStyle = FlatStyle.Flat;
            kptbtn.FlatAppearance.BorderSize = 0;
            kptbtn.BackColor = Color.FromArgb(18,18,18); // Cyberpunk hissi verir

            kptbtn.ForeColor = Color.Lime;
            kptbtn.Font = new Font("Arial", 10, FontStyle.Bold);

            // K���lt butonu: koyu gri, ye�il yaz�
            kucultbtn.FlatStyle = FlatStyle.Flat;
            kucultbtn.FlatAppearance.BorderSize = 0;
            kucultbtn.BackColor = Color.FromArgb(18, 18, 18);
            kucultbtn.ForeColor = Color.Lime;
            kucultbtn.Font = new Font("Segoe UI", 10, FontStyle.Bold);



        }
        int r = 255, g = 0, b = 0;
        int step = 5;  // Renk de�i�im h�z�
        int phase = 0; // Hangi renk ge�i�indeyiz

        private void rgbtimer_Tick(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0: // K�rm�z�dan sar�ya (k�rm�z�dan ye�ile art��)
                    g += step;
                    if (g >= 255)
                    {
                        g = 255;
                        phase = 1;
                    }
                    break;

                case 1: // Sar�dan ye�ile (k�rm�z� azal�yor)
                    r -= step;
                    if (r <= 0)
                    {
                        r = 0;
                        phase = 2;
                    }
                    break;

                case 2: // Ye�ilden camg�be�ine (mavi art�yor)
                    b += step;
                    if (b >= 255)
                    {
                        b = 255;
                        phase = 3;
                    }
                    break;

                case 3: // Camg�be�inden maviye (ye�il azal�yor)
                    g -= step;
                    if (g <= 0)
                    {
                        g = 0;
                        phase = 4;
                    }
                    break;

                case 4: // Maviden moruya (k�rm�z� art�yor)
                    r += step;
                    if (r >= 255)
                    {
                        r = 255;
                        phase = 5;
                    }
                    break;

                case 5: // Morudan maviye (mavi azal�yor)
                    b -= step;
                    if (b <= 0)
                    {
                        b = 0;
                        phase = 0;
                    }
                    break;
            }

            rgblbl.ForeColor = Color.FromArgb(r, g, b);
        }



        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (kullan�c�tb.Text == "arda" && sifretb.Text == "arda")
            {
                //MessageBox.Show("giri� ba�ar�l�");
                YonetimPaneli yonetimPaneli = new YonetimPaneli();
                yonetimPaneli.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("yanl�� giri� oe");
            }
        }

        private void kptbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kucultbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void kullan�c�tb_Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Lime;

            panel3.BackColor = Color.White;

        }

        private void sifretb_Click(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Lime;
            panel2.BackColor = Color.White;
        }

        private void loginbtn_MouseEnter(object sender, EventArgs e)
        {
            loginbtn.BackColor = Color.FromArgb(0, 180, 0);  // Koyu ye�il
            loginbtn.ForeColor = Color.Black;
            loginbtn.FlatAppearance.BorderColor = Color.Lime;
        }

        private void loginbtn_MouseLeave(object sender, EventArgs e)
        {
            loginbtn.BackColor = Color.FromArgb(18, 18, 18);    // Arka planla uyumlu
            loginbtn.ForeColor = Color.Lime;
            loginbtn.FlatAppearance.BorderColor = Color.DarkGreen;
        }

    }
}
