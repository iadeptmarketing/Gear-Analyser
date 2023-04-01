using System;
using System.IO;
using System.Linq;

namespace Eicher
{
    class ReadData
    {
        int Number_Of_Spectrum = 0;
        bool Is2Channel = false;
        double[] XData = null;
        double[] YData = null;
        double[] YData2 = null;

        float RMS2 = 0;
        public float _RMS2
        {
            get
            {
                return RMS2;
            }
            set
            {
                RMS2 = value;
            }
        }

        float P_P = 0;
        public float _P_P
        {
            get
            {
                return P_P;
            }
            set
            {
                P_P = value;
            }
        }

        float P_P2 = 0;
        public float _P_P2
        {
            get
            {
                return P_P2;
            }
            set
            {
                P_P2 = value;
            }
        }
        double dF = 0;
        public double _dF
        {
            get
            {
                return dF;
            }
            set
            {
                dF = value;
            }
        }
        float RMS = 0;
        public float _RMS
        {
            get
            {
                return RMS;
            }
            set
            {
                RMS = value;
            }
        }

        int Window = 0;
        public int _Window
        {
            get
            {
                return Window;
            }
            set
            {
                Window = value;
            }
        }

        int Window2 = 0;
        public int _Window2
        {
            get
            {
                return Window2;
            }
            set
            {
                Window2 = value;
            }
        }

        int pwr2 = 0;
        public int _pwr2
        {
            get
            {
                return pwr2;
            }
            set
            {
                pwr2 = value;
            }
        }

        int Measurement = 0;
        public int _Measurement
        {
            get
            {
                return Measurement;
            }
            set
            {
                Measurement = value;
            }
        }

        int Measurement2 = 0;
        public int _Measurement2
        {
            get
            {
                return Measurement2;
            }
            set
            {
                Measurement2 = value;
            }
        }

        int ChnlA = 0;
        public int _ChnlA
        {
            get
            {
                return ChnlA;
            }
            set
            {
                ChnlA = value;
            }
        }

        int ChnlB = 0;
        public int _ChnlB
        {
            get
            {
                return ChnlB;
            }
            set
            {
                ChnlB = value;
            }
        }



        int Trig = 0;
        public int _Trig
        {
            get
            {
                return Trig;
            }
            set
            {
                Trig = value;
            }
        }

        int Avgm = 0;
        public int _Avgm
        {
            get
            {
                return Avgm;
            }
            set
            {
                Avgm = value;
            }
        }


        int Navg = 0;
        public int _Navg
        {
            get
            {
                return Navg;
            }
            set
            {
                Navg = value;
            }
        }

        int EPC = 0;
        public int _EPC
        {
            get
            {
                return EPC;
            }
            set
            {
                EPC = value;
            }
        }

        enum ampmode { Mode_A, Mode_V, Mode_S, Mode_E };
        int Ampmode = 0;
        public int _Ampmode
        {
            get
            {
                return Ampmode;
            }
            set
            {
                Ampmode = value;
            }
        }

        int Ampmode2 = 0;
        public int _Ampmode2
        {
            get
            {
                return Ampmode2;
            }
            set
            {
                Ampmode2 = value;
            }
        }

        int Amphpf = 0;
        public int _Amphpf
        {
            get
            {
                return Amphpf;
            }
            set
            {
                Amphpf = value;
            }
        }

        int Amphpf2 = 0;
        public int _Amphpf2
        {
            get
            {
                return Amphpf2;
            }
            set
            {
                Amphpf2 = value;
            }
        }

        enum ampenvcr { KTu_2, KTu_4, KTu_8, KTu_16, KTu_32 };
        int Ampenvcr = 0;
        public int _Ampenvcr
        {
            get
            {
                return Ampenvcr;
            }
            set
            {
                Ampenvcr = value;
            }
        }

        int Ampenvcr2 = 0;
        public int _Ampenvcr2
        {
            get
            {
                return Ampenvcr2;
            }
            set
            {
                Ampenvcr2 = value;
            }
        }

        float Sens = 0;
        public float _Sens
        {
            get
            {
                return Sens;
            }
            set
            {
                Sens = value;
            }
        }

        float Sens2 = 0;
        public float _Sens2
        {
            get
            {
                return Sens2;
            }
            set
            {
                Sens2 = value;
            }
        }

        ulong SerialN = 0;
        public ulong _SerialN
        {
            get
            {
                return SerialN;
            }
            set
            {
                SerialN = value;
            }
        }

        int Revision = 0;
        public int _Revision
        {
            get
            {
                return Revision;
            }
            set
            {
                Revision = value;
            }
        }
        public void ReadFFTData()
        {

        }

        private void GetByteData(string FileName)
        {
            using (FileStream objInput = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                byte[] MainArr = new byte[(int)objInput.Length];
                int contents = objInput.Read(MainArr, 0, (int)objInput.Length);
                if (Directory.Exists("c:\\vvtemp\\"))
                {
                    Directory.Delete("c:\\vvtemp\\", true);
                }
                ExtractData(MainArr);
                string[] arrFilePath = FileName.ToString().Split(new string[] { "\\", ".fft" }, StringSplitOptions.RemoveEmptyEntries);
                CalculateAllData();


            }
        }

        private void ExtractData(byte[] MainArr)
        {
            int CtrToStart = 0;
            byte[] fs = new byte[2];
            Is2Channel = false;
            try
            {
                //List<byte> arrByte = new List<byte>();
                //foreach (byte btm in MainArr)
                //{
                //    arrByte.Add(Convert.ToByte(Common.DeciamlToHexadeciaml1(Convert.ToInt32(btm))));
                //}

                //Reading Buffer  cnt
                fs[0] = MainArr[CtrToStart];
                fs[1] = MainArr[CtrToStart + 1];
                string byteval = fs[0].ToString() + fs[1].ToString();
                int ival = Common.HexadecimaltoDecimal(byteval);
                int BufferCNT = ival;

                //Reading buf1  --- 1 buffer size (currently 238 bytes)
                CtrToStart = 2;
                fs = new byte[2];
                fs[0] = MainArr[CtrToStart];
                fs[1] = MainArr[CtrToStart + 1];
                byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[0].ToString())) + Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[1].ToString()));
                ival = Common.HexadecimaltoDecimal(byteval);
                int Buf1 = ival;
                //int Buf1 = 238;

                //Reading buf2  --- 2 buffer size depends on the count of the spectral lines or sample length * (t)
                CtrToStart = 4;
                fs = new byte[2];
                fs[0] = MainArr[CtrToStart];
                fs[1] = MainArr[CtrToStart + 1];
                byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[0].ToString())) + Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[1].ToString()));
                ival = Common.HexadecimaltoDecimal(byteval);
                int Buf2 = ival;

                //Reading buf3  ---=0  if one channel---- 3 buffer size depends on the count of the spectral lines or sample length * (t)
                CtrToStart = 6;
                fs = new byte[2];
                fs[0] = MainArr[CtrToStart];
                fs[1] = MainArr[CtrToStart + 1];
                byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[0].ToString())) + Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[1].ToString()));
                ival = Common.HexadecimaltoDecimal(byteval);
                int Buf3 = ival;
                if (Buf3 > 0)
                {
                    Is2Channel = true;
                }

                //Reading LinesFFT  ---100, 200, 400, 800, 1600, 3200, 6400 ---- The number of spectral lines () - throwback - can take the value of the structure device [ ]
                CtrToStart = 8;
                fs = new byte[2];
                fs[0] = MainArr[CtrToStart];
                fs[1] = MainArr[CtrToStart + 1];
                byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[0].ToString())) + Common.DeciamlToHexadeciaml1(Convert.ToInt32(fs[1].ToString()));
                ival = Common.HexadecimaltoDecimal(byteval);
                int LinesFFT = ival;

                //Reading device[238 byte]  ---             //Reading device[238 byte]  --- 100, 200, 400, 800, 1600, 3200, 6400 ---- The number of spectral lines () - throwback - can take the value of the structure device [ ]
                // CtrToStart = 1660;
                CtrToStart = 10;
                fs = new byte[238];
                byteval = null;
                //int[] devicedata = new int[238];
                byte[] devicedata = new byte[238];
                for (int i = 0; i < 238; i++, CtrToStart++)
                {
                    //byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(MainArr[CtrToStart].ToString()));
                    //ival = Common.HexadecimaltoDecimal(byteval);
                    //devicedata[i] = ival;   
                    devicedata[i] = MainArr[CtrToStart];

                }

                GetDevicestructure(devicedata);
                // CtrToStart = 248;









                //Reading ch1 float FFT[size] or int   F(t)[size] ---- CH1 or range of functions. time
                CtrToStart = 248;
                fs = new byte[Buf2];
                byteval = null;
                //int[] CH1 = new int[Buf2];
                //List<float> CH1 = new List<float>();
                YData = new double[Number_Of_Spectrum];
                for (int i = 0; i < Number_Of_Spectrum; i++)
                {
                    //byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(MainArr[CtrToStart].ToString()));
                    //ival = Common.HexadecimaltoDecimal(byteval);
                    //CH1[i] = ival;

                    float fabc = BytetoFloat(MainArr, CtrToStart);

                    YData[i] = Convert.ToDouble(fabc);
                    //CH1.Add(fabc);
                    CtrToStart += 4;

                }

                if (Is2Channel)
                {
                    //Reading ch2 float FFT[size] or int   F(t)[size] ---- Channel2 range or function. time
                    CtrToStart = 248 + Buf2;
                    fs = new byte[Buf3];
                    byteval = null;
                    //int[] CH2 = new int[Buf3];
                    //for (int i = 0; i < Buf3; i++, CtrToStart++)
                    //{
                    //    byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(MainArr[CtrToStart].ToString()));
                    //    ival = Common.HexadecimaltoDecimal(byteval);
                    //    CH2[i] = ival;
                    //}
                    YData2 = new double[Number_Of_Spectrum];
                    for (int i = 0; i < Number_Of_Spectrum; i++)
                    {
                        //byteval = Common.DeciamlToHexadeciaml1(Convert.ToInt32(MainArr[CtrToStart].ToString()));
                        //ival = Common.HexadecimaltoDecimal(byteval);
                        //CH1[i] = ival;

                        float fabc = BytetoFloat(MainArr, CtrToStart);

                        YData2[i] = Convert.ToDouble(fabc);
                        //CH1.Add(fabc);
                        CtrToStart += 4;

                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private float BytetoFloat(byte[] MainArr, int CtrToStart)
        {
            float returnfloat = 0;
            try
            {
                byte[] newbyte = new byte[4];
                newbyte[0] = MainArr[CtrToStart];
                newbyte[1] = MainArr[CtrToStart + 1];
                newbyte[2] = MainArr[CtrToStart + 2];
                newbyte[3] = MainArr[CtrToStart + 3];

                newbyte = newbyte.Reverse().ToArray();

                returnfloat = BitConverter.ToSingle(newbyte, 0);
            }
            catch
            {
            }
            return returnfloat;
        }
        private void GetDevicestructure(byte[] devicedata)
        {
            int ctrStructure = 0;
            try
            {
                //Read RMS
                _RMS = BytetoFloat(devicedata, ctrStructure);
                if (Is2Channel)
                {
                    ctrStructure = 4;
                    _RMS2 = BytetoFloat(devicedata, ctrStructure);
                }

                //Read P_P
                ctrStructure = 12;
                _P_P = BytetoFloat(devicedata, ctrStructure);
                if (Is2Channel)
                {
                    ctrStructure = 16;
                    _P_P2 = BytetoFloat(devicedata, ctrStructure);
                }

                //Read dF
                ctrStructure = 24;
                _dF = Math.Round(Convert.ToDouble(BytetoFloat(devicedata, ctrStructure)), 3);

                //Read Window
                ctrStructure = 54;
                _Window = Convert.ToInt32(devicedata[ctrStructure].ToString());
                if (Is2Channel)
                {
                    ctrStructure = 55;
                    _Window2 = Convert.ToInt32(devicedata[ctrStructure].ToString());
                }

                //Read pwr2
                ctrStructure = 57;
                _pwr2 = Convert.ToInt32(devicedata[ctrStructure].ToString());

                //Read Measurement
                ctrStructure = 58;
                _Measurement = Convert.ToInt32(devicedata[ctrStructure].ToString());
                if (Is2Channel)
                {
                    ctrStructure = 59;
                    _Measurement2 = Convert.ToInt32(devicedata[ctrStructure].ToString());
                }

                //Read channel A
                ctrStructure = 61;
                _ChnlA = Convert.ToInt32(devicedata[ctrStructure].ToString());

                if (_Measurement == 0) // For Time
                {
                    Number_Of_Spectrum = 1 << _pwr2;
                }
                else if (_Measurement == 1) // For FFT
                {
                    Number_Of_Spectrum = (1 << (_pwr2 - 6)) * 25;
                }

                HighestFrequency = dF * Number_Of_Spectrum;
                XData = new double[Number_Of_Spectrum];
                for (int i = 0; i < Number_Of_Spectrum; i++)
                {
                    XData[i] = Convert.ToDouble(dF * i);
                }

                //Read channel B
                ctrStructure = 62;
                _ChnlB = Convert.ToInt32(devicedata[ctrStructure].ToString());


                //Read Trigger
                ctrStructure = 63;
                _Trig = Convert.ToInt32(devicedata[ctrStructure].ToString());


                //Read Averaging Mode
                ctrStructure = 64;
                _Avgm = Convert.ToInt32(devicedata[ctrStructure].ToString());


                //Read Averaging Number
                ctrStructure = 65;
                _Navg = Convert.ToInt32(devicedata[ctrStructure].ToString());


                //Read EPC
                ctrStructure = 66;
                _EPC = Convert.ToInt32(devicedata[ctrStructure].ToString());


                //Read Amplifier Mode
                ctrStructure = 70;
                _Ampmode = Convert.ToInt32(devicedata[ctrStructure].ToString());
                if (Is2Channel)
                {
                    ctrStructure = 71;
                    _Ampmode2 = Convert.ToInt32(devicedata[ctrStructure].ToString());
                }

                //Read Low Frequency Cut Off
                ctrStructure = 76;
                _Amphpf = Convert.ToInt32(devicedata[ctrStructure].ToString());
                if (Is2Channel)
                {
                    ctrStructure = 77;
                    _Amphpf2 = Convert.ToInt32(devicedata[ctrStructure].ToString());
                }

                //Read Carrier for channel
                ctrStructure = 79;
                _Ampenvcr = Convert.ToInt32(devicedata[ctrStructure].ToString());
                if (Is2Channel)
                {
                    ctrStructure = 80;
                    _Ampenvcr2 = Convert.ToInt32(devicedata[ctrStructure].ToString());
                }

                //Read Transducer factor/Sensitivity
                ctrStructure = 90;
                _Sens = BytetoFloat(devicedata, ctrStructure);
                if (Is2Channel)
                {
                    ctrStructure = 94;
                    _Sens2 = BytetoFloat(devicedata, ctrStructure);
                }
                ctrStructure = 226;
                ulong serialNumber = Bytetoulong(devicedata, ctrStructure);
            }
            catch (Exception ex)
            {
            }
        }
        private ulong Bytetoulong(byte[] MainArr, int CtrToStart)
        {
            ulong returnval = 0;
            try
            {
                byte[] newbyte = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    newbyte[i] = MainArr[CtrToStart + i];
                }

                newbyte = newbyte.Reverse().ToArray();

                returnval = BitConverter.ToUInt64(newbyte, 0);
            }
            catch
            {
            }
            return returnval;
        }
        double HighestFrequency = 0;
        public double _highestFreq
        {
            get
            {
                return HighestFrequency;
            }
            set
            {
                HighestFrequency = value;
            }
        }
    }
}
