using com.iAM.chart2dnet;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Eicher
{
    public sealed class FileHandling
    {
        private const string tabShift = "\t";
        string defaultString = "N/A";
        public Form1 Form1 { get; set; }

        private static FileHandling Instance = null;

        public static FileHandling GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FileHandling();
            }
            return Instance;
        }

        public void CreateDefaultValueFile()
        {
            if (!File.Exists(Constants.LOCALINITFILE))
            {
                File.AppendAllText(Constants.LOCALINITFILE, Constants.ALARM + " : 10" + Environment.NewLine);
                File.AppendAllText(Constants.LOCALINITFILE, Constants.PINION + " : 22" + Environment.NewLine);
                File.AppendAllText(Constants.LOCALINITFILE, Constants.GEAR + " : 45" + Environment.NewLine);
            }
            if (!File.Exists("Initialize.ini"))
            {
                File.AppendAllText("Initialize.ini", "Admin,P@ssw0rd" + Environment.NewLine);
                File.AppendAllText("Initialize.ini", "Operator,Operator" + Environment.NewLine);
            }
        }

        public string GetPassword(string user)
        {
            if (File.Exists("Initialize.ini"))
            {
                var lines = File.ReadAllLines("Initialize.ini");
                foreach (string line in lines)
                {
                    var splitLine = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitLine[0].ToString().Trim() == user)
                    {
                        return splitLine[1].ToString().Trim();
                    }
                }
            }
            return null;
        }

        public void SetPassword(string user, string newPassword)
        {
            StringBuilder sbFileText = new StringBuilder();
            if (File.Exists("Initialize.ini"))
            {
                var lines = File.ReadAllLines("Initialize.ini");
                foreach (string line in lines)
                {
                    var splitLine = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitLine[0].ToString().Trim() == user)
                    {
                        sbFileText.AppendLine(user + "," + newPassword);
                    }
                    else
                    {
                        sbFileText.AppendLine(line);
                    }
                }
                File.Delete("Initialize.ini");
            }
            File.AppendAllText("Initialize.ini", sbFileText.ToString());
        }

        public string ReadDefault(string Header)
        {
            if (File.Exists(Constants.LOCALINITFILE))
            {
                var lines = File.ReadAllLines(Constants.LOCALINITFILE);
                foreach (string line in lines)
                {
                    var splitLine = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitLine[0].ToString().Trim() == Header)
                    {
                        return splitLine[1].ToString().Trim();
                    }
                }
            }
            return null;
        }

        public void SaveDefault(string Header, string value)
        {
            StringBuilder sbFileText = new StringBuilder();
            if (File.Exists(Constants.LOCALINITFILE))
            {
                var lines = File.ReadAllLines(Constants.LOCALINITFILE);

                foreach (string line in lines)
                {
                    var splitLine = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitLine[0].ToString().Trim() == Header)
                    {
                        sbFileText.AppendLine(Header + " : " + value);                       
                    }
                    else
                    {
                        sbFileText.AppendLine(line);
                    }
                }
                File.Delete(Constants.LOCALINITFILE);
            }
            File.AppendAllText(Constants.LOCALINITFILE,sbFileText.ToString());
        }

        /// <summary>
        /// Saves the File and returns the status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="batchNo"></param>
        /// <param name="gearNo"></param>
        /// <returns>True, if file is saved</returns>
        public bool SaveFile(string status, string batchNo, string gearNo)
        {
            if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(batchNo) || string.IsNullOrEmpty(gearNo))
            {
                throw new ArgumentNullException("Batch number or Gear number or Status can not be null");
            }

            string currentDate = DateTime.Today.ToString("dd-MM-yyyy");
            string fileName = batchNo + "_" + gearNo + ".txt";
            if (!Directory.Exists(currentDate))
            {
                Directory.CreateDirectory(currentDate);
            }
            File.AppendAllText(currentDate + Path.DirectorySeparatorChar + fileName, status);


            return true;
        }

        public void MoveTempToFinal(Form1 main)
        {
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");           
            string FileNameWithoutExtension = main.BatchNo + "_" + main.GearNo;
            string fileName = FileNameWithoutExtension + ".txt";
            string directoryPath = currentDate + Path.DirectorySeparatorChar + currentDate + "_" + main.BatchNo;
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if(main.TempStatus==Constants.FAIL)
            {
                FileNameWithoutExtension = "Failed";
            }
            string filePath = FindFileExist(directoryPath, FileNameWithoutExtension);
            File.Move(Constants.LOCALTEMPREPORTFILE, filePath);
        }
        int count = 0;
        private string FindFileExist(string directoryPath, string FileNameWithoutExtension)
        {
            if (File.Exists(directoryPath + Path.DirectorySeparatorChar + FileNameWithoutExtension + ".txt"))
            {
                count++;
                return FindFileExist(directoryPath, FileNameWithoutExtension + "-" + count);
            }
            return directoryPath + Path.DirectorySeparatorChar + FileNameWithoutExtension + ".txt";
        }

        public bool SaveTempFile(Form1 main)
        {
            if (string.IsNullOrEmpty(main.Status) || string.IsNullOrEmpty(main.BatchNo) || string.IsNullOrEmpty(main.GearNo))
            {
                throw new ArgumentNullException("Batch number or Gear number or Status can not be null");
            }
            if (string.IsNullOrEmpty(main.ShiftIncharge) || string.IsNullOrEmpty(main.Shift) || string.IsNullOrEmpty(main.OperatorName))
            {
                throw new ArgumentNullException("Shift Incharge or Operator Name or Shift not found");
            }
            if (string.IsNullOrEmpty(main.Customer))
            {
                throw new ArgumentNullException("Customer Detail not found");
            }

            string currentDateTime = DateTime.Now.ToString("dd-MM-yyyy hh.mm tt");
            string datastring = main.ReadButtonText.Contains(Constants.REVERSE) ? Constants.REVERSE : Constants.FORWARD;
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.STATUS + " " + datastring + " : " + main.Status + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.DATETIME + " " + datastring + " : " + currentDateTime + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.HIGHEST + " " + datastring + " : " + main.HighestPeakValue + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.SHIFTINCHARGE + " " + datastring + " : " + main.ShiftIncharge + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.OPERATOR + " " + datastring + " : " + main.OperatorName + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.SHIFTVALUE + " " + datastring + " : " + main.Shift + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CUSTOMER + " " + datastring + " : " + main.Customer + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.GM1 + " " + datastring + " : " + main.GM1Value + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.GM2 + " " + datastring + " : " + main.GM2Value + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.GM3 + " " + datastring + " : " + main.GM3Value + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.GM4 + " " + datastring + " : " + main.GM4Value + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.GM5 + " " + datastring + " : " + main.GM5Value + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.GM6 + " " + datastring + " : " + main.GM6Value + Environment.NewLine);

            if (main.ChannelCount >= 2)
            {
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.STATUS_CH1 + " " + datastring + " : " + main.Status_CH1 + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.HIGHEST_GM_CH1 + " " + datastring + " : " + main.Highest_GM_CH1 + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.STATUS_CH2 + " " + datastring + " : " + main.Status_CH2 + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.HIGHEST_GM_CH2 + " " + datastring + " : " + main.Highest_GM_CH2 + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH2GM1 + " " + datastring + " : " + main.GM1CH2Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH2GM2 + " " + datastring + " : " + main.GM2CH2Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH2GM3 + " " + datastring + " : " + main.GM3CH2Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH2GM4 + " " + datastring + " : " + main.GM4CH2Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH2GM5 + " " + datastring + " : " + main.GM5CH2Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH2GM6 + " " + datastring + " : " + main.GM6CH2Value + Environment.NewLine);
            }

            if (main.ChannelCount >= 3)
            {
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.STATUS_CH3 + " " + datastring + " : " + main.Status_CH3 + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.HIGHEST_GM_CH3 + " " + datastring + " : " + main.Highest_GM_CH3 + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH3GM1 + " " + datastring + " : " + main.GM1CH3Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH3GM2 + " " + datastring + " : " + main.GM2CH3Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH3GM3 + " " + datastring + " : " + main.GM3CH3Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH3GM4 + " " + datastring + " : " + main.GM4CH3Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH3GM5 + " " + datastring + " : " + main.GM5CH3Value + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.CH3GM6 + " " + datastring + " : " + main.GM6CH3Value + Environment.NewLine);
            }

            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, Constants.RPM + " " + datastring + " : " + main.RPM + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, "XData-Ch1 " + datastring + " : " + string.Join(", ", main.XData) + Environment.NewLine);
            File.AppendAllText(Constants.LOCALTEMPREPORTFILE, "YData-Ch1 " + datastring + " : " + string.Join(", ", main.YData) + Environment.NewLine);

            if (main.XDataCh2 != null && main.YDataCh2 != null)
            {
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, "XData-Ch2 " + datastring + " : " + string.Join(", ", main.XDataCh2) + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, "YData-Ch2 " + datastring + " : " + string.Join(", ", main.YDataCh2) + Environment.NewLine);
            }
            if (main.XDataCh3 != null && main.YDataCh3 != null)
            {
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, "XData-Ch3 " + datastring + " : " + string.Join(", ", main.XDataCh3) + Environment.NewLine);
                File.AppendAllText(Constants.LOCALTEMPREPORTFILE, "YData-Ch3 " + datastring + " : " + string.Join(", ", main.YDataCh3) + Environment.NewLine);
            }

            return true;
        }

        public List<List<Double>> ReadFFTFile(string filePath)
        {
            IReadData readData = new ReadFFTData();
            var dataXY = readData.GetData(filePath);

            return dataXY;
        }

        public void SaveReportFileHeader(string fileName)
        {
            StringBuilder content = new StringBuilder();
            content.Append(Constants.CUSTOMER +
                tabShift + Constants.SHIFTINCHARGE +
                tabShift + Constants.OPERATOR +
                tabShift + Constants.SHIFTVALUE +
                tabShift + "Batch No" +
                tabShift + "Gear No" +
                tabShift + "Date" +
                tabShift + "Highest(F)" +
                tabShift + "Highest(F) CH1" +
                tabShift + "RPM(F)" +
                tabShift + "Highest(R)" +
                tabShift + "Highest(R) CH1" +
                tabShift + "RPM(R)");

            //if (Form1.ChannelCount >= 2)
            {
                content.Append(tabShift + "Status CH1" +
                    tabShift + "Highest(F) CH2" +
                    tabShift + "Highest(R) CH2" +
                    tabShift + "Status CH2");
            }

            //if (Form1.ChannelCount >= 3)
            {
                content.Append(tabShift + "Highest(F) CH3" +
                    tabShift + "Highest(R) CH3" +
                    tabShift + "Status CH3");
            }

            content.Append(tabShift + "Final Status" + Environment.NewLine);

            File.AppendAllText(fileName, content.ToString());

        }
        public void SaveReportValues(string fileName, string BatchNo, String GearNo, List<Dictionary<string, string>> dataSet)
        {
            string status = Constants.FAIL;
            StringBuilder content = AppendDefaultData(BatchNo, GearNo, dataSet);

            if (dataSet[1].Count > 0) //Checking if both forward and reverse data available in dataset
            {
                if (dataSet[0][Constants.STATUS].Trim() == Constants.PASS
                    && dataSet[1][Constants.STATUS].Trim() == Constants.PASS)
                {
                    status = Constants.PASS;
                }

                content.Append(tabShift + dataSet[1][Constants.HIGHEST] +
                    tabShift + dataSet[1][Constants.HIGHEST_GM_CH1] +
                    tabShift + dataSet[1][Constants.RPM]);

                //if (Form1.ChannelCount >= 2)
                try
                {
                    content.Append(tabShift + dataSet[0][Constants.STATUS_CH1] +
                        tabShift + dataSet[0][Constants.HIGHEST_GM_CH2] +
                        tabShift + dataSet[1][Constants.HIGHEST_GM_CH2] +
                        tabShift + dataSet[0][Constants.STATUS_CH2]);
                }
                catch
                { }

                //if (Form1.ChannelCount >= 3)
                try
                {
                    content.Append(tabShift + dataSet[0][Constants.HIGHEST_GM_CH3] +
                        tabShift + dataSet[1][Constants.HIGHEST_GM_CH3] +
                        tabShift + dataSet[0][Constants.STATUS_CH2]);
                }
                catch
                { }

                content.Append(tabShift + status + Environment.NewLine);
            }
            else
                content.Append(tabShift + defaultString +
                    tabShift + "UnVerified" + Environment.NewLine);

            File.AppendAllText(fileName, content.ToString());
        }

        private StringBuilder AppendDefaultData(string BatchNo, String GearNo, List<Dictionary<string, string>> dataSet)
        {
            return new StringBuilder(dataSet[0][Constants.CUSTOMER] +
                    tabShift + dataSet[0][Constants.SHIFTINCHARGE] +
                    tabShift + dataSet[0][Constants.OPERATOR] +
                    tabShift + dataSet[0][Constants.SHIFTVALUE] +
                    tabShift + BatchNo +
                    tabShift + GearNo +
                    tabShift + dataSet[0][Constants.DATETIME] +
                    tabShift + dataSet[0][Constants.HIGHEST] +
                    tabShift + dataSet[0][Constants.HIGHEST_GM_CH1] +
                    tabShift + dataSet[0][Constants.RPM]);
        }

        public void SaveReportPassValues(string fileName, string BatchNo, String GearNo, List<Dictionary<string, string>> dataSet)
        {
            string status = Constants.FAIL;
            if (dataSet[1].Count > 0)
            {
                if (dataSet[0][Constants.STATUS].Trim() == Constants.PASS
                    && dataSet[1][Constants.STATUS].Trim() == Constants.PASS)
                {
                    status = Constants.PASS;
                    StringBuilder content = AppendDefaultData(BatchNo, GearNo, dataSet);
                    content.Append(tabShift + status + Environment.NewLine);
                    File.AppendAllText(fileName, content.ToString());                    
                }
            }
        }
        public void SaveGraphReport(string fileName, string BatchNo, String GearNo, List<Dictionary<string, string>> dataSet)
        {
            string status = Constants.FAIL;
            if (dataSet[1].Count > 0)
            {
                if (dataSet[0][Constants.STATUS].Trim() == Constants.PASS
                    && dataSet[1][Constants.STATUS].Trim() == Constants.PASS)
                {
                    status = Constants.PASS;
                }
                File.AppendAllText(fileName, BatchNo + tabShift + GearNo + tabShift + dataSet[0][Constants.HIGHEST] + tabShift + dataSet[1][Constants.HIGHEST] + tabShift + status + Environment.NewLine);
                var stringXData = dataSet[0]["XData"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var stringYData = dataSet[0]["YData"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var xData = Array.ConvertAll(stringXData, double.Parse);
                var yData = Array.ConvertAll(stringYData, double.Parse);
                var graph = GenerateReportGraph(fileName, xData, yData);
            }
            else
                File.AppendAllText(fileName, BatchNo + tabShift + GearNo + tabShift + dataSet[0][Constants.HIGHEST] + tabShift + defaultString + tabShift + "UnVerified" + Environment.NewLine);

        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private ChartView GenerateReportGraph(string fileName, double[] x, double[] y)
        {
            ChartView _chartview = null;
            try
            {
                Graph _lineGraph = new Graph();
                _chartview = _lineGraph.DrawGraph(x, y);
                if (_chartview != null)
                {
                    BufferedImage objImage = new BufferedImage(_chartview);
                    Image GraphImage = (Image)objImage.GetBufferedImage();
                    GraphImage.Save(@"C:\testimage.jpeg");
                    //byte[] byteImageData = ImageToByte(GraphImage);
                    
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
            return _chartview;
        }
    }
}
