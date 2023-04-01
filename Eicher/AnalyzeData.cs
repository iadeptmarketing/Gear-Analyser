using System;

namespace Eicher
{
    class AnalyzeData
    {
        public string FetchStatus(string highestPeakValue)
        {
            if(string.IsNullOrEmpty(Common.AlarmValue))
            {
                //Highest Peak not set in alarm value
                throw new ApplicationException("Alarm Value not found in Settings");
            }
            if (Convert.ToDouble(highestPeakValue) > Convert.ToDouble(Common.AlarmValue))
            {
                return Constants.FAIL;
            }
            else
                return Constants.PASS;
        }

        internal bool IsGMFHigherThenRequired(string highestPeakValue)
        {
            if (string.IsNullOrEmpty(Common.AlarmValue))
            {
                //Highest Peak not set in alarm value
                throw new ApplicationException("Alarm Value not found in Settings");
            }
            if (Convert.ToDouble(highestPeakValue) > (Convert.ToDouble(Common.AlarmValue) * 2.5))
            {
                return true;
            }
            return false;
        }
    }
}
