using System;
using System.Windows.Forms;

namespace Eicher
{
    public partial class AlarmSettings : Form
    {
        public AlarmSettings()
        {
            InitializeComponent();
            FileHandling fileHandling = FileHandling.GetInstance();
            MaxPeakValue = fileHandling.ReadDefault(Constants.ALARM);
            AlarmDataValidation = Convert.ToBoolean(fileHandling.ReadDefault(Constants.ALARMDATAVALIDATION));
        }
        public bool AlarmDataValidation
        {
            get => chkDataAlarmValidation.Checked;
            set => chkDataAlarmValidation.Checked = value;
        }
        public string MaxPeakValue
        {
            get => textBoxMaxVal.Text;
            set => textBoxMaxVal.Text = value;
        }
        public bool IsOK { get; set; } = false;
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MaxPeakValue))
            {
                IsOK = true;
                this.Close();
            }
            else
                MessageBox.Show("Values required before saving", "Value(s) missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    textBoxMaxVal.Text = "1";
                    break;
                case 1:
                    textBoxMaxVal.Text = "5";
                    break;
                case 2:
                    textBoxMaxVal.Text = "10";
                    break;
                case 3:
                    textBoxMaxVal.Text = "50";
                    break;
                case 4:
                    textBoxMaxVal.Text = "100";
                    break;
            }
            
        }
        
        private void chkDataAlarmValidation_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
