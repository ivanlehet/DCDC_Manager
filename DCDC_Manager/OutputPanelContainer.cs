using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace DCDC_Manager
{
    public partial class OutputPanelContainer : UserControl
    {
        public String Header
        {
            get { return this.fileToolStripMenuItem.Text; }
            set {
                this.fileToolStripMenuItem.Text = value;
                this.sourceSateStatusStrip.Text = value;
            }
        }
        public const int ChartCtrlVisMinSize = 306;
        public const int ChartCtrlMinSize = 50;
        private String logFileName=String.Empty;
        private Output _output=new Output();

        public RealProperties Real
        {
            get { return _output.Real; }
            set
            {
                this._output.Real = value;
                Debug.WriteLine(this.GetType().Name + "\t Real voltage\t"+ this._output.Real.Voltage.ToString());

                this.realVoltage.Text = this._output.Real.Voltage.ToString()+" V";

                this.realCurrent.Text = this._output.Real.Current.ToString()+" A";

                this.realCurrentProgressBar.Value = (int)(this._output.Real.Current.Value * 9.0);
                this.realCurrentProgressInfoStrip.Value = this.realCurrentProgressBar.Value;

                this.realVoltageProgressBar.Value =(int) (this._output.Real.Voltage.Value*4.0);
                this.realVtgProgresInfoStrip.Value = this.realVoltageProgressBar.Value;


                if (this.showGraphLayout.Checked)
                {
                    Series[] points = { new Series(), new Series() };
                    for (int i = 0; i <= 1; i++)
                    {
                        if (chart.Series[i].Points.Count > 180)
                        {
                            for (int j = 0; j < 179; j++)
                            {
                                points[i].Points.Add(chart.Series[i].Points[j + 1]);
                            }

                            chart.Series[i].Points.Clear();
                            for (int k = 0; k < 179; k++)
                            {
                                chart.Series[i].Points.Add(points[i].Points[k]);
                            }

                        }
                    }

                    if (this.plotVoltage.Checked)
                    {
         
                        this.chart.Series[0].Points.AddXY(this.Real.Voltage.TimeStamp,this._output.Real.Voltage.Value);
                    }
                    if (this.plotCurrent.Checked)
                    {
                        this.chart.Series[1].Points.AddXY(this._output.Real.Current.TimeStamp,this._output.Real.Current.Value);
                    }
                }


            }
        }
        public DesiredProperties Desired
        {
            get { return this._output.Desired; }
            set {
                this._output.Desired = value;
                this.desiredVoltage.Value = this._output.Desired.Voltage.Value;
                this.desiredCurrent.Value= this._output.Desired.Current.Value;
            }
        }

        /// <summary>
        /// Indicates if output is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return _output.IsEnabled.Value; }
            set
            {
                this._output.IsEnabled.Value = value;
                this.EnabledChckBox.Checked = this._output.IsEnabled.Value;
                this.outEnableToolStripMenuItem.Checked = this._output.IsEnabled.Value;
                if (this._output.IsEnabled.Value)
                {

                    sourceSateStatusStrip.Image = Properties.Resources.switch_off_icon;

                    statusPanel.BackColor = Color.LimeGreen;

                }
                else
                {

                    sourceSateStatusStrip.Image = Properties.Resources.switch_on_icon;

                    statusPanel.BackColor = Color.Transparent;

                }
            }
        }



        public OutputPanelContainer()
        {
            InitializeComponent();
   

           


        }

        private void splitContainer7_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void showGraphLayout_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            this.chart.Visible = item.Checked;
            this.splitContainer.IsSplitterFixed = !item.Checked;

            if (item.Checked)
            {
                splitContainer.Panel2MinSize = OutputPanelContainer.ChartCtrlVisMinSize;
                if (splitContainer.Panel2.Width<OutputPanelContainer.ChartCtrlVisMinSize)
                {
                    splitContainer.SplitterDistance = splitContainer.Width - OutputPanelContainer.ChartCtrlMinSize;
                }
            }
            else
            {
                splitContainer.Panel2MinSize = OutputPanelContainer.ChartCtrlMinSize;
                splitContainer.SplitterDistance = splitContainer.Width - splitContainer.Panel2MinSize;
            }
        }

        private void EnabledChckBox_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox t = sender as CheckBox;
        
            this.IsEnabled = t.Checked;
        }

        private void outEnableToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {

            ToolStripMenuItem t = sender as ToolStripMenuItem;

            this.IsEnabled = t.Checked;
        }

        private void logOutput_CheckedChanged(object sender, EventArgs e)
        {
            logIconToolStripLabel.Visible ^= true;
            logFileNameToolStripLabel.Visible ^= true;
            if (logIconToolStripLabel.Visible)
            {
                logFileNameToolStripLabel.Text = logFileName;

            }
        }

        private void logFileNameMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "comma separated values (*.csv) | *.csv | txt files (*.txt) | *.txt | All files(*.*) | *.* ";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".csv";
            saveFileDialog1.FileName = "log";
            //saveFileDialog1.CheckFileExists = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                logFileName = saveFileDialog1.FileName;
                logFileNameMenuItem.Text = logFileName;
                try
                {
                    File.Create(logFileName);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "File create error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void voltageLogMenuItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void currentLogMenuItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void plotVoltage_CheckedChanged(object sender, EventArgs e)
        {
            plotLegend_CheckedChanged(sender, e);
            if(!plotVoltage.Checked)
            {
                chart.Series[0].Points.Clear();
            }
        }

        private void plotLegend_CheckedChanged(object sender, EventArgs e)
        {

            if (plotLegend.Checked)
            {


                if (plotVoltage.Checked)
                {
                    chart.Series[0].IsVisibleInLegend = true;
                }
                else
                {
                    chart.Series[0].IsVisibleInLegend = false;
                }
                if (plotCurrent.Checked)
                {
                    chart.Series[1].IsVisibleInLegend = true;
                }
                else
                {
                   chart.Series[1].IsVisibleInLegend = false;
                }
            }
            else
            {
                chart.Series[0].IsVisibleInLegend = false;
                chart.Series[0].IsVisibleInLegend = false;
            }
        }

        private void plotCurrent_CheckedChanged(object sender, EventArgs e)
        {
            plotLegend_CheckedChanged(sender, e);
            if (!plotCurrent.Checked)
            {
                chart.Series[1].Points.Clear();
            }
        }

        private void saveChartMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Comma separated values (*.csv)|";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".csv";
            
            saveFileDialog1.FileName = Header.Replace(' ','_').ToLower
                ()+"_chart_data";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {


                File.WriteAllText(saveFileDialog1.FileName, "");
                int length = chart.Series[0].Points.Count();
                if (length == 0)
                {

                    File.AppendAllText(saveFileDialog1.FileName, "No datas...\r\n");
                }
                else
                {
                    File.AppendAllText(saveFileDialog1.FileName, "Time;Value\r\n");

                    foreach (DataPoint point in chart.Series[0].Points)
                    {
                        File.AppendAllText(saveFileDialog1.FileName, point.XValue.ToString().Replace(',', '.') + ";" + point.YValues[0].ToString().Replace(',', '.') + "\r\n");
                    }

                }

            }
        }

        private void printChart_Click(object sender, EventArgs e)
        {
            chart.Printing.Print(true);
        }

        private void out1MainChartTitleTb_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(out1MainChartTitleTb.Text))
            {
                chart.Titles[0].Text = out1MainChartTitleTb.Text;
               
            }
        }
    }
}
