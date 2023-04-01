using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trial6;

namespace Eicher
{
    public sealed class Kohtect107TXV : Instrument
    {
        private static Kohtect107TXV instance = null;
        private static readonly object padlock = new object();

        public static Kohtect107TXV Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Kohtect107TXV();
                    }
                    return instance;
                }
            }
        }
        Kohtect107TXV()
        {

        }
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
            if (DeviceConnection == null)
            {
                DeviceConnection = new RAPI();
            }
            DeviceConnection.CopyFileFromDevice(Constants.LOCALFILEPATH, deviceFolder + latestFile, true);
        }

        string deviceFolder = @"\Storage Card\";

        //public void CopyFilesFromDevice(string localStartingDirectory,
        //          string deviceStartingDirectory,
        //          bool includeSubDirectories,
        //          bool overwrite)
        //{
        //    RAPI m_objRAPI = new RAPI();
        //    m_objRAPI.Connect();
        //    if(!Directory.Exists(localStartingDirectory))
        //    {
        //        Directory.CreateDirectory(localStartingDirectory);
        //    }
        //    FileList deviceDirectory = m_objRAPI.EnumFiles(deviceFolder);
        //    if (deviceDirectory == null || deviceDirectory.Count != 1)
        //    {
        //        throw new System.IO.FileNotFoundException("Invalid Device Directory",
        //                  deviceStartingDirectory);
        //    }

        //    FileList directoryList = m_objRAPI.EnumFiles(deviceFolder + "/*.fft");

        //    foreach (FileInformation dirInfo in directoryList)
        //    {
        //        if (dirInfo.FileAttributes == (int)FileAttributes.Directory)
        //        {
        //            if (!includeSubDirectories) continue;

        //            string newDeviceDirectory =
        //                deviceStartingDirectory + "\\" + dirInfo.FileName;
        //            string newLocalDirectory =
        //                localStartingDirectory + "\\" + dirInfo.FileName;
        //            CopyFilesFromDevice(newLocalDirectory,
        //                newDeviceDirectory,  includeSubDirectories, overwrite);
        //        }
        //        else
        //        {
        //            string newDeviceFile =
        //                deviceStartingDirectory + "\\" + dirInfo.FileName;
        //            string newLocalFile =
        //                localStartingDirectory + "\\" + dirInfo.FileName;
        //            //CopyFileFromDevice(newLocalFile, newDeviceFile, overwrite);
        //        }
        //    }
        //}
        public string latestFile { get; set; }
        public string lastFile { get; set; }
        public RAPI DeviceConnection { get; set; } = new RAPI();
        bool CheckIfFileExist()
        {
            lastFile = latestFile;
            using (DeviceConnection = new RAPI())
            {
                if (DeviceConnection.DevicePresent)
                {
                    DeviceConnection.Connect();
                    var files = DeviceConnection.EnumFiles(deviceFolder + "/*.fft");
                    if (files == null)
                    {
                        return false;
                    }
                    //var files = new DirectoryInfo(deviceFolder).GetFiles(".");
                    latestFile = "";

                    DateTime lastUpdated = DateTime.MinValue;
                    foreach (FileInformation fileInfo in files)
                    {

                        if (fileInfo.LastWriteTime > lastUpdated)
                        {
                            lastUpdated = fileInfo.LastWriteTime;
                            latestFile = fileInfo.FileName;
                        }
                    }
                    if (lastFile == latestFile)
                    {
                        throw new Exception("New data not saved in instrument.");
                    }
                    return true;

                }
            }
            return false;
        }
    }
}
