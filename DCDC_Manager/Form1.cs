using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;


namespace DCDC_Manager
{

    public partial class Form1 : Form
    {

        PowerSupply p = new PowerSupply();
        
        
          

        String message;
        String logFileName = String.Empty; 
        double function(double x)
        {
            return (Math.Pow(Math.Sin(x), 2) * 13.6);
        }

        public Form1()
        {


            InitializeComponent();
            device_serial.NewLine = "\r";
            out1MainChartTitleTb.TextChanged += Out1MainChartTitleTb_TextChanged;
            /*
            battTrendsChart.ChartAreas[0].AxisY.Maximum = 15;
            battTrendsChart.ChartAreas[0].AxisY.Minimum = 0;
            for (double i = 0; i < 500; i+=50)
            {
                battTrendsChart.Series[0].Points.AddXY(i,function(i));
             }
          */
        }

        private void cONNECTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connGb.Visible = true;
            mainPageContainer.Visible = false;
        }

        private void connBackBtn_Click(object sender, EventArgs e)
        {
            connGb.Visible = false;
            mainPageContainer.Visible = true;
        }

        private void connGb_VisibleChanged(object sender, EventArgs e)
        {
            if (connGb.Visible)
            {


                fillComList(comPresentListBox); //fill com ports names list


                comPortProperty.SelectedObject = device_serial; //show serial port properties
            }
        }


        /// <summary>
        /// Connecting to the Power supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connBtn_Click(object sender, EventArgs e)
        {
            if (!device_serial.IsOpen)
            {
                try
                {
                    IsValidPortName(device_serial.PortName);
                    device_serial.Open();
                    if (device_serial.IsOpen)
                    {
                        statusStrip1.Items.Add(device_serial.PortName);
                        statusStrip1.Items[0].ForeColor = SystemColors.Control;
                        valuesRefresTimer.Enabled = true;
                       device_serial.Write("com_star\xFF\xFF\xFF");
                        connBtn.Text = "DISCONNECT";
                        comPortProperty.Refresh();
                    }

                }
                catch (Exception ex)
                {
                    valuesRefresTimer.Enabled = false;
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                device_serial.Close();
                while (device_serial.IsOpen)
                {

                }
                if (!device_serial.IsOpen)
                {
                    statusStrip1.Items.RemoveAt(0);
                    connBtn.Text = "CONNECT";
                    valuesRefresTimer.Enabled = false;
                    comPortProperty.Refresh();
                }


            }

            ConsoleWrite(serialConsoleRtb, "Serial running: " + device_serial.IsOpen);
            //ConsoleWrite(serialConsoleRtb, "dim=50\x0FF\x0FF\x0FF");
            
          



        }

        private void ConsoleWrite(object sender, String message)
        {
            RichTextBox rtb = sender as RichTextBox;

                
                rtb.SelectionStart = 0;
                rtb.SelectionLength = 0;
                rtb.SelectedText += System.DateTime.Now.ToLongTimeString() + "\t";
                rtb.SelectedText += message;
                rtb.SelectedText += Environment.NewLine;
            
        }

        #region Custom methods
        public void fillComList(ListBox listBox)
        {
            listBox.Items.Clear();
            listBox.Items.AddRange(SerialPort.GetPortNames()); //vyhladaj porty COM, vloz do zoznamu portov
        }

        public bool IsValidPortName(String port_name)
        {
            if (String.IsNullOrEmpty(port_name) || String.IsNullOrWhiteSpace(port_name))
                throw new ArgumentNullException("Port name not set.");

            String pattern = @"COM[0-9]+";
            RegexOptions reg_opt = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
            Match result = Regex.Match(port_name, pattern, reg_opt);

            if (result.Success)
                return true;

            throw new ArgumentException(port_name + " is nvalid com port.");

        }

        /// <summary>
        /// Commpute point for simulated Square signal
        /// </summary>
        /// <param name="value">Time stamp</param>
        /// <param name="fourier_degree">Degree of Fourier series</param>
        /// <param name="offset">Point offset</param>
        /// <param name="amplitude">Point upper bound</param>
        /// <returns>Function returning commputed point of Square vawe fourier approximation.</returns>
        public double ComputeSquarePoint(double value, int fourier_degree, double offset, double amplitude)
        {
            double square=0;

            for (int i = 1; i < fourier_degree; i++)
            {
                square += ((1 - Math.Cos(i * Math.PI)) / (i * Math.PI)) * Math.Sin(i * Math.PI * value);
            }
            square += 0.5+offset;
            square = (amplitude > 0) ? square *= amplitude : square;
            return square;
        }

        /// <summary>
        ///Commpute point for simulated Square signal
        /// </summary>
        /// <param name="value">Time stamp</param>
        /// <param name="fourier_degree">Degree of Fourier series</param>
        /// <param name="offset">Point offset</param>
        /// <returns>Function returning commputed point of Square vawe fourier approximation.</returns>
        public double ComputeSquarePoint(double value, int fourier_degree, double offset)
        {
            return ComputeSquarePoint(value,fourier_degree,offset,1.0);
        }

        /// <summary>
        /// Function sets proper battery Icon
        /// </summary>
        /// <param name="buton">Button in which is object located.</param>
        /// <param name="capacity">Current battery capacity.</param>
        private void EnsureBatteryCapacity(Button  buton, double  capacity)
        {
            if (capacity>71)
            {
                buton.Image = DCDC_Manager.Properties.Resources.Status_battery_100_icon ;
            }
            else if (capacity>70&&capacity<=76)
            {
                buton.Image = Properties.Resources.Status_battery_080_icon;

            }
            else if (capacity>64&&capacity<=70)
            {
                buton.Image = Properties.Resources.Status_battery_060_icon;
            }
            else if (capacity>60&&capacity<=64)
            {
                buton.Image = Properties.Resources.Status_battery_040_icon;
            }
            else if (capacity>55&&capacity<=60)
            {
                buton.Image = Properties.Resources.Status_battery_caution_icon;
            }
            else if (capacity<=55)
            {
                buton.Image = Properties.Resources.Status_battery_low_icon;
            }
        }



        /// <summary>
        /// Resizing Output panel 'Plot page' if plot option is changed.
        /// </summary>
        /// <param name="container">Container in wich 'Plot page' is located</param>
        /// <param name="hide">Indicates Plot option check state.</param>
        private void ResizeOutputPanel(SplitContainer container, bool hide)
        {
            if (!hide)
            {
                container.SplitterDistance = container.Width - container.Panel2MinSize;
            }
            else
            {
               
                    container.SplitterDistance = container.Width - container.Panel2MinSize;
             
                
            }
        }

        #endregion

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void connGb_Enter(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void portRefBtn_Click(object sender, EventArgs e)
        {

            fillComList(comPresentListBox);
        }

        private void portApplyBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(comPresentListBox.Text))
            {
                try
                {
                    IsValidPortName(comPresentListBox.Text);
                    device_serial.PortName = comPresentListBox.Text;
                    comPortProperty.Refresh();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "COM port error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void splitContainer4_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void showTrendsChcBx_CheckedChanged(object sender, EventArgs e)
        {
            //battTrendsChart.Visible ^= true;
        }

        private void splitContainer7_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        

        #region Output 1 Plot checkboxes 

        /// <summary>
        /// Output 1 Plot checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        /// <summary>
        /// Output 1 Legend checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out1PlotLegendChckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Output 1 Plot Voltage checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out1PlotVtgChckBox_CheckedChanged(object sender, EventArgs e)
        {



        }

        /// <summary>
        /// Output 1 Plot Current checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out1PlotCurrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
          
        }
        #endregion

        #region Output 2 Plot checkboxes

        /// <summary>
        /// Output 2 Plot checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out2PlotChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out2MainValuesChart.Visible ^= true;
            out2PlotVtgChckBox.Visible ^= true;
            out2PlotCurrCheckBox.Visible ^= true;
            out2PlotLegendChckBox.Visible ^= true;
            splitContainer8.IsSplitterFixed ^= true;
           
            if (out2PlotChckBox.Checked)
            {
                splitContainer8.Panel2MinSize = 306;
            }
            else
            {
                splitContainer8.Panel2MinSize = 63;
            }

            ResizeOutputPanel(splitContainer8, out2PlotChckBox.Checked);
        }

        /// <summary>
        /// Output 2 Legend checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out2PlotLegendChckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (out2PlotLegendChckBox.Checked)
            {


                if (out2PlotVtgChckBox.Checked)
                {
                    out2MainValuesChart.Series["out2MainChartVoltage"].IsVisibleInLegend = true;
                }
                else
                {
                    out2MainValuesChart.Series["out2MainChartVoltage"].IsVisibleInLegend = false;
                }
                if (out2PlotCurrCheckBox.Checked)
                {
                    out2MainValuesChart.Series["out2MainChartCurrent"].IsVisibleInLegend = true;
                }
                else
                {
                    out2MainValuesChart.Series["out2MainChartCurrent"].IsVisibleInLegend = false;
                }
            }
            else
            {
                out2MainValuesChart.Series["out2MainChartVoltage"].IsVisibleInLegend = false;
                out2MainValuesChart.Series["out2MainChartCurrent"].IsVisibleInLegend = false;
            }
        }

        /// <summary>
        /// Output 2 Plot Voltage checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out2PlotVtgChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out2PlotLegendChckBox_CheckedChanged(sender, e);
            if (!out2PlotVtgChckBox.Checked)
            {
                out2MainValuesChart.Series["out2MainChartVoltage"].Points.Clear();
            }
        }

        /// <summary>
        /// Output 2 Plot Current checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out2PlotCurrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            out2PlotLegendChckBox_CheckedChanged(sender, e);
            if (!out2PlotCurrCheckBox.Checked)
            {
                out2MainValuesChart.Series["out2MainChartCurrent"].Points.Clear();
            }
        }

        #endregion

        #region Output 3 Plot checkboxes

        /// <summary>
        /// Output 3 Plot checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out3PlotChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out3MainValuesChart.Visible ^= true;
            out3PlotVtgChckBox.Visible ^= true;
            out3PlotCurrCheckBox.Visible ^= true;
            out3PlotLegendChckBox.Visible ^= true;
            splitContainer9.IsSplitterFixed ^= true;

            if (out3PlotChckBox.Checked)
            {
                splitContainer9.Panel2MinSize = 306;
            }
            else
            {
                splitContainer9.Panel2MinSize = 63;
            }
            ResizeOutputPanel(splitContainer9, out3PlotChckBox.Checked);

        }

        /// <summary>
        /// Output 3 Plot Voltage checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out3PlotVtgChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out3PlotLegendChckBox_CheckedChanged(sender, e);
            if (!out3PlotVtgChckBox.Checked)
            {
                out3MainValuesChart.Series["out3MainChartVoltage"].Points.Clear();
            }
        }

        /// <summary>
        /// Output 3 Plot Current checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out3PlotCurrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            out3PlotLegendChckBox_CheckedChanged(sender, e);
            if (!out3PlotCurrCheckBox.Checked)
            {
                out3MainValuesChart.Series["out3MainChartCurrent"].Points.Clear();
            }
                
        }

        /// <summary>
        /// Output 3 Legend checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out3PlotLegendChckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (out3PlotLegendChckBox.Checked)
            {


                if (out3PlotVtgChckBox.Checked)
                {
                    out3MainValuesChart.Series["out3MainChartVoltage"].IsVisibleInLegend = true;
                }
                else
                {
                    out3MainValuesChart.Series["out3MainChartVoltage"].IsVisibleInLegend = false;
                }
                if (out3PlotCurrCheckBox.Checked)
                {
                    out3MainValuesChart.Series["out3MainChartCurrent"].IsVisibleInLegend = true;
                }
                else
                {
                    out3MainValuesChart.Series["out3MainChartCurrent"].IsVisibleInLegend = false;
                }
            }
            else
            {
                out3MainValuesChart.Series["out3MainChartVoltage"].IsVisibleInLegend = false;
                out3MainValuesChart.Series["out3MainChartCurrent"].IsVisibleInLegend = false;
            }
        }
        #endregion

        #region Outputs enabling/disabling
        private void out1MainEnabledChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out1InfoEnabledChckBox.Checked = out1MainEnabledChckBox.Checked;
            toolStripMenuItem3.Checked = out1MainEnabledChckBox.Checked;

            if (out1MainEnabledChckBox.Checked)
            {

                toolStripStatusLabel1.Image = Properties.Resources.switch_off_icon;

                out1MainStatusPanel.BackColor = Color.LimeGreen;
                out1InfoStatusPanel.BackColor = Color.LimeGreen;
            }
            else
            {
            
                toolStripStatusLabel1.Image = Properties.Resources.switch_on_icon;

                out1MainStatusPanel.BackColor = Color.Transparent;
                out1InfoStatusPanel.BackColor = Color.Transparent;
            }
         
        }

        private void out1InfoEnabledChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out1MainEnabledChckBox.Checked = out1InfoEnabledChckBox.Checked;
            toolStripMenuItem3.Checked = out1MainEnabledChckBox.Checked;
        }


        private void out2MainEnabledChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out2InfoEnabledChckBox.Checked = out2MainEnabledChckBox.Checked;
            if (out2MainEnabledChckBox.Checked)
            {
                out2MainStatusPanel.BackColor = Color.LimeGreen;
                out2InfoStatusPanel.BackColor = Color.LimeGreen;
            }
            else
            {
                out2MainStatusPanel.BackColor = Color.Transparent;
                out2InfoStatusPanel.BackColor = Color.Transparent;
            }
        }

        private void out2InfoEnabledChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out2MainEnabledChckBox.Checked = out2InfoEnabledChckBox.Checked;
        }


        private void out3MainEnabledChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out3InfoEnabledChckBox.Checked = out3MainEnabledChckBox.Checked;
            if (out3MainEnabledChckBox.Checked)
            {
                out3MainStatusPanel.BackColor = Color.LimeGreen;
                out3InfoStatusPanel.BackColor = Color.LimeGreen;
            }
            else
            {
                out3MainStatusPanel.BackColor = Color.Transparent;
                out3InfoStatusPanel.BackColor = Color.Transparent;
            }

        }

        private void out3InfoEnabledChckBox_CheckedChanged(object sender, EventArgs e)
        {
            out3MainEnabledChckBox.Checked = out3InfoEnabledChckBox.Checked;
        }


        #endregion
        
        double time;
        private void valuesRefresTimer_Tick(object sender, EventArgs e)
        {
            
            time += 0.01;
            Series[] points= {new Series(),new Series() };
            for (int i = 0; i <= 1; i++)
            {
                if (out1MainValuesChart.Series[i].Points.Count > 180)
                {
                    for (int j = 0; j < 179; j++)
                    {
                        points[i].Points.Add(out1MainValuesChart.Series[i].Points[j+1]);
                    }
                    
                    out1MainValuesChart.Series[i].Points.Clear();
                    for (int k = 0; k < 179; k++)
                    {
                        out1MainValuesChart.Series[i].Points.Add(points[i].Points[k]);
                    }
                   
                }
            }
          
         
          
            if (!String.IsNullOrEmpty(message))
            {
                ConsoleWrite(serialConsoleRtb, message);
                message = String.Empty;
            }


            double saw = ComputeSawPoint(time/500, 50, 0.2, 3.5);

            chart1.Series[0].Points.AddXY(time,saw);

            double square = ComputeSquarePoint(time, 2,0.2,10);

            battVoltageProgress.Value = (int)(saw*100/13.5);
            
            battVoltageLbl.Text =  saw.ToString();
            battVoltageLbl.Text += " V";

            out1MainRealVtgLbl.Text = ComputeSquarePoint(time, 30, 0.2, 17).ToString().Substring(0, 5)+" V";
            out1InfoRealVtgLbl.Text = out1MainRealVtgLbl.Text;

            out1InfoDesVtgLbl.Text = out1MainDesVtgIntNumUpDown.Value+","+out1MainDesVtgDecNumericUpDown.Value+" V";

            out1MainRealVtgProgress.Value = (int)(ComputeSquarePoint(time, 30, 0.2, 17)*4.0);

            out1VtgToolStripProgressBar.Value = out1MainRealVtgProgress.Value;

            out1InfoVoltageProgress.Value = out1MainRealVtgProgress.Value;

            out1MainRealCurrLbl.Text = ComputeSquarePoint(time + Math.PI / 2, 3, 0.2, 8).ToString().Substring(0,5)+" A";
            out1InfoRealCurrLbl.Text = out1MainRealCurrLbl.Text;

            out1InfoDesCurrLbl.Text = out1MainDesCurrIntNumericUpDown.Value + "," + out1MainDesCurrDecNumericUpDown.Value+" A";
            out1MainRealCurrProgress.Value = (int)(ComputeSquarePoint(time + Math.PI / 2, 3, 0.2, 8)*9.0);
            out1CurrToolStripProgressBar.Value = out1MainRealCurrProgress.Value;
            out1InfoCurrentProgress.Value = out1MainRealCurrProgress.Value;

            

            if (square > ComputeSquarePoint(time, 30, 0.2, 18))
            {
                progressBar5.Value = (int)((square-ComputeSquarePoint(time, 30, 0.2, 17))*10);
                progressBar6.Value = 0;
            }
            else
            {
                progressBar6.Value = ((int)((ComputeSquarePoint(time, 30, 0.2, 17) - square)*10)>progressBar6.Maximum)?progressBar6.Maximum: ((int)(ComputeSquarePoint(time, 30, 0.2, 17) - square) * 10);
                progressBar5.Value = 0;
            }

            EnsureBatteryCapacity(batteryBtn, battVoltageProgress.Value);


       /*     if (showTrendsChcBx.Checked)
            {
                battTrendsChart.Series[0].Points.AddXY(time, square);
                 
    
            }*/
            if (toolStripMenuItem41.Checked)
            {
                if (out1MainChartVtgToolStripMenuItem.Checked)
                {
                    out1MainValuesChart.Series[0].Points.AddXY(time, ComputeSquarePoint(time, 30, 0.2, 18)); //add values to voltage line
                }
                if (out1MainChartCurrToolStripMenuItem.Checked)
                {
                    out1MainValuesChart.Series[1].Points.AddXY(time, ComputeSquarePoint(time+Math.PI/2, 3, 0.2, 8)); //add values to current line
                }

            }

            if (out2PlotChckBox.Checked)
            {


                if (out2PlotVtgChckBox.Checked)
                {
                    out2MainValuesChart.Series[0].Points.AddXY(time, function(time) + 10); //add values to voltage line
                }
                if (out2PlotCurrCheckBox.Checked)
                {
                    out2MainValuesChart.Series[1].Points.AddXY(time, function(time + 10.0) - 5.0); //add values to current line
                }
            }

            if (out3PlotChckBox.Checked)
            {
                if (out3PlotVtgChckBox.Checked)
                {
                    out3MainValuesChart.Series[0].Points.AddXY(time, function(time) + 10); //add values to voltage line
                }
                if (out3PlotCurrCheckBox.Checked)
                {
                    out3MainValuesChart.Series[1].Points.AddXY(time, function(time + 10.0) - 5.0); //add values to current line
                }
            }


            if (toolStripMenuItem6.Checked && !String.IsNullOrEmpty(logFileName))
            {
                try
                {
                    File.AppendAllText(logFileName, time.ToString().Replace(',','.') + ";" + out1MainRealVtgLbl.Text.Split(' ')[0].Replace(',', '.') + "\r\n");
                }
                catch (Exception)
                {
                   
                }
            }
          

        }

        private double ComputeSawPoint(double time, int fourier_degree=2, double offset=0, double amplitude=1)
        {
            double triangle=0;

            for (int i = 1; i < 100000; i+=2)
            {
                triangle += (Math.Pow(-1,(i-1)/2)/Math.Pow(i,2)*Math.Sin(i*Math.PI*fourier_degree*time));
            }
            triangle *= (8 / Math.Pow(Math.PI, 2));
            triangle *= amplitude;
            triangle = Math.Pow(triangle, 2) ;
            return triangle;
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void comPresentLb_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void splitContainer7_Resize(object sender, EventArgs e)
        {
            SplitContainer container = sender as SplitContainer;
            ResizeOutputPanel(container, toolStripMenuItem41.Checked);
        }

        private void splitContainer8_Resize(object sender, EventArgs e)
        {
            SplitContainer container = sender as SplitContainer;
            ResizeOutputPanel(container, out2PlotChckBox.Checked);
        }

        private void splitContainer9_Resize(object sender, EventArgs e)
        {
            SplitContainer container = sender as SplitContainer;
            ResizeOutputPanel(container, out3PlotChckBox.Checked);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            ResizeOutputPanel(splitContainer7, toolStripMenuItem41.Checked);
            ResizeOutputPanel(splitContainer8, out2PlotChckBox.Checked);
            ResizeOutputPanel(splitContainer9, out3PlotChckBox.Checked);
           
     
            

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            device_serial.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            
            while (device_serial.IsOpen)
            {
                device_serial.Close();
            }
            if (valuesRefresTimer.Enabled)
            {
                valuesRefresTimer.Enabled = false;
            }
        }

        private void device_serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = sender as SerialPort;
            //message = port.ReadLine();
            message = device_serial.ReadExisting().Trim('\xFF');
            // ConsoleWrite(serialConsoleRtb,message);
       
           
            //device_serial.DiscardInBuffer();
            
        }

        private void Form1_Validating(object sender, CancelEventArgs e)
        { 
            ConsoleWrite(serialConsoleRtb, message);
        }

        private void Form1_Validated(object sender, EventArgs e)
        {
            ConsoleWrite(serialConsoleRtb, message);
        }

        private void serialConsoleRtb_Validated(object sender, EventArgs e)
        {
            RichTextBox temp =  sender as RichTextBox;
            ConsoleWrite(temp,this.message);
            
            
        }

        private void splitContainer12_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainPageContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void consoleRefTimer_Tick(object sender, EventArgs e)
        {

        }

        private void out1SaveChartPb_Click(object sender, EventArgs e)
        {
           
        }

        private void out1MainChartVtgToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            out1MainCharLegendToolStripMenuItem_CheckedChanged(sender, e);
            if (!out1MainChartVtgToolStripMenuItem.Checked)
            {
                out1MainValuesChart.Series["out1MainChartVoltage"].Points.Clear();
            }
        }

        private void out1MainChartCurrToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            out1MainCharLegendToolStripMenuItem_CheckedChanged(sender, e);

           
            if (!out1MainChartCurrToolStripMenuItem.Checked)
            {
                out1MainValuesChart.Series["out1MainChartCurrent"].Points.Clear();
            }
        }

        private void out1MainCharLegendToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (out1MainCharLegendToolStripMenuItem.Checked)
            {


                if (out1MainChartVtgToolStripMenuItem.Checked)
                {
                    out1MainValuesChart.Series["out1MainChartVoltage"].IsVisibleInLegend = true;
                }
                else
                {
                    out1MainValuesChart.Series["out1MainChartVoltage"].IsVisibleInLegend = false;
                }
                if (out1MainChartCurrToolStripMenuItem.Checked)
                {
                    out1MainValuesChart.Series["out1MainChartCurrent"].IsVisibleInLegend = true;
                }
                else
                {
                    out1MainValuesChart.Series["out1MainChartCurrent"].IsVisibleInLegend = false;
                }
            }
            else
            {
                out1MainValuesChart.Series["out1MainChartVoltage"].IsVisibleInLegend = false;
                out1MainValuesChart.Series["out1MainChartCurrent"].IsVisibleInLegend = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="series"></param>
        /// <param name="header"></param>
   

           
 /// <summary>
 /// 
 /// </summary>
 /// <param name="series"></param>
 /// <param name="header"></param>
 /// <param name="series2"></param>
 /// <param name="header2"></param>
        private void saveChart(Series series,String header=null, Series series2=null, String header2=null)
        {
            Series[] chart = { series, series2 };

     
         
            String[] headers = {header,header2 };

            
            saveFileDialog1.Filter = "comma separated values (*.csv) | *.csv | txt files (*.txt) | *.txt | All files(*.*) | *.* " ;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".csv";
            saveFileDialog1.FileName = "plot";
            saveFileDialog1.CheckFileExists = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               
                
                File.WriteAllText(saveFileDialog1.FileName, "");


                int length = series.Points.Count();
                if (length == 0)
                {

                    File.AppendAllText(saveFileDialog1.FileName, "No datas...\r\n");
                }
                else
                {
                    StringBuilder data = new StringBuilder(length+1);
                    data.Append("Time;");
                    foreach (string item in headers)
                    {
                        data.Append("Value;");
                    }
                    if (String.IsNullOrWhiteSpace(header))
                    {
                       data.Append("Value");
                    }
                    else
                    {
                        data.Append(header.Trim().Replace(' ','_'));
                    }
                    if (series2!=null)
                    {
                        if (String.IsNullOrWhiteSpace(header))
                        {
                            data.Append(";Value");
                        }
                        else
                        {
                            data.Append(";"+header.Trim().Replace(' ', '_'));
                        }
                    }
                    data.AppendLine();

                    

                    foreach (DataPoint point in series.Points)
                    {
                        File.AppendAllText(saveFileDialog1.FileName, point.XValue.ToString().Replace(',', '.'));
            
                       // ( ";" + point.YValues[0].ToString().Replace(',', '.') + "\r\n");
                    }

                }
            }
        }

        private void Out1MainChartTitleTb_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(out1MainChartTitleTb.Text))
            {
                out1MainValuesChart.Titles[0].Text = out1MainChartTitleTb.Text;
               // MessageBox.Show( + Environment.NewLine + );// ((!String.IsNullOrWhiteSpace(tb.Text))?tb.Text : "Output 1");
            }

         
        }

        private void out1MainChartSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "comma separated values (*.csv)|";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".csv";
            saveFileDialog1.FileName = "out1plot";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {


                File.WriteAllText(saveFileDialog1.FileName, "");
                int length = out1MainValuesChart.Series[0].Points.Count();
                if (length == 0)
                {

                    File.AppendAllText(saveFileDialog1.FileName, "No datas...\r\n");
                }
                else
                {
                    File.AppendAllText(saveFileDialog1.FileName, "Time;Value\r\n");

                    foreach (DataPoint point in out1MainValuesChart.Series[0].Points)
                    {
                        File.AppendAllText(saveFileDialog1.FileName, point.XValue.ToString().Replace(',', '.') + ";" + point.YValues[0].ToString().Replace(',', '.') + "\r\n");
                    }

                }

            }
        }

        private void out1MainChartPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            out1MainValuesChart.Printing.Print(true);
        }

        private void splitContainer11_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripMenuItem41_CheckedChanged(object sender, EventArgs e)
        { 
            out1MainValuesChart.Visible ^= true;
            /*out1PlotVtgChckBox.Visible ^= true;
            out1PlotCurrCheckBox.Visible ^= true;
            out1PlotLegendChckBox.Visible ^= true;*/
            splitContainer7.IsSplitterFixed ^= true;
            
            if (toolStripMenuItem41.Checked)
            {
                splitContainer7.Panel2MinSize = 306;
            }
            else
            {
                splitContainer7.Panel2MinSize = 50;
            }
            ResizeOutputPanel(splitContainer7, toolStripMenuItem41.Checked);
        }

        private void out1MainChartTitleToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            out1MainValuesChart.Titles[0].Visible ^= true;
        }

        private void toolStripMenuItem43_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
           


            out1MainValuesChart.ChartAreas[0].AxisY.MajorGrid.Enabled = item.Checked;
        }

        private void toolStripMenuItem44_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;



            out1MainValuesChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = item.Checked;
        

    }

    private void toolStripMenuItem42_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fileNameToolStripMenuItem40_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "comma separated values (*.csv) | *.csv | txt files (*.txt) | *.txt | All files(*.*) | *.* ";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".csv";
            saveFileDialog1.FileName = "log";
            //saveFileDialog1.CheckFileExists = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                logFileName = saveFileDialog1.FileName;
                fileNameToolStripMenuItem40.Text = logFileName;
                try
                {
                    File.Create(logFileName);
                }
                catch(Exception ex)
                {

                   MessageBox.Show(ex.Message, "File create error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         
                }
             
            }
            }

        private void toolStripMenuItem6_CheckedChanged(object sender, EventArgs e)
        {

         
            out1MainInfoTsSl.Visible^=true;
            
            if (out1MainInfoTsSl.Visible)
            {
                out1MainInfoTsSl.Text = logFileName;
            }
        }

        private void toolStripMenuItem3_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            out1MainEnabledChckBox.Checked = item.Checked;
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem t = sender as ToolStripMenuItem;
            chart1.Visible = t.Checked;
            splitContainer13.Panel1MinSize = (t.Checked) ? 400 : 215;
            splitContainer13.SplitterDistance = (t.Checked) ? 400 : 215;
          
                
   
            splitContainer13.IsSplitterFixed = !t.Checked;
        }
    }
}
