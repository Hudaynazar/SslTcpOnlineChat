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
            int port = XXXX; // İZİN VERDİĞİNİZ PORTU
            try
            {
                TcpListener server = new TcpListener(ip, port);
                server.Start();
                Form1.Instance?.AppendTextSafe("Sunucu başlatıldı. Bağlantı bekleniyor...");

                X509Certificate2 certificate = new X509Certificate2("cert.pfx", "ŞİFRENİZ"); // CERT.PFX İN ŞİFRESİ

                while (true)
                {
                    TcpClient client = null;
                    try
                    {
                        client = server.AcceptTcpClient();

                        SslStream sslStream = new SslStream(client.GetStream(), false);
                        sslStream.AuthenticateAsServer(certificate, false, false);

                        MessageControl.SendMessage(sslStream, "Lütfen Adınızı Giriniz!", "Server");
                        string clientName = MessageControl.ReceiveMessage(sslStream);
                        lock (clientLock)
                        {
                            clientStreams.Add(clientName, sslStream);
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

                    string gelen = MessageControl.ReceiveMessage(sslStream);
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
                        if (clientStreams.ContainsKey(gelen))
                        {
                            MessageControl.SendMessage(clientStreams[gelen], gelenMesaj, clientName);
                            count = 0;
                        }
                        else
                        {
                            Form1.Instance?.AppendTextSafe("Mesaj Gönderilemedi! Alıcı bulunamadı!");
                            MessageControl.SendMessage(clientStreams[clientName], "Alıcı bulunamadı", "Server");
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
