using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eicher
{
    public partial class ShowReport : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int BringWindowToTop(IntPtr hwnd);

        public IntPtr HWND_TOPMOST = (IntPtr)(-1);
        public IntPtr HWND_NOTOPMOST = (IntPtr)(-2);
        public int SWP_NOSIZE = 0x1;

        public ShowReport()
        {
            InitializeComponent();
            this.TopMost = false;
        }
        public string selectedDate { get; set; }
        public string selectedBatch { get; set; }
        private void ShowReport_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("Directory.currentdirectory -> " + Directory.GetCurrentDirectory());
            //MessageBox.Show("Environment.currentdirectory -> " + Environment.CurrentDirectory);
            string[] DateFolders = System.IO.Directory.GetDirectories(Directory.GetCurrentDirectory(), "*", System.IO.SearchOption.TopDirectoryOnly);
            int i = 0;
            foreach (var dir in DateFolders)
            {
                listViewDate.Items.Add(new ListViewItem(new string[] { dir.Substring(dir.LastIndexOf("\\") + 1),dir }));
                listViewDate.Items[i].Tag = dir;
                i++;
            }
            
            
        }

        private void buttonDateToBatch_Click(object sender, EventArgs e)
        {
            try
            {
                int i = listViewBatch.Items.Count;
                selectedDate = listViewDate.SelectedItems[0].Tag.ToString();
                string[] BatchFolders = System.IO.Directory.GetDirectories(selectedDate, "*", System.IO.SearchOption.TopDirectoryOnly);
                foreach (var dir in BatchFolders)
                {
                    listViewBatch.Items.Add(new ListViewItem(new string[] { dir.Substring(dir.LastIndexOf("\\") + 1), dir }));
                    listViewBatch.Items[i].Tag = dir;
                    i++;
                }
            }
            catch
            {

            }
        }

        private void buttonBatchToFiles_Click(object sender, EventArgs e)
        {
            int i = listViewFile.Items.Count;
            try
            {
                selectedBatch = listViewBatch.SelectedItems[0].Tag.ToString();
                string[] dataFiles = System.IO.Directory.GetFiles(selectedBatch, "*");
                foreach (var file in dataFiles)
                {
                    listViewFile.Items.Add(new ListViewItem(new string[] { file.Substring(file.LastIndexOf("\\") + 1), file }));
                    listViewFile.Items[i].Tag = file;
                    i++;
                }
            }
            catch(Exception ex)
            {
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
            checkBoxAll.Visible = true;
        }

        /// <summary>
        /// Generate Simple report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWord_Click(object sender, EventArgs e)
        {
            FileHandling fileHandling; string fileName;
            CallSave(out fileHandling, out fileName);

            if (fileHandling != null)
            {
                ReadReportData readReport = new ReadReportData();
                for (int i = 0; i < listViewFile.SelectedItems.Count; i++)
                {
                    try
                    {
                        string reportfile = listViewFile.SelectedItems[i].Tag.ToString();
                        FileInfo fileInfo = new FileInfo(reportfile);
                        var batchNo = fileInfo.Directory.Name.Substring(11);
                        string GearNo = fileInfo.Name.Substring(0, fileInfo.Name.Length-4);
                        if (fileInfo.Name.Contains("_"))
                             GearNo = fileInfo.Name.Substring(batchNo.Length + 1, fileInfo.Name.Length - (batchNo.Length + 5));

                        var fileData = readReport.GetData(reportfile);
                        fileHandling.SaveReportValues(fileName, batchNo, GearNo, fileData);
                    }
                    catch
                    { }
                }
                StartProcess(fileName);
            }

           

            //foreach(var file in listViewFile.Items)
            //{
            //    string reportFile = ((ListView.SelectedListViewItemCollection)file)[0].Tag.ToString();
            //    var fileData = readReport.GetData(reportFile);

            //}
        }

        private void CallSave(out FileHandling fileHandling, out string fileName)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.InitialDirectory = @"C:\";
            SaveFileDialog1.RestoreDirectory = true;
            SaveFileDialog1.Title = "Save Report File";
            SaveFileDialog1.DefaultExt = "xls";
            SaveFileDialog1.Filter = "Excel files (*.xls)|*.xls|txt files (*.txt)|*.txt|Word files (*.doc)|*.doc";
            if (DialogResult.OK == SaveFileDialog1.ShowDialog())
            {
                fileHandling = new FileHandling();
                fileName = SaveFileDialog1.FileName;
                fileHandling.SaveReportFileHeader(SaveFileDialog1.FileName);
                return;
            }
            fileHandling = null;
            fileName = null;
        }
        private void StartProcess(string fileName)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Maximized,
                FileName = fileName
            };
            process.Start();
            BringWindowToTop(process.MainWindowHandle);
            SetWindowPos(process.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE);
        }
        private void buttonPassReport_Click(object sender, EventArgs e)
        {
            FileHandling fileHandling; string fileName;
            CallSave(out fileHandling, out fileName);
            
            if( fileHandling!=null)
            {
               ReadReportData readReport = new ReadReportData();
                for (int i = 0; i < listViewFile.SelectedItems.Count; i++)
                {
                    try
                    {
                        string reportfile = listViewFile.SelectedItems[i].Tag.ToString();
                        FileInfo fileInfo = new FileInfo(reportfile);
                        var batchNo = fileInfo.Directory.Name.Substring(11);
                        string GearNo = fileInfo.Name.Substring(batchNo.Length + 1, fileInfo.Name.Length - (batchNo.Length + 5));

                        var fileData = readReport.GetData(reportfile);
                        fileHandling.SaveReportPassValues(fileName, batchNo, GearNo, fileData);
                    }
                    catch
                    { }
                }
                StartProcess(fileName);
            }
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAll.Checked)
            {
                listViewFile.SelectedItems.Clear();
            }
            else
            {
                for(int i=0;i<listViewFile.Items.Count;i++)
                {
                    listViewFile.Items[i].Selected = true;
                }
                listViewFile.Focus();
            }
        }

        private void buttonResetGears_Click(object sender, EventArgs e)
        {
            listViewFile.Items.Clear();
        }
    }
}
