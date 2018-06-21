using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Disintegrate.Customization;
using Force.DeepCloner;

namespace Disintegrate.UI
{
    public partial class PreferencesEditor : Form
    {
        public Preferences Preferences { get; private set; }
        public PresenceApp App{ get; }

        private int _lastSelectedTextBox = 1;

        public PreferencesEditor()
        {
            InitializeComponent();
        }

        public PreferencesEditor(PresenceApp app, Preferences preferences) : this()
        {
            Preferences = preferences;
            App = app;
        }

        private void PreferencesEditor_Load(object sender, EventArgs e)
        {
            // TODO: Checkboxes not implemented
            errorText.Text = "";

            titleLabel.Text = $"Customising {App.AppName}";

            lineOneTextBox.Text = Preferences.LineOne;
            lineTwoTextBox.Text = Preferences.LineTwo;

            iconComboBox.Items.Clear();
            iconComboBox.Items.AddRange(Preferences.Customizer.Icons.ToArray());
            iconComboBox.SelectedItem = Preferences.Icon;

            var fieldNames = Preferences.Customizer.TextFields.Select(f => f.Name).ToArray();
            fieldsListBox.Items.Clear();
            fieldsListBox.Items.AddRange(fieldNames);

            RefreshPreview();
        }

        private void RefreshPreview()
        {
            try
            {
                (previewLineOne.Text, previewLineTwo.Text) = Preferences.FillFieldsByFunction(k =>
                    Preferences.Customizer.TextFields.SingleOrDefault(t => t.Name == k)?.Example
                );
                errorText.Text = "";
            }
            catch (Exception e)
            {
                errorText.Text = $"Error: {e.Message}";
            }
        }

        private void fieldsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = fieldsListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var target = _lastSelectedTextBox == 1
                    ? lineOneTextBox
                    : lineTwoTextBox;

                var item = fieldsListBox.Items[index];

                var field = $"{{{item}}}";
                target.Text = target.Text.Insert(target.SelectionStart, field);
                target.SelectionStart += field.Length;
            }
        }

        private void lineOneTextBox_TextChanged(object sender, EventArgs e)
        {
            Preferences.LineOne = lineOneTextBox.Text;
            RefreshPreview();
        }

        private void lineTwoTextBox_TextChanged(object sender, EventArgs e)
        {
            Preferences.LineTwo = lineTwoTextBox.Text;
            RefreshPreview();
        }

        private void lineOneTextBox_Enter(object sender, EventArgs e)
        {
            _lastSelectedTextBox = 1;
        }

        private void lineTwoTextBox_Enter(object sender, EventArgs e)
        {
            _lastSelectedTextBox = 2;
        }

        private void iconComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Preferences.Icon = (string)iconComboBox.SelectedItem;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset your preferences?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //var def = App.Customizer.Default;
                Preferences = App.Customizer.Default.DeepClone();
                //App.Customizer.Default = def;
                PreferencesEditor_Load(this, null);
            }
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard your changes?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Loader.SavePreferences(App, Preferences);
            MessageBox.Show("Saved your preferences.");

            App.ClearCachedPreferences();

            Close();
        }
    }
}
