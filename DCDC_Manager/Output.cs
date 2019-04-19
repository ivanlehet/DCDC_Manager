using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class Output : WatchDog, IWritableProperty, IReadableProperty
    {
        protected DesiredProperties _desired;
        protected RealProperties _real;
        protected PSValue<bool> _enabled=new PSValue<bool>();

        public Output()
        {
            this._desired = new DesiredProperties();
            this._real = new RealProperties();
            this._enabled = new PSValue<bool>();
            this._enabled.Value = false;
        }




        public DesiredProperties Desired
        {
            get
            {

                return this._desired;
            }

            set
            {
                this._desired = value;
            }
        }

        /// <summary></summary>
        /// <value></value>
        public RealProperties Real
        {
            get
            {
                return this._real;
            }

            set
            {
                this._real = value;

            }
        }

        public PSValue<bool> IsEnabled
        {
            get
            {
                return this._enabled;
            }

            set
            {
                this._enabled = value;
            }
        }

        public  void read()
        {
            throw new System.NotImplementedException();
        }

        public  void write()
        {
            throw new System.NotImplementedException();
        }

        public override string getReadQuery()
        {
            throw new System.NotImplementedException();
        }

        public  string getWriteQuery()
        {
            return string.Empty;
        }
    }
}