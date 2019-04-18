using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class CommonProperties : WatchDog
    {
        private PSValue<double> _current;
        private PSValue<double> _voltage;

        public PSValue<double> Current
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<double> Voltage
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public override String getReadQeuery()
        {
            throw new System.NotImplementedException();
        }

        public override string getWriteQuery()
        {
            throw new System.NotImplementedException();
        }

        public override void read()
        {
            throw new System.NotImplementedException();
        }

        public override void write()
        {
            throw new System.NotImplementedException();
        }
    }
}