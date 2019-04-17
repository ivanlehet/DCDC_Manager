using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class Output : WatchDog
    {
        private CommonProperties _desired;
        private CommonProperties _real;
        private bool _enabled;

        public CommonProperties Desired
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
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