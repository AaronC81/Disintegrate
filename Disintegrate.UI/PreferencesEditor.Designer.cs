namespace Disintegrate.UI
{
    partial class PreferencesEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesEditor));
            this.resetButton = new System.Windows.Forms.Button();
            this.discardButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.lineOneLabel = new System.Windows.Forms.Label();
            this.lineTwoLabel = new System.Windows.Forms.Label();
            this.lineOneTextBox = new System.Windows.Forms.TextBox();
            this.lineTwoTextBox = new System.Windows.Forms.TextBox();
            this.fieldsListBox = new System.Windows.Forms.ListBox();
            this.helpText = new System.Windows.Forms.Label();
            this.previewLabel = new System.Windows.Forms.Label();
            this.previewLineOne = new System.Windows.Forms.Label();
            this.previewLineTwo = new System.Windows.Forms.Label();
            this.iconLabel = new System.Windows.Forms.Label();
            this.iconComboBox = new System.Windows.Forms.ComboBox();
            this.errorText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(458, 12);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // discardButton
            // 
            this.discardButton.Location = new System.Drawing.Point(557, 12);
            this.discardButton.Name = "discardButton";
            this.discardButton.Size = new System.Drawing.Size(75, 23);
            this.discardButton.TabIndex = 1;
            this.discardButton.Text = "Discard";
            this.discardButton.UseVisualStyleBackColor = true;
            this.discardButton.Click += new System.EventHandler(this.discardButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(656, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(12, 12);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(130, 20);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Customising <...>";
            // 
            // lineOneLabel
            // 
            this.lineOneLabel.AutoSize = true;
            this.lineOneLabel.Location = new System.Drawing.Point(13, 58);
            this.lineOneLabel.Name = "lineOneLabel";
            this.lineOneLabel.Size = new System.Drawing.Size(51, 13);
            this.lineOneLabel.TabIndex = 4;
            this.lineOneLabel.Text = "Line one:";
            // 
            // lineTwoLabel
            // 
            this.lineTwoLabel.AutoSize = true;
            this.lineTwoLabel.Location = new System.Drawing.Point(13, 97);
            this.lineTwoLabel.Name = "lineTwoLabel";
            this.lineTwoLabel.Size = new System.Drawing.Size(50, 13);
            this.lineTwoLabel.TabIndex = 5;
            this.lineTwoLabel.Text = "Line two:";
            // 
            // lineOneTextBox
            // 
            this.lineOneTextBox.Location = new System.Drawing.Point(70, 55);
            this.lineOneTextBox.Name = "lineOneTextBox";
            this.lineOneTextBox.Size = new System.Drawing.Size(440, 20);
            this.lineOneTextBox.TabIndex = 6;
            this.lineOneTextBox.TextChanged += new System.EventHandler(this.lineOneTextBox_TextChanged);
            this.lineOneTextBox.Enter += new System.EventHandler(this.lineOneTextBox_Enter);
            // 
            // lineTwoTextBox
            // 
            this.lineTwoTextBox.Location = new System.Drawing.Point(70, 95);
            this.lineTwoTextBox.Name = "lineTwoTextBox";
            this.lineTwoTextBox.Size = new System.Drawing.Size(440, 20);
            this.lineTwoTextBox.TabIndex = 7;
            this.lineTwoTextBox.TextChanged += new System.EventHandler(this.lineTwoTextBox_TextChanged);
            this.lineTwoTextBox.Enter += new System.EventHandler(this.lineTwoTextBox_Enter);
            // 
            // fieldsListBox
            // 
            this.fieldsListBox.FormattingEnabled = true;
            this.fieldsListBox.Location = new System.Drawing.Point(527, 55);
            this.fieldsListBox.Name = "fieldsListBox";
            this.fieldsListBox.Size = new System.Drawing.Size(204, 134);
            this.fieldsListBox.TabIndex = 8;
            this.fieldsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fieldsListBox_MouseDoubleClick);
            // 
            // helpText
            // 
            this.helpText.Location = new System.Drawing.Point(527, 192);
            this.helpText.Name = "helpText";
            this.helpText.Size = new System.Drawing.Size(203, 50);
            this.helpText.TabIndex = 9;
            this.helpText.Text = "To insert a game field into a line, double-click the field\'s name while editing t" +
    "he line.";
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Location = new System.Drawing.Point(16, 134);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(48, 13);
            this.previewLabel.TabIndex = 10;
            this.previewLabel.Text = "Preview:";
            // 
            // previewLineOne
            // 
            this.previewLineOne.AutoSize = true;
            this.previewLineOne.Location = new System.Drawing.Point(67, 134);
            this.previewLineOne.Name = "previewLineOne";
            this.previewLineOne.Size = new System.Drawing.Size(48, 13);
            this.previewLineOne.TabIndex = 11;
            this.previewLineOne.Text = "Line one";
            // 
            // previewLineTwo
            // 
            this.previewLineTwo.AutoSize = true;
            this.previewLineTwo.Location = new System.Drawing.Point(67, 157);
            this.previewLineTwo.Name = "previewLineTwo";
            this.previewLineTwo.Size = new System.Drawing.Size(47, 13);
            this.previewLineTwo.TabIndex = 12;
            this.previewLineTwo.Text = "Line two";
            // 
            // iconLabel
            // 
            this.iconLabel.AutoSize = true;
            this.iconLabel.Location = new System.Drawing.Point(16, 192);
            this.iconLabel.Name = "iconLabel";
            this.iconLabel.Size = new System.Drawing.Size(31, 13);
            this.iconLabel.TabIndex = 13;
            this.iconLabel.Text = "Icon:";
            // 
            // iconComboBox
            // 
            this.iconComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iconComboBox.FormattingEnabled = true;
            this.iconComboBox.Location = new System.Drawing.Point(71, 189);
            this.iconComboBox.Name = "iconComboBox";
            this.iconComboBox.Size = new System.Drawing.Size(121, 21);
            this.iconComboBox.TabIndex = 14;
            this.iconComboBox.SelectedValueChanged += new System.EventHandler(this.iconComboBox_SelectedValueChanged);
            // 
            // errorText
            // 
            this.errorText.AutoSize = true;
            this.errorText.ForeColor = System.Drawing.Color.Red;
            this.errorText.Location = new System.Drawing.Point(15, 219);
            this.errorText.Name = "errorText";
            this.errorText.Size = new System.Drawing.Size(29, 13);
            this.errorText.TabIndex = 15;
            this.errorText.Text = "Error";
            // 
            // PreferencesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 241);
            this.Controls.Add(this.errorText);
            this.Controls.Add(this.iconComboBox);
            this.Controls.Add(this.iconLabel);
            this.Controls.Add(this.previewLineTwo);
            this.Controls.Add(this.previewLineOne);
            this.Controls.Add(this.previewLabel);
            this.Controls.Add(this.helpText);
            this.Controls.Add(this.fieldsListBox);
            this.Controls.Add(this.lineTwoTextBox);
            this.Controls.Add(this.lineOneTextBox);
            this.Controls.Add(this.lineTwoLabel);
            this.Controls.Add(this.lineOneLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.discardButton);
            this.Controls.Add(this.resetButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PreferencesEditor";
            this.Text = "Preferences Editor";
            this.Load += new System.EventHandler(this.PreferencesEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button discardButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label lineOneLabel;
        private System.Windows.Forms.Label lineTwoLabel;
        private System.Windows.Forms.TextBox lineOneTextBox;
        private System.Windows.Forms.TextBox lineTwoTextBox;
        private System.Windows.Forms.ListBox fieldsListBox;
        private System.Windows.Forms.Label helpText;
        private System.Windows.Forms.Label previewLabel;
        private System.Windows.Forms.Label previewLineOne;
        private System.Windows.Forms.Label previewLineTwo;
        private System.Windows.Forms.Label iconLabel;
        private System.Windows.Forms.ComboBox iconComboBox;
        private System.Windows.Forms.Label errorText;
    }
}