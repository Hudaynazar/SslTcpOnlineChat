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

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection.ConnectClient();
        }

        public string GetSendTxtBox()
        {
            return sendTxtBox.Text;
        }
        public string GetReceiverTextBox() 
        { 
            return receiverNameTxtBox.Text;
        }
        public bool GetReceiverTextBoxBool()
        {
            if (receiverNameTxtBox.Enabled)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetReceiverTxtBoxBool(bool enable)
        {
            this.receiverNameTxtBox.Enabled = enable;
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
            MessageControl.SendMessage(Connection.sslStream);
        }
    }
}
