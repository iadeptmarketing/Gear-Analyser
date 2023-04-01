using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eicher
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            FileHandling fileHandling = new FileHandling();
            GearTeeth = fileHandling.ReadDefault(Constants.GEAR);
            PinionTeeth = fileHandling.ReadDefault(Constants.PINION);
        }

        public string GearTeeth
        {
            get => textBoxGearTeeth.Text;
            set => textBoxGearTeeth.Text = value;
        }
        public string PinionTeeth
        {
            get => textBoxPinionTeeth.Text;
            set => textBoxPinionTeeth.Text = value;
        }
        public bool IsOK { get; set; } = false;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GearTeeth) && !string.IsNullOrEmpty(PinionTeeth))
            {
                IsOK = true;
                this.Close();
            }
            else
                MessageBox.Show("All values required before saving", "Value(s) missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

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
    }
}
