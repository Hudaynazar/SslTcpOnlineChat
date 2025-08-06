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
        public static Form1 Instance;
        public Form1()
        {
            InitializeComponent(); 
            Instance = this;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            Task.Run(() => Connection.StartServer());
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
        private void sendBtn_Click(object sender, EventArgs e)
        {
            string hedef = this.messageToTxtBox.Text.Trim();

            lock (Connection.clientLock)
            {
                if (Connection.GetStreams().ContainsKey(hedef))
                {
                    string mesaj =this.messageTxtBox.Text;
                    MessageControl.SendMessage(Connection.GetStreams()[hedef], mesaj, "Server", "");
                }
                else
                {
                   AppendTextSafe("Bu isimde istemci yok");
                }
            }
        }
    }
}
