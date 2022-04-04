namespace net
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.connect = new System.Windows.Forms.Button();
            this.mytext = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.originIdiom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gender = new System.Windows.Forms.ComboBox();
            this.nickName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.disconnect = new System.Windows.Forms.Button();
            this.chatText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.audioInputs = new System.Windows.Forms.ComboBox();
            this.talk = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.hearOwnAudio = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(12, 226);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(118, 29);
            this.connect.TabIndex = 0;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // mytext
            // 
            this.mytext.AcceptsReturn = true;
            this.mytext.Location = new System.Drawing.Point(336, 65);
            this.mytext.Multiline = true;
            this.mytext.Name = "mytext";
            this.mytext.ReadOnly = true;
            this.mytext.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mytext.Size = new System.Drawing.Size(791, 216);
            this.mytext.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // originIdiom
            // 
            this.originIdiom.FormattingEnabled = true;
            this.originIdiom.Location = new System.Drawing.Point(168, 62);
            this.originIdiom.Name = "originIdiom";
            this.originIdiom.Size = new System.Drawing.Size(151, 28);
            this.originIdiom.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Your Language";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Desired voice gender";
            // 
            // gender
            // 
            this.gender.FormattingEnabled = true;
            this.gender.Items.AddRange(new object[] {
            "male",
            "female"});
            this.gender.Location = new System.Drawing.Point(168, 96);
            this.gender.Name = "gender";
            this.gender.Size = new System.Drawing.Size(151, 28);
            this.gender.TabIndex = 4;
            // 
            // nickName
            // 
            this.nickName.Location = new System.Drawing.Point(168, 29);
            this.nickName.Name = "nickName";
            this.nickName.Size = new System.Drawing.Size(151, 27);
            this.nickName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Your Name";
            // 
            // disconnect
            // 
            this.disconnect.Enabled = false;
            this.disconnect.Location = new System.Drawing.Point(12, 272);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(118, 29);
            this.disconnect.TabIndex = 8;
            this.disconnect.Text = "Disconnect";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // chatText
            // 
            this.chatText.AcceptsReturn = true;
            this.chatText.Location = new System.Drawing.Point(336, 316);
            this.chatText.Multiline = true;
            this.chatText.Name = "chatText";
            this.chatText.ReadOnly = true;
            this.chatText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.chatText.Size = new System.Drawing.Size(791, 216);
            this.chatText.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Your recognition result";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Text Chat";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Audio input";
            // 
            // audioInputs
            // 
            this.audioInputs.FormattingEnabled = true;
            this.audioInputs.Location = new System.Drawing.Point(168, 130);
            this.audioInputs.Name = "audioInputs";
            this.audioInputs.Size = new System.Drawing.Size(151, 28);
            this.audioInputs.TabIndex = 12;
            // 
            // talk
            // 
            this.talk.Enabled = false;
            this.talk.Location = new System.Drawing.Point(201, 226);
            this.talk.Name = "talk";
            this.talk.Size = new System.Drawing.Size(118, 29);
            this.talk.TabIndex = 14;
            this.talk.Text = "Talk";
            this.talk.UseVisualStyleBackColor = true;
            this.talk.Click += new System.EventHandler(this.talk_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Hear Own Audio";
            // 
            // hearOwnAudio
            // 
            this.hearOwnAudio.AutoSize = true;
            this.hearOwnAudio.Location = new System.Drawing.Point(169, 169);
            this.hearOwnAudio.Name = "hearOwnAudio";
            this.hearOwnAudio.Size = new System.Drawing.Size(18, 17);
            this.hearOwnAudio.TabIndex = 16;
            this.hearOwnAudio.UseVisualStyleBackColor = true;
            this.hearOwnAudio.CheckedChanged += new System.EventHandler(this.hearOwnAudio_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 558);
            this.Controls.Add(this.hearOwnAudio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.talk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.audioInputs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chatText);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nickName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gender);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.originIdiom);
            this.Controls.Add(this.mytext);
            this.Controls.Add(this.connect);
            this.Name = "Form1";
            this.Text = "Mulilanguage chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.TextBox mytext;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ComboBox originIdiom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox gender;
        private System.Windows.Forms.TextBox nickName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.TextBox chatText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox audioInputs;
        private System.Windows.Forms.Button talk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox hearOwnAudio;
    }
}

