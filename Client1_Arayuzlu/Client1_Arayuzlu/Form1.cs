using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client1_Arayuzlu
{
    public partial class Form1 : Form
    {
        static TcpClient tcpClient;
        SslStream sslStream;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient("sizinSiteniz.com", XXXX); //Buraya Sizin SSL'li web siteniz gelmeli
                sslStream = new SslStream(
                    tcpClient.GetStream(),
                    false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate),
                    null);
                sslStream.AuthenticateAsClient("sizinSiteniz.com"); // Sertifika CN ile aynı olmalı

                AppendTextSafe("Sunucuya güvenli bağlantı sağlandı.");

                Task.Run(() => HoldClient(tcpClient, sslStream));
            }
            catch (Exception ex)
            {
                AppendTextSafe("Sunucuya bağlanılamadı: " + ex.Message);
            }
        }
        // Sertifika doğrulaması (şu anlık her şeyi kabul ediyoruz)
        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        void HoldClient(TcpClient tcpClient, SslStream sslStream)
        {
            string gelenMesaj = "";
            int count = 0;
            while (true)
            {
                if (!IsConnected(tcpClient))
                {
                    AppendTextSafe("Bağlantı koptu!");
                    break;
                }

                try
                {
                    string gelen = ReceiveMessage(sslStream);

                    if (gelen == null)
                        break;

                    if (count == 0)
                    {
                        gelenMesaj = gelen;
                        count++;
                    }
                    else if (count == 1)
                    {
                        AppendTextSafe($"{gelen.ToUpper()} dedi ki: {gelenMesaj}");
                        count = 0;
                    }

                }
                catch (Exception ex)
                {
                    AppendTextSafe("İletişim hatası: " + ex.Message);
                }
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
        string ReceiveMessage(SslStream stream)
        {
            try
            {
                byte[] uzunlukBytes = new byte[4];
                stream.Read(uzunlukBytes, 0, 4);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(uzunlukBytes);

                int uzunluk = BitConverter.ToInt32(uzunlukBytes, 0);
                byte[] mesajBytes = new byte[uzunluk];
                int toplamOkunan = 0;

                while (toplamOkunan < uzunluk)
                {
                    int okunan = stream.Read(mesajBytes, toplamOkunan, uzunluk - toplamOkunan);
                    if (okunan == 0) break;
                    toplamOkunan += okunan;
                }

                return Encoding.UTF8.GetString(mesajBytes);
            }
            catch (Exception ex)
            {
                AppendTextSafe("Mesaj alma hatası: " + ex.Message);
                return null;
            }
        }

        void SendMessage(SslStream stream)
        {
            if (receiverNameTxtBox.Enabled == true)
            {
                if (string.IsNullOrEmpty(receiverNameTxtBox.Text))
                {
                    AppendTextSafe("Lütfen alıcı giriniz!");
                }
                else
                {
                    try
                    {
                        string alici = receiverNameTxtBox.Text;
                        string mesaj = sendTxtBox.Text;

                        byte[] aliciBytes = Encoding.UTF8.GetBytes(alici);
                        byte[] mesajBytes = Encoding.UTF8.GetBytes(mesaj);
                        int mesajUzunlugu = mesajBytes.Length;
                        int aliciUzunlugu = aliciBytes.Length;
                        byte[] mesajUzunlukBytes = BitConverter.GetBytes(mesajUzunlugu);
                        byte[] aliciUzunlukBytes = BitConverter.GetBytes(aliciUzunlugu);
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(mesajUzunlukBytes);
                            Array.Reverse(aliciUzunlukBytes);
                        }

                        byte[] gonderilecek = new byte[8 + mesajUzunlugu + aliciUzunlugu];
                        Array.Copy(mesajUzunlukBytes, 0, gonderilecek, 0, 4);
                        Array.Copy(mesajBytes, 0, gonderilecek, 4, mesajUzunlugu);
                        Array.Copy(aliciUzunlukBytes, 0, gonderilecek, 4 + mesajUzunlugu, 4);
                        Array.Copy(aliciBytes, 0, gonderilecek, 8 + mesajUzunlugu, aliciUzunlugu);

                        stream.Write(gonderilecek);
                        AppendTextSafe("Sen: " + mesaj);
                    }
                    catch (Exception ex)
                    {
                        AppendTextSafe("Alıcı Gönderme hatası: " + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    string mesaj = sendTxtBox.Text;

                    byte[] mesajBytes = Encoding.UTF8.GetBytes(mesaj);
                    int mesajUzunlugu = mesajBytes.Length;
                    byte[] mesajUzunlukBytes = BitConverter.GetBytes(mesajUzunlugu);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(mesajUzunlukBytes);
                    }

                    byte[] gonderilecek = new byte[4 + mesajUzunlugu];
                    Array.Copy(mesajUzunlukBytes, 0, gonderilecek, 0, 4);
                    Array.Copy(mesajBytes, 0, gonderilecek, 4, mesajUzunlugu);

                    stream.Write(gonderilecek);
                }
                catch (Exception ex)
                {
                    AppendTextSafe("Alıcı Gönderme hatası: " + ex.Message);
                }
                receiverNameTxtBox.Enabled = true;
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

        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMessage(sslStream);
        }
    }
}
