using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client1_Arayuzlu
{
    internal class Connection
    {
        private static TaskCompletionSource<string> nameResponceTsc;
        private static TcpClient tcpClient;
        public static SslStream sslStream;
        private static string gelenMesaj;


        private static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public static Task<string> WaitForNameResponseAsync()
        {
            nameResponceTsc = new TaskCompletionSource<string>();
            return nameResponceTsc.Task;
        }
        static public void ConnectClient(){
            try
            {
                tcpClient = new TcpClient("ornekSite.com", XXXX); // "ornekSite.com" yerine kendi gönderdiğin siteyi yaz, XXXX yerine izin verdiğin portu yaz
                sslStream = new SslStream(
                    tcpClient.GetStream(),
                    false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate),
                    null);

                sslStream.AuthenticateAsClient("ornekSite.com"); // Sertifika CN ile aynı olmalı

                Form1.Instance?.AppendTextSafe("Sunucuya güvenli bağlantı sağlandı.");

                Task.Run(() => HoldClient(tcpClient, sslStream));
                

            }
            catch (Exception ex)
            {
                Form1.Instance?.AppendTextSafe("Sunucuya bağlanılamadı: " + ex.Message);
            }

        }

        private static async void HoldClient(TcpClient tcpClient, SslStream sslStream)
        {
            
            
            int count = 0;
            while (true)
            {
                if (!IsConnected(tcpClient))
                {
                    ConnectForm connectForm = new ConnectForm();
                    connectForm.Show();
                    Form1 form = new Form1();
                    form.Hide();
                    break;
                }

                try
                {
                    string gelen = MessageControl.ReceiveMessage(sslStream, count);
                    
                    if (gelen == null)
                        break;

                    if (count == 0)
                    {
                        gelenMesaj = gelen;
                        nameResponceTsc?.TrySetResult(gelenMesaj);
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
        public static string ControlName()
        {
            return gelenMesaj;
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
