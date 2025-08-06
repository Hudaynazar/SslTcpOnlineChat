using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Server_Arayüzlü
{
    internal class Connection
    {
        private static Dictionary<string, SslStream> clientStreams = new Dictionary<string, SslStream>();
        public static object clientLock = new object();

        public static ReadOnlyDictionary<string, SslStream> GetStreams()
        {
            return new ReadOnlyDictionary<string, SslStream>(clientStreams);
        }

        static public void StartServer()
        {
            IPAddress ip = IPAddress.Any;
            int port = XXXX; //Izin verdiğin port
            try
            {
                TcpListener server = new TcpListener(ip, port);
                server.Start();
                Form1.Instance?.AppendTextSafe("Sunucu başlatıldı. Bağlantı bekleniyor...");

                X509Certificate2 certificate = new X509Certificate2("cert.pfx", "XXXXXXXX"); //Sertifikanın parolası

                while (true)
                {
                    TcpClient client = null;
                    try
                    {
                        client = server.AcceptTcpClient();

                        SslStream sslStream = new SslStream(client.GetStream(), false);
                        sslStream.AuthenticateAsServer(certificate, false, false);
                    basadon:
                        MessageControl.SendMessage(sslStream, "Lütfen Adınızı Giriniz!", "Server", "");
                        string clientName = MessageControl.ReceiveMessage(sslStream, 0);

                        lock (clientLock)
                        {
                            if (clientStreams.ContainsKey(clientName))
                            {
                                goto basadon;
                            }
                            else
                            {
                                clientStreams.Add(clientName, sslStream);
                                MessageControl.SendMessage(sslStream, "Artık Adınız " + clientName, "Server", "");
                            }
                        }

                        Form1.Instance?.AppendTextSafe($"{clientName} bağlandı.");

                        Task.Run(() => HandleClient(client, sslStream, clientName));
                    }
                    catch (Exception ex)
                    {
                        Form1.Instance?.AppendTextSafe("İstemci Bağlanamadı: " + ex.ToString());
                        client.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.Instance?.AppendTextSafe("Sunucu Başlatılamadı: " + ex.ToString());
            }
        }
        static public void HandleClient(TcpClient client, SslStream sslStream, string clientName)
        {
            try
            {
                string gelenMesaj = "";
                int count = 0;
                while (true)
                {
                    if (!IsConnected(client))
                    {
                        Form1.Instance?.AppendTextSafe(" Bağlantı Koptu");
                        break;  
                    }

                    string gelen = MessageControl.ReceiveMessage(sslStream, count);

                    if (gelen == null)
                        break;

                    if (count == 0)
                    {
                        gelenMesaj = gelen;
                        Form1.Instance?.AppendTextSafe($"{clientName} dedi ki: {gelen}");
                        count++;
                    }
                    else if (count == 1)
                    {
                        string[] split = gelen.Split('|');
                        if (clientStreams.ContainsKey(split[0]))
                        {
                            if (!string.IsNullOrWhiteSpace(split[1]))
                            {
                                MessageControl.SendMessage(clientStreams[split[0]], gelenMesaj, clientName, "C:/Users/_Kullanıcı adı_/OneDrive/Masaüstü/GelenDosyalar/" + split[1]); // "_Kullanıcı adı_" yerine kendi pc ismini gir. Masaüstünde "GelenDosyalar" diye klasör oluştur
                                count = 0;
                            }
                            else
                            {
                                MessageControl.SendMessage(clientStreams[split[0]], gelenMesaj, clientName, "");
                                count = 0;
                            }
                        }
                        else
                        {
                            Form1.Instance?.AppendTextSafe("Mesaj Gönderilemedi! Alıcı bulunamadı!");
                            MessageControl.SendMessage(clientStreams[clientName], "Alıcı bulunamadı", "Server", "");
                            count = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.Instance?.AppendTextSafe("Veri Alınamadı: " + ex.Message);
            }
            finally
            {
                lock (clientLock)
                {
                    clientStreams.Remove(clientName);
                }
                client.Close();
                Form1.Instance?.AppendTextSafe($"{clientName} bağlantısı kapandı.");
            }
        }

        static bool IsConnected(TcpClient client)
        {
            try
            {
                if (client == null || !client.Connected)
                    return false;

                if (client.Client.Poll(0, SelectMode.SelectRead) && client.Client.Available == 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
