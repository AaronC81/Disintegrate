namespace Disintegrate.UI
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.gameTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.websiteLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = global::Disintegrate.UI.Properties.Resources.Logo;
            this.logoPictureBox.Location = new System.Drawing.Point(13, 13);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(259, 50);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 2;
            this.logoPictureBox.TabStop = false;
            // 
            // gameTableLayout
            // 
            this.gameTableLayout.ColumnCount = 1;
            this.gameTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gameTableLayout.Location = new System.Drawing.Point(13, 69);
            this.gameTableLayout.Name = "gameTableLayout";
            this.gameTableLayout.RowCount = 1;
            this.gameTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gameTableLayout.Size = new System.Drawing.Size(259, 281);
            this.gameTableLayout.TabIndex = 3;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(13, 353);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(66, 13);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "Version ###";
            // 
            // websiteLink
            // 
            this.websiteLink.AutoSize = true;
            this.websiteLink.Location = new System.Drawing.Point(226, 353);
            this.websiteLink.Name = "websiteLink";
            this.websiteLink.Size = new System.Drawing.Size(46, 13);
            this.websiteLink.TabIndex = 5;
            this.websiteLink.TabStop = true;
            this.websiteLink.Text = "disint.cc";
            this.websiteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.websiteLink_LinkClicked);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 377);
            this.Controls.Add(this.websiteLink);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.gameTableLayout);
            this.Controls.Add(this.logoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Disintegrate";
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.TableLayoutPanel gameTableLayout;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.LinkLabel websiteLink;
    }
}

