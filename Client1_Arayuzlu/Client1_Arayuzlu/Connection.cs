using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Client1_Arayuzlu
{
    internal class Connection
    {
        private static TcpClient tcpClient;
        public static SslStream sslStream;


        private static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        static public void ConnectClient(){
            try
            {
                tcpClient = new TcpClient("YOURWEBSİTE.COM", XXXX);// BURADA WEB SİTENİZ VE İZİN VERİLEN PORTU YAZACAKSINIZ
                sslStream = new SslStream(
                    tcpClient.GetStream(),
                    false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate),
                    null);
                
                sslStream.AuthenticateAsClient("YOURWEBSİTE.COM"); // Sertifika CN ile aynı olmalı

                Form1.Instance?.AppendTextSafe("Sunucuya güvenli bağlantı sağlandı.");

                Task.Run(() => HoldClient(tcpClient, sslStream));
            }
            catch (Exception ex)
            {
                Form1.Instance?.AppendTextSafe("Sunucuya bağlanılamadı: " + ex.Message);
            }

        }
        private static void HoldClient(TcpClient tcpClient, SslStream sslStream)
        {
            string gelenMesaj = "";
            int count = 0;
            while (true)
            {
                if (!IsConnected(tcpClient))
                {
                    Form1.Instance?.AppendTextSafe("Bağlantı koptu!");
                    break;
                }

                try
                {
                    string gelen = MessageControl.ReceiveMessage(sslStream);

                    if (gelen == null)
                        break;

                    if (count == 0)
                    {
                        gelenMesaj = gelen;
                        count++;
                    }
                    else if (count == 1)
                    {
                        Form1.Instance?.AppendTextSafe($"{gelen.ToUpper()} dedi ki: {gelenMesaj}");
                        count = 0;
                    }

                }
                catch (Exception ex)
                {
                    Form1.Instance?.AppendTextSafe("İletişim hatası: " + ex.Message);
                }
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
