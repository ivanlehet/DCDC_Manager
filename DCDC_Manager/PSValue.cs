using System;
using System.Diagnostics;
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
   

        public override string ToString()
        {
            Debug.WriteLine(this.GetType().Name+"\ttoString()\tInvoked");
     
            if(_value.GetType() == typeof(double))
            {
                Debug.WriteLine(this.GetType().Name + "\ttoString()\tReturning formated double "+ string.Format("{0:F3}", this._value));
                return string.Format("{0:F3}",this._value);
            }
            Debug.WriteLine(this.GetType().Name + "\ttoString()\tReturning base method");
            return base.ToString();
       
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

    
    }
}