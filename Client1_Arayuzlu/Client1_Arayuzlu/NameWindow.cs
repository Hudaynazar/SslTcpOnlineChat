using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client1_Arayuzlu
{
    public partial class NameWindow : Form
    {
        public NameWindow()
        {
            InitializeComponent();
        }

        private async void applyBtn_Click(object sender, EventArgs e)
        {

            MessageControl.SendMessageName(Connection.sslStream, nameTxtBox.Text);

            string result = await Connection.WaitForNameResponseAsync();

            if (!result.Equals("Lütfen Adınızı Giriniz!")) {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bu isim başka kullanıcı tarafından alınmıştır!\nLütfen başka isim deneyin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
