using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eicher
{
    class ReadReportData 
    {
        public List<Dictionary<string, string>> GetData(string FileName)
        {
            List<Dictionary<string, string>> dataSet = new List<Dictionary<string, string>>();
            Dictionary<string, string> DataForward = new Dictionary<string, string>();
            Dictionary<string, string> DataReverse = new Dictionary<string, string>();
            var lines = File.ReadAllLines(FileName);
            foreach (string line in lines)
            {
                var splitLine = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                var splitHeader = splitLine[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    switch (splitHeader[1].ToString().Trim())
                    {
                        case Constants.FORWARD: { DataForward.Add(splitHeader[0].ToString(), splitLine[1].ToString()); break; }
                        case Constants.REVERSE: { DataReverse.Add(splitHeader[0].ToString(), splitLine[1].ToString()); break; }
                    }
                }
                catch
                { }
            }
            dataSet.Add(DataForward);
            dataSet.Add(DataReverse);
            return dataSet;
        }
    }
}
