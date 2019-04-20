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
    public partial class decimalNumericUpDown : UserControl
    {

        private PSValue<double> _numValue = new PSValue<double>();
        public double Value
        {
            get { return this._numValue.Value; }
           set
            {
                this._numValue.Value = value;
            }
        }

        public decimalNumericUpDown()
        {
            InitializeComponent();
        }

        private void increment_Click(object sender, EventArgs e)
        {
            this._numValue.Value += 0.01;
            this.value.Text = this._numValue.ToString();
        }

        private void decrement_Click(object sender, EventArgs e)
        {
            this._numValue.Value -= 0.01;
            this.value.Text = this._numValue.ToString();
        }



        private void value_KeyDown(object sender, KeyEventArgs e)
        {

            if ( e.KeyCode== Keys.Enter)
            {
                this.value.Enabled = false;
             
                e.Handled = true;
                TextBox t = sender as TextBox;
                double d;
                try
                {
                    double.TryParse(t.Text, out d);
                    this._numValue.Value = d;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                }
                this.value.Enabled = true;
            }
        }
    }
}
