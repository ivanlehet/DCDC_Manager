using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCDC_Manager
{
    public partial class OutputPanelContainer : UserControl
    {
        public const int ChartCtrlVisMinSize = 306;
        public const int ChartCtrlMinSize = 50;

        private Output _output=new Output();
        public RealProperties Real
        {
            get { return _output.Real; }
            set
            {
                this._output.Real = value;
                this.realVoltage.Text = value.Voltage.ToString();
            }
        }
        

       public String RealVoltage
        {
            
            get { return this.realVoltage.Text; }
            set
            {
                if (!String.IsNullOrEmpty(value)&&!String.IsNullOrWhiteSpace(value))
                {
                    this.realVoltage.Text = value;
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


    }
}
