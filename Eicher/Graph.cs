using com.iAM.chart2dnet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Eicher
{
    class Graph : ChartView
    {
        ChartAttribute attrib1 = null;
        NumericLabel objLabel = null;
        Background background = null;
        Font theFont;
        SimpleLinePlot thePlot1 = null;


        public ChartView chart
        {
            get; set;
        }
        public string XLabel
        {
            get; set;
        } = "Hz";
        public string YLabel
        {
            get; set;
        } = "Test";

        public List<double> GMXValue { get; set; }
        public ChartView DrawGraph(double[] xdata, double[] ydata)
        {
            ChartView chartVu = this;
            try
            {
                
                DrawGraph(xdata, ydata, chartVu);
            }
            catch(Exception ex)
            {
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
            return chartVu;
        }

        private void DrawGraph(double[] xdata, double[] ydata, ChartView chartVu)
        {
            try
            {
                CartesianCoordinates pTransform1 = null;
                SimpleDataset[] datasetarray = null;
                LinearAxis xAxis = null;
                LinearAxis yAxis = null;
                NumericAxisLabels xAxisLab = null;
                NumericAxisLabels yAxisLab = null;
                Grid xgrid = null;
                Grid ygrid = null;
                AxisTitle objXTitle = null;
                AxisTitle objYTitle = null;
                theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                SimpleDataset Dataset1 = null;
                Dataset1 = new SimpleDataset("First", (double[])xdata, (double[])ydata);
                datasetarray = new SimpleDataset[1];
                pTransform1 = new CartesianCoordinates();
                double[] FindLength = (double[])xdata;
                datasetarray[0] = Dataset1.CompressSimpleDataset(GraphObj.DATACOMPRESS_MAX, GraphObj.DATACOMPRESS_MAX, 1, 0, FindLength.Length - 1, "NewData");
                pTransform1.AutoScale(datasetarray, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_EXACT);
                pTransform1.SetGraphBorderDiagonal(0.15, .1, .85, 0.8);
                
                Color objBackgroundColor = Color.FromArgb(241, 241, 247);
                background = new Background(pTransform1, ChartObj.BACKGROUND_RECTANGLE, objBackgroundColor);

                chartVu.AddChartObject(background);
                background = new Background(pTransform1, ChartObj.BACKGROUND, Color.LightBlue);

                chartVu.AddChartObject(background);

                xAxis = new LinearAxis(pTransform1, ChartObj.X_AXIS);
                xAxis.CalcAutoAxis();
                chartVu.AddChartObject(xAxis);

                objXTitle = new AxisTitle(xAxis, theFont, XLabel);
                chartVu.AddChartObject(objXTitle);

                yAxis = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                yAxis.CalcAutoAxis();
                chartVu.AddChartObject(yAxis);
                objYTitle = new AxisTitle(yAxis, theFont, YLabel);
                chartVu.AddChartObject(objYTitle);

                //xgrid = new Grid(xAxis, yAxis, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                //xgrid.SetColor(Color.Black);
                //chartVu.AddChartObject(xgrid);

                //ygrid = new Grid(xAxis, yAxis, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                //ygrid.SetColor(Color.Black);
                //chartVu.AddChartObject(ygrid);

                xAxisLab = new NumericAxisLabels(xAxis);
                xAxisLab.SetColor(Color.Black);
                chartVu.AddChartObject(xAxisLab);

                yAxisLab = new NumericAxisLabels(yAxis);
                yAxisLab.SetColor(Color.Black);
                chartVu.AddChartObject(yAxisLab);

                //objXAxisTicks = new XAxisTicks();
                //objXAxisTicks.Visible = true;
                //objXAxisTicks.SetXAxisTicksWidth(2);

                //objYAxisTicks = new YAxisTicks();
                //objYAxisTicks.Visible = true;
                //objYAxisTicks.SetYAxisTicksWidth(2);

                Color transparentRed = Color.FromArgb(200, 255, 0, 0);
                Color transparentGreen = Color.FromArgb(200, 0, 255, 0);
                try
                {
                    foreach (var v in GMXValue)
                    {
                        Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, v, 0, 8, 1);
                        //m_objMarker1.LineColor = Color.Green;
                        m_objMarker1.LineWidth = 1.5;
                        chartVu.AddChartObject(m_objMarker1);
                    }
                }
                catch
                { }
                attrib1 = new ChartAttribute(Color.DarkRed, 1, DashStyle.Solid, transparentRed);
                thePlot1 = new SimpleLinePlot(pTransform1, datasetarray[0], attrib1);

                objLabel = new NumericLabel();
                objLabel.SetNumericValue(1);
                thePlot1.SetPlotLabelTemplate(objLabel);

                thePlot1.PlotLabelTemplate.SetTextBgMode(true);
                thePlot1.PlotLabelTemplate.SetTextString("FFT");


                chartVu.AddChartObject(thePlot1);
                chartVu.UpdateDraw();
                chart = chartVu;
            }
            catch (Exception ex)
            {
                ErrorHandler.AddLog(ex.Message, ex.StackTrace);
            }
        }

        public void DrawGraph(List<List<double>> DataXY)
        {
            double[] xdata = DataXY[0].ToArray(); double[] ydata = DataXY[1].ToArray();
            ChartView chartVu = this;            
            DrawGraph(xdata, ydata, chartVu);
        }

        public void DrawGraph(List<double> DataX, List<double> DataY)
        {
            double[] xdata = DataX.ToArray(); double[] ydata = DataY.ToArray();
            ChartView chartVu = this;
            DrawGraph(xdata, ydata, chartVu);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Graph));
            this.SuspendLayout();
            // 
            // Graph
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Name = "Graph";
            this.PreferredSize = new System.Drawing.Size(424, 272);
            this.ResumeLayout(false);

        }
    }
}
