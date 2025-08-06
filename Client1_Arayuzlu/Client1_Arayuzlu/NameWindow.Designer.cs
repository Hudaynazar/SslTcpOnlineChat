namespace Client1_Arayuzlu
{
    partial class NameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameWindow));
            this.applyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // applyBtn
            // 
            this.applyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.applyBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.applyBtn.Location = new System.Drawing.Point(93, 228);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(150, 57);
            this.applyBtn.TabIndex = 2;
            this.applyBtn.Text = "Save";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(79, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lütfen adınızı giriniz";
            // 
            // nameTxtBox
            // 
            this.nameTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.nameTxtBox.Location = new System.Drawing.Point(64, 150);
            this.nameTxtBox.Name = "nameTxtBox";
            this.nameTxtBox.Size = new System.Drawing.Size(197, 29);
            this.nameTxtBox.TabIndex = 1;
            // 
            // NameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 385);
            this.Controls.Add(this.nameTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.applyBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NameWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NameWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTxtBox;
    }
}