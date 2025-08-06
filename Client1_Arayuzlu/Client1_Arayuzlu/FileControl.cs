using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client1_Arayuzlu
{
    internal class FileControl
    {
        public static void ReceiveFile(SslStream sslStream)
        {
            byte[] headerBuffer = new byte[512];
            int headerBytes = sslStream.Read(headerBuffer, 0, headerBuffer.Length);
            string header = Encoding.UTF8.GetString(headerBuffer, 0, headerBytes);
            string[] split = header.Split('|');
            string fileName = split[0];
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, fileName);
            long fileSize = long.Parse(split[1]);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] buffer = new byte[4096];
                long totalRead = 0;
                int bytesRead;

                while (totalRead < fileSize && (bytesRead = sslStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                    totalRead += bytesRead;
                }
            }

            Form1.Instance?.AppendTextSafe("Geldi file");
        }
        public static void SendFile(SslStream sslStream, string filePath)
        {

            Form1.Instance?.AppendTextSafe("Gidio File");
            FileInfo fileInfo = new FileInfo(filePath);
            string header = fileInfo.Name + "|" + fileInfo.Length;

            byte[] headerBytes = Encoding.UTF8.GetBytes(header);
            sslStream.Write(headerBytes, 0, headerBytes.Length);
            sslStream.Flush();

            byte[] buffer = new byte[4096];
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    sslStream.Write(buffer, 0, bytesRead);
                }
            }

            sslStream.Flush();

            Form1.Instance?.AppendTextSafe("Gitti File");
        }
    }
}
