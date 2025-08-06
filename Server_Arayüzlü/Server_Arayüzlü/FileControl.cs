using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_Arayüzlü
{
    internal class FileControl
    {
        public static string ReceiveFile(SslStream sslStream, string receiver)
        {
            byte[] headerBuffer = new byte[512];
            int heaederBytes = sslStream.Read(headerBuffer, 0, headerBuffer.Length);
            string header = Encoding.UTF8.GetString(headerBuffer,0, heaederBytes);
            string[] split = header.Split('|');
            string fileName = split[0];
            string folderPath = Path.Combine("C:/Users/_Kullanıcı adı_/OneDrive/Masaüstü/GelenDosyalar"); // "_Kullanıcı adı_" yerine kendi pc adını yaz
            string filePath = Path.Combine(folderPath,fileName);
            long fileSize = long.Parse(split[1]);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                byte [] buffer = new byte[4096];
                long totalRead = 0;
                int bytesRead;

                while(totalRead < fileSize && (bytesRead = sslStream.Read(buffer, 0, buffer.Length)) > 0){

                    fs.Write(buffer, 0, bytesRead);
                    totalRead += bytesRead;
                }
            }
            Form1.Instance?.AppendTextSafe("File alındı");
            return fileName;
        }
        public static void SendFile(SslStream sslStream, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string header = fileInfo.Name + "|" + fileInfo.Length;

            byte[] headerBytes = Encoding.UTF8.GetBytes(header);
            sslStream.Write(headerBytes, 0, headerBytes.Length);
            sslStream.Flush();

            byte[] buffer = new byte[4096];
            using(var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    sslStream.Write(buffer, 0, bytesRead);
                }
            }
            sslStream.Flush();
            Form1.Instance?.AppendTextSafe("File gönderildi");
        }
    }
}
