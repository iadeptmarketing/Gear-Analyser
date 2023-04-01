
namespace Eicher
{
    partial class FrmSettings
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
            this.labelGearTeeth = new System.Windows.Forms.Label();
            this.textBoxGearTeeth = new System.Windows.Forms.TextBox();
            this.textBoxPinionTeeth = new System.Windows.Forms.TextBox();
            this.labelPinionTeeth = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGearTeeth
            // 
            this.labelGearTeeth.AutoSize = true;
            this.labelGearTeeth.Location = new System.Drawing.Point(12, 15);
            this.labelGearTeeth.Name = "labelGearTeeth";
            this.labelGearTeeth.Size = new System.Drawing.Size(61, 13);
            this.labelGearTeeth.TabIndex = 0;
            this.labelGearTeeth.Text = "Gear Teeth";
            // 
            // textBoxGearTeeth
            // 
            this.textBoxGearTeeth.Location = new System.Drawing.Point(89, 12);
            this.textBoxGearTeeth.Name = "textBoxGearTeeth";
            this.textBoxGearTeeth.Size = new System.Drawing.Size(136, 20);
            this.textBoxGearTeeth.TabIndex = 1;
            this.textBoxGearTeeth.Text = "45";
            this.textBoxGearTeeth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // textBoxPinionTeeth
            // 
            this.textBoxPinionTeeth.Location = new System.Drawing.Point(89, 38);
            this.textBoxPinionTeeth.Name = "textBoxPinionTeeth";
            this.textBoxPinionTeeth.Size = new System.Drawing.Size(136, 20);
            this.textBoxPinionTeeth.TabIndex = 3;
            this.textBoxPinionTeeth.Text = "22";
            this.textBoxPinionTeeth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // labelPinionTeeth
            // 
            this.labelPinionTeeth.AutoSize = true;
            this.labelPinionTeeth.Location = new System.Drawing.Point(12, 41);
            this.labelPinionTeeth.Name = "labelPinionTeeth";
            this.labelPinionTeeth.Size = new System.Drawing.Size(67, 13);
            this.labelPinionTeeth.TabIndex = 2;
            this.labelPinionTeeth.Text = "Pinion Teeth";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(175, 66);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(50, 25);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 101);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxPinionTeeth);
            this.Controls.Add(this.labelPinionTeeth);
            this.Controls.Add(this.textBoxGearTeeth);
            this.Controls.Add(this.labelGearTeeth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gear Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGearTeeth;
        private System.Windows.Forms.TextBox textBoxGearTeeth;
        private System.Windows.Forms.TextBox textBoxPinionTeeth;
        private System.Windows.Forms.Label labelPinionTeeth;
        private System.Windows.Forms.Button buttonOK;
    }
}