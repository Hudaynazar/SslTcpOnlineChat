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
        public static Form1 Instance;

        public Form1()
        {
            InitializeComponent();
            Instance = this;
        }

        public void AppendTextSafe(string text)
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

        private void sendButton_Click(object sender, EventArgs e)
        {
            MessageControl.SendMessage(Connection.sslStream, sendTxtBox.Text, receiverNameTxtBox.Text, fileName.Text);
            fileName.Text = "";
            sendTxtBox.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Bir dosya seçiniz";
            ofd.Filter = "Tüm Dosyalar| *.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName.Text = ofd.FileName;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }
    }
}
