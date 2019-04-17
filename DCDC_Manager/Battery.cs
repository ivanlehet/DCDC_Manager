using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCDC_Manager;

namespace DCDC_Manager
{
    public class Battery : WatchDog, ISource
    {
        private CommonProperties _details;
        private bool _selected;
        private SourceType _type;
        
        public Battery()
        {
            throw new System.NotImplementedException();
        }

        public Battery(Battery battery)
        {
            throw new System.NotImplementedException();
        }

        public Battery(Battery battery, System.IO.Ports.SerialPort port)
        {
            throw new System.NotImplementedException();
        }

        public CommonProperties Details
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<bool> IsSelected
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public SourceType Type
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<SourceStatus> Status
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