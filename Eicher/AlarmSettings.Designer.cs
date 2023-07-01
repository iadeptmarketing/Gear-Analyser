
namespace Eicher
{
    partial class AlarmSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMaxVal = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkDataAlarmValidation = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maximum Peak Value";
            // 
            // textBoxMaxVal
            // 
            this.textBoxMaxVal.Location = new System.Drawing.Point(122, 12);
            this.textBoxMaxVal.Name = "textBoxMaxVal";
            this.textBoxMaxVal.Size = new System.Drawing.Size(100, 20);
            this.textBoxMaxVal.TabIndex = 1;
            this.textBoxMaxVal.Text = "10";
            this.textBoxMaxVal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(172, 64);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(50, 25);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Gear 1",
            "Gear 2",
            "Gear 3",
            "Gear 4",
            "Gear 5"});
            this.comboBox1.Location = new System.Drawing.Point(16, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Visible = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // chkDataAlarmValidation
            // 
            this.chkDataAlarmValidation.AutoSize = true;
            this.chkDataAlarmValidation.Location = new System.Drawing.Point(10, 45);
            this.chkDataAlarmValidation.Name = "chkDataAlarmValidation";
            this.chkDataAlarmValidation.Size = new System.Drawing.Size(207, 17);
            this.chkDataAlarmValidation.TabIndex = 4;
            this.chkDataAlarmValidation.Text = "Block Data if GMF > 2.5 X Max Pk Val";
            this.chkDataAlarmValidation.UseVisualStyleBackColor = true;
            this.chkDataAlarmValidation.CheckedChanged += new System.EventHandler(this.chkDataAlarmValidation_CheckedChanged);
            // 
            // AlarmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 101);
            this.Controls.Add(this.chkDataAlarmValidation);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxMaxVal);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlarmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alarm Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMaxVal;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkDataAlarmValidation;
    }
}