using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Arayüzlü
{
    internal class MessageControl
    {

        public static string ReceiveMessage(SslStream stream, int count)
        {
            try
            {
                string fileName = " ";
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
                if (count == 1)
                {
                    int file = stream.ReadByte();
                    if (file == 1)
                        fileName = FileControl.ReceiveFile(stream, Encoding.UTF8.GetString(mesajBytes));
                    return Encoding.UTF8.GetString(mesajBytes) + "|" + fileName;
                }

                return Encoding.UTF8.GetString(mesajBytes);
            }
            catch (Exception ex)
            {
                Form1.Instance?.AppendTextSafe("Mesaj alma hatası: " + ex.Message);
                return null;
            }
        }
        public static void SendMessage(SslStream stream, string messageBox, string receiverName, string fileName)
        {
            try
            {
                byte fileByte = string.IsNullOrEmpty(fileName) ? (byte)0 : (byte)1;
                byte[] aliciBytes = Encoding.UTF8.GetBytes(receiverName);
                byte[] mesajBytes = Encoding.UTF8.GetBytes(messageBox);

                int mesajUzunlugu = mesajBytes.Length;
                int aliciUzunlugu = aliciBytes.Length;

                byte[] mesajUzunlukBytes = BitConverter.GetBytes(mesajUzunlugu);
                byte[] aliciUzunlukBytes = BitConverter.GetBytes(aliciUzunlugu);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(mesajUzunlukBytes);
                    Array.Reverse(aliciUzunlukBytes);
                }

                byte[] gonderilecek = new byte[9 + mesajUzunlugu + aliciUzunlugu];

                Array.Copy(mesajUzunlukBytes, 0, gonderilecek, 0, 4);
                Array.Copy(mesajBytes, 0, gonderilecek, 4, mesajUzunlugu);
                Array.Copy(aliciUzunlukBytes, 0, gonderilecek, 4 + mesajUzunlugu, 4);
                Array.Copy(aliciBytes, 0, gonderilecek, 8 + mesajUzunlugu, aliciUzunlugu);
                gonderilecek[gonderilecek.Length - 1] = (byte)fileByte;
                stream.Write(gonderilecek);
                Form1.Instance?.AppendTextSafe("Sen: " + messageBox);
                if(fileByte == 1) {
                    FileControl.SendFile(stream, fileName);
                    Form1.Instance?.AppendTextSafe("File Gönderidi");
                }
                    
            }
            catch (Exception ex)
            {
                Form1.Instance?.AppendTextSafe("Alıcı Gönderme hatası: " + ex.Message);
            }

        }
    }
}
