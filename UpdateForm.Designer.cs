
namespace FallPresence
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.picboxButtonUpdate = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxButtonUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // picboxButtonUpdate
            // 
            this.picboxButtonUpdate.BackColor = System.Drawing.Color.Transparent;
            this.picboxButtonUpdate.BackgroundImage = global::FallPresence.Properties.Resources.btnupdate;
            this.picboxButtonUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picboxButtonUpdate.Location = new System.Drawing.Point(8, 117);
            this.picboxButtonUpdate.Name = "picboxButtonUpdate";
            this.picboxButtonUpdate.Size = new System.Drawing.Size(148, 20);
            this.picboxButtonUpdate.TabIndex = 3;
            this.picboxButtonUpdate.TabStop = false;
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FallPresence.Properties.Resources.fallpresence_winform_updatebg;
            this.ClientSize = new System.Drawing.Size(164, 141);
            this.Controls.Add(this.picboxButtonUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateForm";
            this.Text = "FallPresence";
            ((System.ComponentModel.ISupportInitialize)(this.picboxButtonUpdate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picboxButtonUpdate;
    }
}