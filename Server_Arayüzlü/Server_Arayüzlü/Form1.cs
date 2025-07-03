using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Arayüzlü
{
    public partial class Form1 : Form
    {

        static Dictionary<string, SslStream> clientStreams = new Dictionary<string, SslStream>();
        static object clientLock = new object();

        public Form1()
        {
            InitializeComponent();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            Task.Run(() => StartServer());
        }
        void StartServer()
        {
            IPAddress ip = IPAddress.Any;
            int port = XXXX; //Burada İzin verdiğiniz port olmalıdır

            try
            {
                TcpListener server = new TcpListener(ip, port);
                server.Start();
                AppendTextSafe("Sunucu başlatıldı. Bağlantı bekleniyor...");

                //Task.Run(() => ServerConsoleInput());

                X509Certificate2 certificate = new X509Certificate2("cert.pfx", "Şifreniz"); //burada self-signed ssl şifreniz

                while (true)
                {
                    TcpClient client = null;
                    try
                    {
                        client = server.AcceptTcpClient();

                        SslStream sslStream = new SslStream(client.GetStream(), false);
                        sslStream.AuthenticateAsServer(certificate, false, false);

                        SendMessage(sslStream, "Lütfen Adınızı Giriniz!", "Server");
                        string clientName = ReceiveMessage(sslStream);
                        lock (clientLock)
                        {
                            clientStreams.Add(clientName, sslStream);
                        }

                        AppendTextSafe($"{clientName} bağlandı.");

                        Task.Run(() => HandleClient(client, sslStream, clientName));
                    }
                    catch (Exception ex)
                    {
                        AppendTextSafe("İstemci Bağlanamadı: " + ex.ToString());
                        client.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                AppendTextSafe("Sunucu Başlatılamadı: " + ex.ToString());
            }
        } 
        
        void AcceptClients(TcpListener server, X509Certificate2 certificate)
        {
            
        }

        void HandleClient(TcpClient client, SslStream sslStream, string clientName)
        {
            try
            {
                string gelenMesaj = "";
                int count = 0;
                while (true)
                {
                    if (!IsConnected(client))
                    {
                       AppendTextSafe(" Bağlantı Koptu");
                        break;
                    }

                    string gelen = ReceiveMessage(sslStream);
                    if (gelen == null)
                        break;

                    if (count == 0)
                    {
                        gelenMesaj = gelen;
                        AppendTextSafe($"{clientName} dedi ki: {gelen}");
                        count++;
                    }
                    else if (count == 1)
                    {
                        if (clientStreams.ContainsKey(gelen))
                        {
                            SendMessage(clientStreams[gelen], gelenMesaj, clientName);
                            count = 0;
                        }
                        else
                        {
                            AppendTextSafe("Mesaj Gönderilemedi! Alıcı bulunamadı!");
                            SendMessage(clientStreams[clientName], "Alıcı bulunamadı", "Server");
                            count = 0;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
               AppendTextSafe("Veri Alınamadı: " + ex.Message);
            }
            finally
            {
                lock (clientLock)
                {
                    clientStreams.Remove(clientName);
                }
                client.Close();
               AppendTextSafe($"{clientName} bağlantısı kapandı.");
            }
        }

        string ReceiveMessage(SslStream sslStream)
        {
            
            try
            {
                byte[] uzunlukBytes = new byte[4];
                sslStream.Read(uzunlukBytes, 0, 4);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(uzunlukBytes);

                int uzunluk = BitConverter.ToInt32(uzunlukBytes, 0);

                byte[] messageByte = new byte[uzunluk];
                int toplamOkunan = 0;

                while (toplamOkunan < uzunluk)
                {
                    int okunan = sslStream.Read(messageByte, toplamOkunan, uzunluk - toplamOkunan);
                    if (okunan == 0)
                        break;
                    toplamOkunan += okunan;
                }
                return Encoding.UTF8.GetString(messageByte);
            }
            catch (Exception ex)
            {
               AppendTextSafe("Mesaj alma hatası: " + ex.ToString());
                return null;
            }
        }

        void SendMessage(SslStream sslStream, string message, string gonderen)
        {
            try
            {
                byte[] mesajBytes = Encoding.UTF8.GetBytes(message);
                byte[] gonderenByte  = Encoding.UTF8.GetBytes(gonderen);
                int gonderenUzunulugu = gonderenByte.Length;
                int mesajUzunlugu = mesajBytes.Length;

                byte[] gonderenUzunlukBytes = BitConverter.GetBytes(gonderenUzunulugu);
                byte[] uzunlukBytes = BitConverter.GetBytes(mesajUzunlugu);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(uzunlukBytes);
                    Array.Reverse(gonderenUzunlukBytes);
                }

                byte[] gonderilecek = new byte[uzunlukBytes.Length + mesajUzunlugu + gonderenUzunlukBytes.Length + gonderenUzunulugu];
                Array.Copy(uzunlukBytes, 0, gonderilecek, 0, 4);
                Array.Copy(mesajBytes, 0, gonderilecek, 4, mesajUzunlugu);
                Array.Copy(gonderenUzunlukBytes, 0, gonderilecek, 4 + mesajUzunlugu, 4);
                Array.Copy(gonderenByte, 0, gonderilecek, 8 + mesajUzunlugu, gonderenUzunulugu);

                sslStream.Write(gonderilecek, 0, gonderilecek.Length);
            }
            catch (Exception ex)
            {
               AppendTextSafe(" Mesaj gönderme hatası: " + ex);
            }
        }

        /*static void ServerConsoleInput()
        {
            while (true)
            {
               AppendTextSafe(" ");"Kime mesaj göndermek istiyorsun? (Mevcutlar: " + string.Join(", ", clientStreams.Keys) + ")";
                string hedef =this.messageToTxtBox.Text.Trim();

                lock (clientLock)
                {
                    if (clientStreams.ContainsKey(hedef))
                    {
                        string mesaj =this.messageTxtBox.Text;
                        SendMessage(clientStreams[hedef], mesaj);
                    }
                    else
                    {
                       AppendTextSafe(" ");"Bu isimde bir istemci yok.";
                    }
                }
            }
        }*/

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
        void AppendTextSafe(string text)
        {
            if (receiveTxtBox.InvokeRequired)
            {
                receiveTxtBox.Invoke(new Action(() => receiveTxtBox.AppendText(text + Environment.NewLine)));
            }
            else
            {
                receiveTxtBox.AppendText(text + Environment.NewLine);
            }
        }
        private void sendBtn_Click(object sender, EventArgs e)
        {
            string hedef = this.messageToTxtBox.Text.Trim();

            lock (clientLock)
            {
                if (clientStreams.ContainsKey(hedef))
                {
                    string mesaj =this.messageTxtBox.Text;
                    SendMessage(clientStreams[hedef], mesaj, hedef);
                }
                else
                {
                   AppendTextSafe("Bu isimde istemci yok");
                }
            }
        }
    }
}
