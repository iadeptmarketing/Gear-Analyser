using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eicher
{
    class GearAnalysis
    {
        private double _iRpmMargin;
        List<Peak> _peaks;
        private List<double> _peakOrder;
        private int _iRpm;
        private List<double> _peakOrderGmf;
        private List<double> _peakOrderPinion;

        public double GM1Value { get; set; }
        public double GM2Value { get; set; }
        public double GM3Value { get; set; }
        public double GM4Value { get; set; }
        public double GM5Value { get; set; }
        public double GM6Value { get; set; }

        public double GM1CH2Value { get; set; }
        public double GM2CH2Value { get; set; }
        public double GM3CH2Value { get; set; }
        public double GM4CH2Value { get; set; }
        public double GM5CH2Value { get; set; }
        public double GM6CH2Value { get; set; }

        public double GM1CH3Value { get; set; }
        public double GM2CH3Value { get; set; }
        public double GM3CH3Value { get; set; }
        public double GM4CH3Value { get; set; }
        public double GM5CH3Value { get; set; }
        public double GM6CH3Value { get; set; }

        public List<double> GMXValue { get; set; }
       
        public void Analyse(List<List<double>> dataXY, int rpm, int gearCount, int pinionCount)
        {
            _iRpm = rpm;
            _iRpmMargin = rpm * .02 > 100 ? 100  : rpm * .02 ;
            CalculatePeaksAndOrder(dataXY[0].ToArray(), dataXY[1].ToArray());
            CalculateGMF(gearCount, pinionCount);
        }

        public void Analyse(List<double> dataX, List<double> dataY, int rpm, int gearCount, int pinionCount)
        {
            _iRpm = rpm;
            _iRpmMargin = rpm * .02 > 100 ? 100 : rpm * .02;
            CalculatePeaksAndOrder(dataX.ToArray(), dataY.ToArray());
            CalculateGMF(gearCount, pinionCount);
        }
        private void CalculateGMF(int GearTooth, int PinionTooth)
        {
            ////Find 1X order/x value for further analysis
            //int highestPeakX = (int)(_peaks[0].PeakLocation * 60);
            //_highestPeakMargin = 1 / (double)highestPeakX;

            int PinionRpm = (GearTooth / PinionTooth) * _iRpm;

            //calculating Gear Mesh Frequency
            int gmf = _iRpm * GearTooth;
            _peakOrderGmf = new List<double>();
            int frequencyPinion = PinionRpm * PinionTooth;
            _peakOrderPinion = new List<double>();
            GM1Value = 0;
            GM2Value = 0;
            GM3Value = 0;
            GM4Value = 0;
            GM5Value = 0;
            GM6Value = 0;
            GMXValue = new List<double>();
            foreach (var peak in _peaks)
            {
                double vf = Math.Round(peak.PeakLocation * 60 / gmf, 2);
                if (0.98 < vf && vf <= 1.02)
                {
                    if (GM1Value < peak.PeakAmplitude)
                    {
                        GM1Value = peak.PeakAmplitude;
                        GMXValue.Add(peak.PeakLocation);
                    }
                }
                if (1.98 < vf && vf <= 2.02)
                {
                    if (GM2Value < peak.PeakAmplitude)
                    { 
                        GM2Value = peak.PeakAmplitude;
                        GMXValue.Add(peak.PeakLocation);
                    }
                }
                if (2.98 < vf && vf <= 3.02)
                {
                    if (GM3Value < peak.PeakAmplitude)
                    { 
                        GM3Value = peak.PeakAmplitude;
                        GMXValue.Add(peak.PeakLocation);
                    }
                }
                if (3.98 < vf && vf <= 4.02)
                {
                    if (GM4Value < peak.PeakAmplitude)
                    { 
                        GM4Value = peak.PeakAmplitude;
                        GMXValue.Add(peak.PeakLocation);
                    }
                }
                if (4.98 < vf && vf <= 5.02)
                {
                    if (GM5Value < peak.PeakAmplitude)
                    { 
                        GM5Value = peak.PeakAmplitude;
                        GMXValue.Add(peak.PeakLocation);
                    }
                }
                if (5.98 < vf && vf <= 6.02)
                {
                    if (GM6Value < peak.PeakAmplitude)
                    { 
                        GM6Value = peak.PeakAmplitude;
                        GMXValue.Add(peak.PeakLocation);
                    }
                }




                _peakOrderGmf.Add(Math.Round(peak.PeakLocation * 60 / gmf, 2));
                //_peakOrderPinion.Add(Math.Round(peak.PeakLocation * 60 / frequencyPinion, 2));
            }
        }

        private void CalculatePeaksAndOrder(double[] _dXVals, double[] _dYVals)
        {
            _peaks = new List<Peak>();
            _peakOrder = new List<double>();
            try
            {
                double fst;
                double scnd;
                double thrd;

                //Fetching the peeks
                try
                {
                    for (int i = 2; i < _dYVals.Length; i++)
                    {
                        fst = _dYVals[i - 2];
                        scnd = _dYVals[i - 1];
                        thrd = _dYVals[i];

                        if (fst < scnd && scnd > thrd)
                        {
                            _peaks.Add(new Peak() { PeakAmplitude = _dYVals[i - 1], PeakLocation = _dXVals[i - 1] });
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.AddLog(ex.Message, ex.StackTrace);
                }

                //Sorting Peaks in ascending order based on amplitude
                _peaks.Sort();
                //Rearranging Peeks in descending order based on amplitude
                _peaks.Reverse();

                //Getting highest Peak x (in CPM/RPM) and y
                double highestPeakX = (_peaks[0].PeakLocation * 60);
                double highestPeakY = _peaks[0].PeakAmplitude;

                //Finalizing RPM
                if (_iRpm <= 1)
                {
                    if ((_iRpm - highestPeakX) < Convert.ToInt32(_iRpmMargin * 60))
                    {
                        _iRpm = (int)highestPeakX;
                    }
                }
                else if (Convert.ToInt32(_iRpmMargin * _iRpm) > 0)
                {
                    var rpmMarginLower = _iRpm - Convert.ToInt32(_iRpmMargin);
                    var rpmMarginHigher = _iRpm + Convert.ToInt32(_iRpmMargin);
                    if (Enumerable.Range(rpmMarginLower, rpmMarginHigher - rpmMarginLower).Contains((int)highestPeakX))
                    {
                        _iRpm = (int)highestPeakX;
                    }
                }
                if (_iRpm <= 1)
                {
                    throw new ApplicationException("Unable to calculate RPM values. Kindly contact administrator");
                }
                _iRpmMargin = _iRpmMargin == 1 ? _iRpmMargin / _iRpm : _iRpmMargin;
                //Maintaining limited peaks subjected to 1 / 5th of the highest peak
                //double rangeValue = highestPeakY / 5;
                //_peaks.RemoveAll(x => x.PeakAmplitude < rangeValue);

                foreach (var peak in _peaks)
                {
                    _peakOrder.Add(Math.Round(peak.PeakLocation * 60 / _iRpm, 2));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
        }

    }
}
