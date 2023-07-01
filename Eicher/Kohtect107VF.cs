using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Eicher
{
    public sealed class Kohtect107VF : Instrument
    {
        private static Kohtect107VF instance = null;
        private static readonly object padlock = new object();

        public static Kohtect107VF Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Kohtect107VF();
                    }
                    return instance;
                }
            }
        }
        Kohtect107VF()
        {

        }
        public string latestFile { get; set; }
        public string lastFile { get; set; }
        public override bool CopyFile()
        {

            if (CheckIfFileExist())
            {
                CopyFileFromDevice(latestFile);
                return true;
            }
            return false;
        }

        private void CopyFileFromDevice(string latestFile)
        {
            if (!Directory.Exists(Constants.LOCALTEMPFOLDER))
            {
                Directory.CreateDirectory(Constants.LOCALTEMPFOLDER);
            }
            File.Copy(latestFile, Constants.LOCALFILEPATH,true);
        }

        private bool CheckIfFileExist()
        {
            lastFile = latestFile;
            List<string> drivesName = getDriveForKohtect();
            if(drivesName.Count == 0)
            {
                return false;
            }
            DirectoryInfo d = new DirectoryInfo(drivesName[0].ToString());
            DateTime lastUpdated = DateTime.MinValue;
            foreach (var fileInfo in d.GetFiles("*f.fft"))
            {
                if (fileInfo.LastWriteTime > lastUpdated)
                {
                    lastUpdated = fileInfo.LastWriteTime;
                    latestFile = fileInfo.FullName;
                }
            }
            if (lastFile == latestFile)
            {
                throw new Exception("New data not saved in instrument.");
            }
            return true;
        }

        public List<string> getDriveForKohtect()
        {
            List<string> drives = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && d.DriveType != DriveType.Fixed)
                {
                    if (d.VolumeLabel == "107VF" )
                    {

                        string aa = d.Name;
                        string[] aa1 = aa.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                        drives.Add(aa1[0].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Instrument Drive not found. Please change your drive name with 107VF", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            return drives;
        }
    }
}
