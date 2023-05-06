using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Eicher
{
    public partial class Form1 : Form
    {
        FileHandling fileHandling = FileHandling.GetInstance();
        public Form1()
        {
            InitializeComponent();
            CreateDefaultValueFiles();
            this.TopMost = false;
            InitializeDefaultState();
            ReadButtonText = Constants.READ_FORWARD;
            comboBoxShift.SelectedIndex = 0;
            TempStatus = Constants.PASS;
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
            if(!frmLogin.LoginCheck)
            {
                System.Environment.Exit(1);
            }
            if(!frmLogin.IsAdminUser)
            {
                toolStripMenuItemSetting.Enabled = false;
            }
            fileHandling.Form1 = this;
        }

        private void CreateDefaultValueFiles()
        {
            fileHandling.CreateDefaultValueFile();
        }

        /// <summary>
        /// Resets the form and deletes all the values without saving
        /// </summary>
        private void ResetForm()
        {
            if (Status == Constants.FAIL)
            {
                TempStatus = Constants.FAIL;
            }
            if (ReadButtonText.Contains(Constants.REVERSE))
            {
                fileHandling.MoveTempToFinal(this);
                if (TempStatus == Constants.PASS)
                {
                    GearNo = (Convert.ToInt32(GearNo) + 1).ToString();
                }
                TempStatus = Constants.PASS;
            }
            
            CommonSetting();
        }

        private void InitializeDefaultState()
        {
            if (string.IsNullOrEmpty(Common.AlarmValue))
                Common.AlarmValue = fileHandling.ReadDefault(Constants.ALARM);
            panelStatus.BackColor = Color.RoyalBlue;

            CommonSetting();
        }
        private void CommonSetting()
        {
            textBoxDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
            //RPM = string.Empty;
            GM1Value = string.Empty;
            GM2Value = string.Empty;
            GM3Value = string.Empty;
            GM4Value = string.Empty;
            GM5Value = string.Empty;
            GM6Value = string.Empty;

            GM1CH2Value = string.Empty;
            GM2CH2Value = string.Empty;
            GM3CH2Value = string.Empty;
            GM4CH2Value = string.Empty;
            GM5CH2Value = string.Empty;
            GM6CH2Value = string.Empty;

            GM1CH3Value = string.Empty;
            GM2CH3Value = string.Empty;
            GM3CH3Value = string.Empty;
            GM4CH3Value = string.Empty;
            GM5CH3Value = string.Empty;
            GM6CH3Value = string.Empty;

            HighestPeakValue = string.Empty;
            Status = Constants.STATUS;
            //cmbChannel.Text = "Channel 1";
            if (GearTeeth == null)
            {
                GearTeeth = fileHandling.ReadDefault(Constants.GEAR);
            }
            if (PinionTeeth == null)
            {
                PinionTeeth = fileHandling.ReadDefault(Constants.PINION);
            }
            textBoxGearTeeth.Text = GearTeeth;
            textBoxPinionTeeth.Text = PinionTeeth;
            if (chartViewFFT.Controls.Count > 0)
            {
                chartViewFFT.Controls.Clear();
            }
            if (chartViewFFT2.Controls.Count > 0)
            {
                chartViewFFT2.Controls.Clear();
            }
            if (chartViewFFT3.Controls.Count > 0)
            {
                chartViewFFT3.Controls.Clear();
            }
            this.buttonNext.Visible = false;
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeDefaultState();
        }

        private void gearDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
            if (frmSettings.IsOK)
            {
                GearTeeth = frmSettings.GearTeeth;
                PinionTeeth = frmSettings.PinionTeeth;
                fileHandling.SaveDefault(Constants.GEAR, GearTeeth);
                fileHandling.SaveDefault(Constants.PINION, PinionTeeth);
            }
            textBoxGearTeeth.Text = GearTeeth == null ? "45" : GearTeeth;
            textBoxPinionTeeth.Text = PinionTeeth == null ? "22" : PinionTeeth;
            
        }

        private void alarmSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlarmSettings alarmSettings = new AlarmSettings();
            alarmSettings.ShowDialog();
            if (alarmSettings.IsOK)
            {
                Common.AlarmValue = alarmSettings.MaxPeakValue;
                fileHandling.SaveDefault(Constants.ALARM, Common.AlarmValue);
            }
        }

        /// <summary>
        /// Saves the Current status in File structure based on date and batch number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileHandling.SaveTempFile(this))
                {
                    MessageBox.Show("File saved successfully", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ResetForm();
                    ChangeSaveButtonDisplay(false);
                    //Change button text
                    UpdateReadButtonText();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Intimates user about the change of Batch Number as it will affect the file structure for saving data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxBatchNo_TextChanged(object sender, EventArgs e)
        {

        }

        public string TempStatus { get; set; }
        public string GearTeeth { get; set; }
        public string PinionTeeth { get; set; }
        public string Status
        {
            get => labelStatus.Text;
            set => labelStatus.Text = value;
        }
        public string Status_CH1 { get; set; }
        public string Status_CH2 { get; set; }
        public string Status_CH3 { get; set; }
        public string BatchNo => textBoxBatchNo.Text;

        public string Customer => textBoxCustomer.Text;
        public string ShiftIncharge => textBoxShiftIncharge.Text;
        public string OperatorName => textBoxOperatorName.Text;
        public string Shift => comboBoxShift.SelectedItem.ToString();



        public string GearNo
        {
            get => textBoxGearNo.Text;
            set => textBoxGearNo.Text = value;
        }
        public string HighestPeakValue
        {
            get => labelPeakValue.Text;
            set => labelPeakValue.Text = value;
        }
        public string Highest_GM_CH1{ get; set; }
        public string Highest_GM_CH2 { get; set; }
        public string Highest_GM_CH3 { get; set; }
        public string GM1Value
        {
            get => textBoxGM1.Text;
            set => textBoxGM1.Text = value;
        }

        public string GM2Value
        {
            get => textBoxGM2.Text;
            set => textBoxGM2.Text = value;
        }

        public string GM3Value
        {
            get => textBoxGM3.Text;
            set => textBoxGM3.Text = value;
        }

        public string GM4Value
        {
            get => textBoxGM4.Text;
            set => textBoxGM4.Text = value;
        }

        public string GM5Value
        {
            get => textBoxGM5.Text;
            set => textBoxGM5.Text = value;
        }

        public string GM6Value
        {
            get => textBoxGM6.Text;
            set => textBoxGM6.Text = value;
        }

        public string GM1CH2Value
        {
            get => tbC2GM1.Text;
            set => tbC2GM1.Text = value;
        }

        public string GM2CH2Value
        {
            get => tbC2GM2.Text;
            set => tbC2GM2.Text = value;
        }

        public string GM3CH2Value
        {
            get => tbC2GM3.Text;
            set => tbC2GM3.Text = value;
        }

        public string GM4CH2Value
        {
            get => tbC2GM4.Text;
            set => tbC2GM4.Text = value;
        }

        public string GM5CH2Value
        {
            get => tbC2GM5.Text;
            set => tbC2GM5.Text = value;
        }

        public string GM6CH2Value
        {
            get => tbC2GM6.Text;
            set => tbC2GM6.Text = value;
        }

        public string GM1CH3Value
        {
            get => tbC3GM1.Text;
            set => tbC3GM1.Text = value;
        }

        public string GM2CH3Value
        {
            get => tbC3GM2.Text;
            set => tbC3GM2.Text = value;
        }

        public string GM3CH3Value
        {
            get => tbC3GM3.Text;
            set => tbC3GM3.Text = value;
        }

        public string GM4CH3Value
        {
            get => tbC3GM4.Text;
            set => tbC3GM4.Text = value;
        }

        public string GM5CH3Value
        {
            get => tbC3GM5.Text;
            set => tbC3GM5.Text = value;
        }

        public string GM6CH3Value
        {
            get => tbC3GM6.Text;
            set => tbC3GM6.Text = value;
        }

        public string RPM
        {
            get => textBoxRPM.Text;
            set => textBoxRPM.Text = value;
        }

        public string ReadButtonText
        {
            get => buttonRead.Text;
            set => buttonRead.Text = value;
        }
        public List<double> XData
        { get; set; }

        public List<double> YData
        { get; set; }

        public List<double> XDataCh2
        { get; set; }

        public List<double> YDataCh2
        { get; set; }

        public List<double> XDataCh3
        { get; set; }

        public List<double> YDataCh3
        { get; set; }

        public int ChannelCount
        { get; set; }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Change Status Bar color based on status value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatus_TextChanged(object sender, EventArgs e)
        {
            switch (Status)
            {
                case Constants.PASS: panelStatus.BackColor = Color.Green; labelPeakValue.ForeColor = Color.Green; break;
                case Constants.FAIL: panelStatus.BackColor = Color.Red; labelPeakValue.ForeColor = Color.Red; break;
                default: panelStatus.BackColor = Color.RoyalBlue; labelPeakValue.ForeColor = Color.Black; break;
            }
        }

       

        /// <summary>
        /// Update the display once Highest peak value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelPeakValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(labelPeakValue.Text))
            {
                return;
            }
            try
            {
                AnalyzeData analyseData = new AnalyzeData();
                Status = analyseData.FetchStatus(HighestPeakValue);
                Status_CH1 = analyseData.FetchStatus(Highest_GM_CH1);
                Status_CH2 = analyseData.FetchStatus(Highest_GM_CH2);
                Status_CH3 = analyseData.FetchStatus(Highest_GM_CH3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Status Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
        }

        private void GMValue_TextChanged(object sender, EventArgs e)
        {
            double CH1GM1 = !string.IsNullOrEmpty(GM1Value) ? Convert.ToDouble(GM1Value) : 0;
            double CH1GM2 = !string.IsNullOrEmpty(GM2Value) ? Convert.ToDouble(GM2Value) : 0;
            double CH1GM3 = !string.IsNullOrEmpty(GM3Value) ? Convert.ToDouble(GM3Value) : 0;
            double CH1GM4 = !string.IsNullOrEmpty(GM4Value) ? Convert.ToDouble(GM4Value) : 0;
            double CH1GM5 = !string.IsNullOrEmpty(GM5Value) ? Convert.ToDouble(GM5Value) : 0;
            double CH1GM6 = !string.IsNullOrEmpty(GM6Value) ? Convert.ToDouble(GM6Value) : 0;

            double CH2GM1 = !string.IsNullOrEmpty(GM1CH2Value) ? Convert.ToDouble(GM1CH2Value) : 0;
            double CH2GM2 = !string.IsNullOrEmpty(GM2CH2Value) ? Convert.ToDouble(GM2CH2Value) : 0;
            double CH2GM3 = !string.IsNullOrEmpty(GM3CH2Value) ? Convert.ToDouble(GM3CH2Value) : 0;
            double CH2GM4 = !string.IsNullOrEmpty(GM4CH2Value) ? Convert.ToDouble(GM4CH2Value) : 0;
            double CH2GM5 = !string.IsNullOrEmpty(GM5CH2Value) ? Convert.ToDouble(GM5CH2Value) : 0;
            double CH2GM6 = !string.IsNullOrEmpty(GM6CH2Value) ? Convert.ToDouble(GM6CH2Value) : 0;

            double CH3GM1 = !string.IsNullOrEmpty(GM1CH3Value) ? Convert.ToDouble(GM1CH3Value) : 0;
            double CH3GM2 = !string.IsNullOrEmpty(GM2CH3Value) ? Convert.ToDouble(GM2CH3Value) : 0;
            double CH3GM3 = !string.IsNullOrEmpty(GM3CH3Value) ? Convert.ToDouble(GM3CH3Value) : 0;
            double CH3GM4 = !string.IsNullOrEmpty(GM4CH3Value) ? Convert.ToDouble(GM4CH3Value) : 0;
            double CH3GM5 = !string.IsNullOrEmpty(GM5CH3Value) ? Convert.ToDouble(GM5CH3Value) : 0;
            double CH3GM6 = !string.IsNullOrEmpty(GM6CH3Value) ? Convert.ToDouble(GM6CH3Value) : 0;

            double[] GMs = new double[] { CH1GM1, CH1GM2, CH2GM1, CH2GM2, CH3GM1, CH3GM2 };
            double[] GMsCH1 = new double[] { CH1GM1, CH1GM2, CH1GM3, CH1GM4, CH1GM5, CH1GM6 };
            double[] GMsCH2 = new double[] { CH2GM1, CH2GM2, CH2GM3, CH2GM4, CH2GM5, CH2GM6 };
            double[] GMsCH3 = new double[] { CH3GM1, CH3GM2, CH3GM3, CH3GM4, CH3GM5, CH3GM6 };

            Highest_GM_CH1 = Math.Round(GMsCH1.Max(), 2).ToString();
            Highest_GM_CH2 = Math.Round(GMsCH2.Max(), 2).ToString();
            Highest_GM_CH3 = Math.Round(GMsCH3.Max(), 2).ToString();
            HighestPeakValue = Math.Round(GMs.Max(), 2).ToString();
            
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            try
            {
                CleanPreviousDataVariables();
                if (buttonNext.Visible)
                {
                    string datastring = ReadButtonText.Contains(Constants.REVERSE) ? Constants.REVERSE : Constants.FORWARD;
                    throw new Exception("Please save the " + datastring + " data before proceding.");
                }
                if (string.IsNullOrEmpty(RPM))
                {
                    throw new NullReferenceException("RPM value can not be blank");
                }
                //Connect with instrument and Save latest datafile to localsystem
                Kohtect107TXV device = Kohtect107TXV.Instance;
                bool newFileSaved = device.CopyFile();
                if (!newFileSaved)
                {
                    throw new Exception("Unable to read data from instrument.");
                }
                //Read Data
                var dataXY = fileHandling.ReadFFTFile(Constants.LOCALFILEPATH);
                if (dataXY.Count < 2)
                {
                    throw new Exception("Data error. Kindly contact administrator.");
                }
                SetGraphWindowSettings(dataXY.Count);

                AnalyseAndCreateGraph(dataXY);
                AnalyzeData analyseData = new AnalyzeData();

                if (analyseData.IsGMFHigherThenRequired(HighestPeakValue))
                {
                    MessageBox.Show("Reading is not Perfect. Kindly take reading again in instrument.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    ChangeSaveButtonDisplay(true);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("rapi.dll"))
                {
                    MessageBox.Show("Device connection issue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
        }

        private void CleanPreviousDataVariables()
        {
            XData = null;
            YData = null;
            XDataCh2 = null;
            YDataCh2 = null;
            XDataCh3 = null;
            YDataCh3 = null;
        }

        private void SetGraphWindowSettings(int count)
        {
            switch(count)
            {
                case 2:  chartViewFFT.Height = panelGraph.Height;
                    ChannelCount = 1; 
                    break; 
                case 4:  chartViewFFT.Height = panelGraph.Height / 2;
                    chartViewFFT2.Height = panelGraph.Height / 2;
                    ChannelCount = 2;
                    break;
                case 6:  chartViewFFT.Height = panelGraph.Height / 3;
                    chartViewFFT2.Height = panelGraph.Height / 3;
                    chartViewFFT3.Height = panelGraph.Height / 3;
                    ChannelCount = 3;
                    break;
            }
        }

        private void textBox_OnlyDecimal(object sender, KeyPressEventArgs e)
        {

        }
        private void textBox_OnlyInt(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_AvoidSpecialCharacters(object sender, KeyPressEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9\s\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void textBox_NoEntry(object sender, KeyPressEventArgs e)
        {

            e.Handled = true;

        }
        private void ChangeSaveButtonDisplay(bool display)
        {
            this.buttonNext.Visible = display;
        }
        private void UpdateReadButtonText()
        {
            ReadButtonText = string.Equals(ReadButtonText, Constants.READ_FORWARD) ? Constants.READ_REVERSE : Constants.READ_FORWARD;
        }

        private void AnalyseAndCreateGraph(List<List<double>> dataXY)
        {
            try
            {
                XData = dataXY[0];
                YData = dataXY[1];

                //Calculate and Analyse gear data
                GearAnalysis gearAnalysis = new GearAnalysis();
                gearAnalysis.Analyse(XData, YData,
                                     !string.IsNullOrEmpty(RPM) ? Convert.ToInt32(RPM) : Constants.DEFAULT_RPM,
                                     !string.IsNullOrEmpty(GearTeeth) ? Convert.ToInt32(GearTeeth) : Convert.ToInt32(fileHandling.ReadDefault(Constants.GEAR)),
                                     !string.IsNullOrEmpty(PinionTeeth) ? Convert.ToInt32(PinionTeeth) : Convert.ToInt32(fileHandling.ReadDefault(Constants.PINION)));
                GM1Value = Math.Round(gearAnalysis.GM1Value, 2).ToString();
                GM2Value = Math.Round(gearAnalysis.GM2Value, 2).ToString();
                GM3Value = Math.Round(gearAnalysis.GM3Value, 2).ToString();
                GM4Value = Math.Round(gearAnalysis.GM4Value, 2).ToString();
                GM5Value = Math.Round(gearAnalysis.GM5Value, 2).ToString();
                GM6Value = Math.Round(gearAnalysis.GM6Value, 2).ToString();

                //Add graph
                if (chartViewFFT.Controls.Count > 0)
                {
                    chartViewFFT.Controls.Clear();
                }
                Graph graph = new Graph();
                graph.Dock = DockStyle.Fill;
                graph.XLabel = "Hz";
                graph.YLabel = "um/s";
                graph.GMXValue = gearAnalysis.GMXValue;
                graph.DrawGraph(XData, YData);
                chartViewFFT.Controls.Add(graph);


                XDataCh2 = dataXY[2];
                YDataCh2 = dataXY[3];

                gearAnalysis.Analyse(XDataCh2, YDataCh2,
                                     !string.IsNullOrEmpty(RPM) ? Convert.ToInt32(RPM) : Constants.DEFAULT_RPM,
                                     !string.IsNullOrEmpty(GearTeeth) ? Convert.ToInt32(GearTeeth) : Convert.ToInt32(fileHandling.ReadDefault(Constants.GEAR)),
                                     !string.IsNullOrEmpty(PinionTeeth) ? Convert.ToInt32(PinionTeeth) : Convert.ToInt32(fileHandling.ReadDefault(Constants.PINION)));
                GM1CH2Value = Math.Round(gearAnalysis.GM1Value, 2).ToString();
                GM2CH2Value = Math.Round(gearAnalysis.GM2Value, 2).ToString();
                GM3CH2Value = Math.Round(gearAnalysis.GM3Value, 2).ToString();
                GM4CH2Value = Math.Round(gearAnalysis.GM4Value, 2).ToString();
                GM5CH2Value = Math.Round(gearAnalysis.GM5Value, 2).ToString();
                GM6CH2Value = Math.Round(gearAnalysis.GM6Value, 2).ToString();

                //Add graph
                if (chartViewFFT2.Controls.Count > 0)
                {
                    chartViewFFT2.Controls.Clear();
                }
                graph = new Graph();
                graph.Dock = DockStyle.Fill;
                graph.XLabel = "Hz";
                graph.YLabel = "um/s";
                graph.GMXValue = gearAnalysis.GMXValue;
                graph.DrawGraph(XDataCh2, YDataCh2);
                chartViewFFT2.Controls.Add(graph);



                XDataCh3 = dataXY[4];
                YDataCh3 = dataXY[5];

                gearAnalysis.Analyse(XDataCh3, YDataCh3,
                                     !string.IsNullOrEmpty(RPM) ? Convert.ToInt32(RPM) : Constants.DEFAULT_RPM,
                                     !string.IsNullOrEmpty(GearTeeth) ? Convert.ToInt32(GearTeeth) : Convert.ToInt32(fileHandling.ReadDefault(Constants.GEAR)),
                                     !string.IsNullOrEmpty(PinionTeeth) ? Convert.ToInt32(PinionTeeth) : Convert.ToInt32(fileHandling.ReadDefault(Constants.PINION)));
                GM1CH3Value = Math.Round(gearAnalysis.GM1Value, 2).ToString();
                GM2CH3Value = Math.Round(gearAnalysis.GM2Value, 2).ToString();
                GM3CH3Value = Math.Round(gearAnalysis.GM3Value, 2).ToString();
                GM4CH3Value = Math.Round(gearAnalysis.GM4Value, 2).ToString();
                GM5CH3Value = Math.Round(gearAnalysis.GM5Value, 2).ToString();
                GM6CH3Value = Math.Round(gearAnalysis.GM6Value, 2).ToString();

                //Add graph
                if (chartViewFFT3.Controls.Count > 0)
                {
                    chartViewFFT3.Controls.Clear();
                }
                graph = new Graph();
                graph.Dock = DockStyle.Fill;
                graph.XLabel = "Hz";
                graph.YLabel = "um/s";
                graph.GMXValue = gearAnalysis.GMXValue;
                graph.DrawGraph(XDataCh3, YDataCh3);
                chartViewFFT3.Controls.Add(graph);

                labelPeakValue_TextChanged(null, null);
            }
            catch
            {

            }
        }

        private void generateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowReport showReport = new ShowReport();
            showReport.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void toolStripMenuItemSetting_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItemSetting_DropDownOpening(object sender, EventArgs e)
        {
            
        }

        private void textBoxRPM_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
