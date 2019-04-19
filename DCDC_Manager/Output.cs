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
        protected bool _enabled;

        public Output()
        {
            this._desired = new DesiredProperties();
            this._real = new RealProperties();
            _enabled = false;
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
        public CommonProperties Real
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool IsEnabled
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
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