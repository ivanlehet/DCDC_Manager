using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using DCDC_Manager;

namespace DCDC_Manager
{
    public class Battery : WatchDog, ISource
    {
        private CommonProperties _details;
        private bool _selected;
        private SourceType _type;
        private SourceStatus _status;

        public Battery()
        {
            
        }


        public Battery(Source battery, SerialPort port = null)
        {

            this.Details = battery.Details;
            this.IsAutoUpdate = battery.IsAutoUpdate;
            this.IsSelected = battery.IsSelected;
            this.ReadyToUpdate = battery.ReadyToUpdate;
            this.Type = SourceType.Battery;
            this.Status = battery.Status;
            this.UpdatePeriod = battery.UpdatePeriod;

            if (port != null)
            {
                this.Port = port;
            }
            
                        
        }

        public CommonProperties Details
        {
            get
            {
                return this._details;
            }

            set
            {
                this._details = value;
            }
        }


        public PSValue<bool> IsSelected
        {
            get
            {
                PSValue<bool> tmp = new PSValue<bool>();
                tmp.Value = _selected;
                return tmp;
            }

            set
            {
                this._selected = value.Value;
            }

        }

        public SourceType Type
        {
            get
            {
                return this._type;
            }

            set
            {
                this._type = SourceType.Battery;
            }
        }

        public PSValue<SourceStatus> Status
        {
            get
            {
                PSValue<SourceStatus> tmp = new PSValue<SourceStatus>();
                tmp.Value = this._status;
                return tmp;
            }

            set
            {
                this._status = value.Value;
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