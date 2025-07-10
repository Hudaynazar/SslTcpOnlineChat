using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Server_Arayüzlü
{
    internal class MessageControl
    {
        public static string ReceiveMessage(SslStream sslStream)
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
                Form1.Instance?.AppendTextSafe("Mesaj alma hatası: " + ex.ToString());
                return null;
            }
        }

        public static void SendMessage(SslStream sslStream, string message, string gonderen)
        {
            try
            {
                byte[] mesajBytes = Encoding.UTF8.GetBytes(message);
                byte[] gonderenByte = Encoding.UTF8.GetBytes(gonderen);
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
                Form1.Instance?.AppendTextSafe(" Mesaj gönderme hatası: " + ex);
            }
        }
    }
}
