namespace Server_Arayüzlü
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.receiveTxtBox = new System.Windows.Forms.RichTextBox();
            this.messageTxtBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.s = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.messageToTxtBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.connectBtn.Location = new System.Drawing.Point(12, 652);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(178, 63);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Start Server";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sendBtn.Location = new System.Drawing.Point(689, 652);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(189, 63);
            this.sendBtn.TabIndex = 1;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // receiveTxtBox
            // 
            this.receiveTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.receiveTxtBox.Location = new System.Drawing.Point(12, 46);
            this.receiveTxtBox.Name = "receiveTxtBox";
            this.receiveTxtBox.ReadOnly = true;
            this.receiveTxtBox.Size = new System.Drawing.Size(866, 351);
            this.receiveTxtBox.TabIndex = 2;
            this.receiveTxtBox.Text = "";
            // 
            // messageTxtBox
            // 
            this.messageTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.messageTxtBox.Location = new System.Drawing.Point(12, 536);
            this.messageTxtBox.Name = "messageTxtBox";
            this.messageTxtBox.Size = new System.Drawing.Size(866, 74);
            this.messageTxtBox.TabIndex = 3;
            this.messageTxtBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Messages:";
            // 
            // s
            // 
            this.s.AutoSize = true;
            this.s.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.s.Location = new System.Drawing.Point(12, 508);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(100, 25);
            this.s.TabIndex = 5;
            this.s.Text = "Message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Send Message TO:";
            // 
            // messageToTxtBox
            // 
            this.messageToTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.messageToTxtBox.Location = new System.Drawing.Point(12, 452);
            this.messageToTxtBox.Name = "messageToTxtBox";
            this.messageToTxtBox.Size = new System.Drawing.Size(866, 53);
            this.messageToTxtBox.TabIndex = 6;
            this.messageToTxtBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 729);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.messageToTxtBox);
            this.Controls.Add(this.s);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messageTxtBox);
            this.Controls.Add(this.receiveTxtBox);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.connectBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox messageTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.RichTextBox receiveTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox messageToTxtBox;
    }
}

