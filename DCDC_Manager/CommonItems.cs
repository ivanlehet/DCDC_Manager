using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public abstract class CommonProperties : WatchDog,IWritableProperty
    {
        protected PSValue<double> _current;
        protected PSValue<double> _voltage;

        public CommonProperties()
        {
            this._voltage = new PSValue<double>();
            this._current = new PSValue<double>();
        }
        public PSValue<double> Current
        {
            get
            {
                return this._current;
            }

            set
            {
                this._current = value;
            }
        }

        public PSValue<double> Voltage
        {
            get
            {
                return this._voltage;
            }

            set
            {
                this._voltage = value;
            }
        }

        public virtual void write()
        {
            //TODO: implement write to serial
        }



        /// <summary>
        /// Function creating querry string to read current and voltage values from power supply.
        /// </summary>
        /// <returns>Function returns query string for current and voltage "@curr@vtg". Query is send with \r\n</returns>
        public  string getWriteQuery()
        {

            return "@" + this.GetType().Name.Substring(0, 3).ToLowerInvariant();
        }

        /// <summary>
        /// Function creating querry string to write current and voltage values to power supply.
        /// </summary>
        /// <returns>Function returns query string for current and voltage "@curr@vtg". Query is send with \r\n</returns>
        



    }
}