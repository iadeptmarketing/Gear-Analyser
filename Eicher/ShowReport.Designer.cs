
namespace Eicher
{
    partial class ShowReport
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonWord = new System.Windows.Forms.Button();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.listViewBatch = new System.Windows.Forms.ListView();
            this.listViewDate = new System.Windows.Forms.ListView();
            this.buttonResetGears = new System.Windows.Forms.Button();
            this.buttonBatchToFiles = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonDateToBatch = new System.Windows.Forms.Button();
            this.buttonPassReport = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.SystemColors.Info;
            this.panelMain.Controls.Add(this.checkBoxAll);
            this.panelMain.Controls.Add(this.button4);
            this.panelMain.Controls.Add(this.buttonPassReport);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.buttonWord);
            this.panelMain.Controls.Add(this.listViewFile);
            this.panelMain.Controls.Add(this.listViewBatch);
            this.panelMain.Controls.Add(this.listViewDate);
            this.panelMain.Controls.Add(this.buttonResetGears);
            this.panelMain.Controls.Add(this.buttonBatchToFiles);
            this.panelMain.Controls.Add(this.button2);
            this.panelMain.Controls.Add(this.buttonDateToBatch);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 450);
            this.panelMain.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(582, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Gears";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(295, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Batch No.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "Date";
            // 
            // buttonWord
            // 
            this.buttonWord.Location = new System.Drawing.Point(587, 401);
            this.buttonWord.Name = "buttonWord";
            this.buttonWord.Size = new System.Drawing.Size(201, 39);
            this.buttonWord.TabIndex = 10;
            this.buttonWord.Text = "Generate Report for all selected Gears";
            this.buttonWord.UseVisualStyleBackColor = true;
            this.buttonWord.Click += new System.EventHandler(this.buttonWord_Click);
            // 
            // listViewFile
            // 
            this.listViewFile.HideSelection = false;
            this.listViewFile.Location = new System.Drawing.Point(587, 57);
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(201, 338);
            this.listViewFile.TabIndex = 9;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.List;
            // 
            // listViewBatch
            // 
            this.listViewBatch.HideSelection = false;
            this.listViewBatch.Location = new System.Drawing.Point(300, 57);
            this.listViewBatch.MultiSelect = false;
            this.listViewBatch.Name = "listViewBatch";
            this.listViewBatch.Size = new System.Drawing.Size(201, 338);
            this.listViewBatch.TabIndex = 8;
            this.listViewBatch.UseCompatibleStateImageBehavior = false;
            this.listViewBatch.View = System.Windows.Forms.View.List;
            // 
            // listViewDate
            // 
            this.listViewDate.HideSelection = false;
            this.listViewDate.Location = new System.Drawing.Point(12, 57);
            this.listViewDate.MultiSelect = false;
            this.listViewDate.Name = "listViewDate";
            this.listViewDate.Size = new System.Drawing.Size(201, 338);
            this.listViewDate.TabIndex = 7;
            this.listViewDate.UseCompatibleStateImageBehavior = false;
            this.listViewDate.View = System.Windows.Forms.View.List;
            // 
            // buttonResetGears
            // 
            this.buttonResetGears.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonResetGears.Location = new System.Drawing.Point(507, 234);
            this.buttonResetGears.Name = "buttonResetGears";
            this.buttonResetGears.Size = new System.Drawing.Size(75, 30);
            this.buttonResetGears.TabIndex = 5;
            this.buttonResetGears.Text = "Reset Gears";
            this.buttonResetGears.UseVisualStyleBackColor = true;
            this.buttonResetGears.Click += new System.EventHandler(this.buttonResetGears_Click);
            // 
            // buttonBatchToFiles
            // 
            this.buttonBatchToFiles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBatchToFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBatchToFiles.Location = new System.Drawing.Point(507, 189);
            this.buttonBatchToFiles.Name = "buttonBatchToFiles";
            this.buttonBatchToFiles.Size = new System.Drawing.Size(75, 23);
            this.buttonBatchToFiles.TabIndex = 4;
            this.buttonBatchToFiles.Text = "------>";
            this.buttonBatchToFiles.UseVisualStyleBackColor = true;
            this.buttonBatchToFiles.Click += new System.EventHandler(this.buttonBatchToFiles_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(219, 234);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 49);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // buttonDateToBatch
            // 
            this.buttonDateToBatch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDateToBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDateToBatch.Location = new System.Drawing.Point(219, 189);
            this.buttonDateToBatch.Name = "buttonDateToBatch";
            this.buttonDateToBatch.Size = new System.Drawing.Size(75, 23);
            this.buttonDateToBatch.TabIndex = 1;
            this.buttonDateToBatch.Text = "------>";
            this.buttonDateToBatch.UseVisualStyleBackColor = true;
            this.buttonDateToBatch.Click += new System.EventHandler(this.buttonDateToBatch_Click);
            // 
            // buttonPassReport
            // 
            this.buttonPassReport.Location = new System.Drawing.Point(380, 401);
            this.buttonPassReport.Name = "buttonPassReport";
            this.buttonPassReport.Size = new System.Drawing.Size(201, 39);
            this.buttonPassReport.TabIndex = 14;
            this.buttonPassReport.Text = "Generate Pass Report for selected Gears";
            this.buttonPassReport.UseVisualStyleBackColor = true;
            this.buttonPassReport.Click += new System.EventHandler(this.buttonPassReport_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(173, 401);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(201, 39);
            this.button4.TabIndex = 15;
            this.button4.Text = "Generate Pass Report for selected Gears";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(587, 41);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(117, 17);
            this.checkBoxAll.TabIndex = 16;
            this.checkBoxAll.Text = "Select/Deselect All";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.Visible = false;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // ShowReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Show Report";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ShowReport_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonDateToBatch;
        private System.Windows.Forms.Button buttonResetGears;
        private System.Windows.Forms.Button buttonBatchToFiles;
        private System.Windows.Forms.ListView listViewDate;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ListView listViewBatch;
        private System.Windows.Forms.Button buttonWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonPassReport;
        private System.Windows.Forms.CheckBox checkBoxAll;
    }
}