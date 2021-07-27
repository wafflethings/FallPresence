
namespace FallPresence
{
    partial class FallPresence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FallPresence));
            this.username = new System.Windows.Forms.TextBox();
            this.picboxButtonLog = new System.Windows.Forms.PictureBox();
            this.picboxButtonStart = new System.Windows.Forms.PictureBox();
            this.tutorialPicbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxButtonLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxButtonStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tutorialPicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.username.Font = new System.Drawing.Font("Asap", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.Location = new System.Drawing.Point(13, 57);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(140, 13);
            this.username.TabIndex = 0;
            this.username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // picboxButtonLog
            // 
            this.picboxButtonLog.BackColor = System.Drawing.Color.Transparent;
            this.picboxButtonLog.Image = ((System.Drawing.Image)(resources.GetObject("picboxButtonLog.Image")));
            this.picboxButtonLog.Location = new System.Drawing.Point(9, 88);
            this.picboxButtonLog.Name = "picboxButtonLog";
            this.picboxButtonLog.Size = new System.Drawing.Size(148, 20);
            this.picboxButtonLog.TabIndex = 1;
            this.picboxButtonLog.TabStop = false;
            // 
            // picboxButtonStart
            // 
            this.picboxButtonStart.BackColor = System.Drawing.Color.Transparent;
            this.picboxButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("picboxButtonStart.Image")));
            this.picboxButtonStart.Location = new System.Drawing.Point(10, 115);
            this.picboxButtonStart.Name = "picboxButtonStart";
            this.picboxButtonStart.Size = new System.Drawing.Size(148, 20);
            this.picboxButtonStart.TabIndex = 2;
            this.picboxButtonStart.TabStop = false;
            // 
            // tutorialPicbox
            // 
            this.tutorialPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tutorialPicbox.BackgroundImage")));
            this.tutorialPicbox.Location = new System.Drawing.Point(108, 75);
            this.tutorialPicbox.Name = "tutorialPicbox";
            this.tutorialPicbox.Size = new System.Drawing.Size(44, 12);
            this.tutorialPicbox.TabIndex = 3;
            this.tutorialPicbox.TabStop = false;
            // 
            // FallPresence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(164, 141);
            this.Controls.Add(this.tutorialPicbox);
            this.Controls.Add(this.picboxButtonStart);
            this.Controls.Add(this.picboxButtonLog);
            this.Controls.Add(this.username);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FallPresence";
            this.Text = "FallPresence";
            ((System.ComponentModel.ISupportInitialize)(this.picboxButtonLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxButtonStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tutorialPicbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.PictureBox picboxButtonLog;
        private System.Windows.Forms.PictureBox picboxButtonStart;
        private System.Windows.Forms.PictureBox tutorialPicbox;
    }
}

