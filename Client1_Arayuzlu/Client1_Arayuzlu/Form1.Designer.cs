namespace Client1_Arayuzlu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.receiveTxtBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendTxtBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.receiverNameTxtBox = new System.Windows.Forms.RichTextBox();
            this.fileBtn = new System.Windows.Forms.Button();
            this.fileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // receiveTxtBox
            // 
            this.receiveTxtBox.BackColor = System.Drawing.Color.Silver;
            this.receiveTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.receiveTxtBox.Location = new System.Drawing.Point(12, 37);
            this.receiveTxtBox.Name = "receiveTxtBox";
            this.receiveTxtBox.ReadOnly = true;
            this.receiveTxtBox.Size = new System.Drawing.Size(803, 241);
            this.receiveTxtBox.TabIndex = 100;
            this.receiveTxtBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Received Messages";
            // 
            // sendTxtBox
            // 
            this.sendTxtBox.BackColor = System.Drawing.Color.Gainsboro;
            this.sendTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sendTxtBox.Location = new System.Drawing.Point(12, 464);
            this.sendTxtBox.Name = "sendTxtBox";
            this.sendTxtBox.Size = new System.Drawing.Size(803, 79);
            this.sendTxtBox.TabIndex = 2;
            this.sendTxtBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Message";
            // 
            // sendButton
            // 
            this.sendButton.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sendButton.Location = new System.Drawing.Point(644, 564);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(171, 57);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "SEND";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(12, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Send Message TO";
            // 
            // receiverNameTxtBox
            // 
            this.receiverNameTxtBox.BackColor = System.Drawing.Color.Gainsboro;
            this.receiverNameTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.receiverNameTxtBox.Location = new System.Drawing.Point(12, 342);
            this.receiverNameTxtBox.Name = "receiverNameTxtBox";
            this.receiverNameTxtBox.Size = new System.Drawing.Size(803, 63);
            this.receiverNameTxtBox.TabIndex = 1;
            this.receiverNameTxtBox.Text = "";
            // 
            // fileBtn
            // 
            this.fileBtn.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.fileBtn.Location = new System.Drawing.Point(12, 564);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(171, 57);
            this.fileBtn.TabIndex = 3;
            this.fileBtn.Text = "FİLE";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileName
            // 
            this.fileName.AutoSize = true;
            this.fileName.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.fileName.Location = new System.Drawing.Point(189, 578);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(0, 17);
            this.fileName.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(827, 633);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.fileBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.receiverNameTxtBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sendTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.receiveTxtBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox receiveTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox sendTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox receiverNameTxtBox;
        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.Label fileName;
    }
}

