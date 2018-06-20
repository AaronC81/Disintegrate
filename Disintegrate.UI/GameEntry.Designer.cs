namespace Disintegrate.UI
{
    partial class GameEntry
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.configureButton = new System.Windows.Forms.Button();
            this.customizeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.Location = new System.Drawing.Point(3, 30);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(35, 13);
            this.gameNameLabel.TabIndex = 0;
            this.gameNameLabel.Text = "label1";
            // 
            // configureButton
            // 
            this.configureButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configureButton.Location = new System.Drawing.Point(166, 7);
            this.configureButton.Name = "configureButton";
            this.configureButton.Size = new System.Drawing.Size(75, 29);
            this.configureButton.TabIndex = 1;
            this.configureButton.Text = "...";
            this.configureButton.UseVisualStyleBackColor = true;
            // 
            // customizeButton
            // 
            this.customizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customizeButton.Location = new System.Drawing.Point(166, 38);
            this.customizeButton.Name = "customizeButton";
            this.customizeButton.Size = new System.Drawing.Size(75, 29);
            this.customizeButton.TabIndex = 2;
            this.customizeButton.Text = "Customize";
            this.customizeButton.UseVisualStyleBackColor = true;
            // 
            // GameEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customizeButton);
            this.Controls.Add(this.configureButton);
            this.Controls.Add(this.gameNameLabel);
            this.Name = "GameEntry";
            this.Size = new System.Drawing.Size(244, 73);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.Button configureButton;
        private System.Windows.Forms.Button customizeButton;
    }
}
