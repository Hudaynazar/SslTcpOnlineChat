using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client1_Arayuzlu
{
    internal class MessageControl
    {
        public static string ReceiveMessage(SslStream stream)
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
                Form1.Instance?.AppendTextSafe("Mesaj alma hatası: " + ex.Message);
                return null;
            }
        }

        public static void SendMessage(SslStream stream)
        {
            if (Form1.Instance?.GetReceiverTextBoxBool() == true)
            {
                if (string.IsNullOrEmpty(Form1.Instance?.GetReceiverTextBox()))
                {
                    Form1.Instance?.AppendTextSafe("Lütfen alıcı giriniz!");
                }
                else
                {
                    try
                    {
                        string alici = Form1.Instance?.GetReceiverTextBox();
                        string mesaj = Form1.Instance?.GetSendTxtBox();

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
                        Form1.Instance?.AppendTextSafe("Sen: " + mesaj);
                    }
                    catch (Exception ex)
                    {
                        Form1.Instance?.AppendTextSafe("Alıcı Gönderme hatası: " + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    string mesaj = Form1.Instance?.GetSendTxtBox();
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
                    Form1.Instance?.AppendTextSafe("Alıcı Gönderme hatası: " + ex.Message);
                }
                Form1.Instance?.SetReceiverTxtBoxBool(true);
            }
        }
    }
}
