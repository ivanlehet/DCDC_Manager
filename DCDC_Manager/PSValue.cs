using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.IO.Ports;

namespace DCDC_Manager
{
    public class PSValue<T> : WatchDog
    {
        private T _value;
        private double _timeStamp=0.0;

        public PSValue()
        {
            this.Port = new SerialPort();
         
        }


        public T Value
        {
            get
            {
                return this._value;

            }

            set
            {
                this._value = value;
            }
        }

        public double TimeStamp
        {
            get { return this._timeStamp; }


            set { this._timeStamp = value; }
          
        }

        public override void read()
        {
            throw new System.NotImplementedException();
        }

        public override void write()
        {
            throw new System.NotImplementedException();
        }

        public override string getReadQuery()
        {
            throw new System.NotImplementedException();
        }

        public override string getWriteQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}