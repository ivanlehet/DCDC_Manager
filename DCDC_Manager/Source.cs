using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace DCDC_Manager
{
    /// <summary>
    /// Implements power source for power supply
    /// </summary>
    /// <remarks>
    /// SourceStatus
    /// SourceType
    /// CommonProperties
    /// WatchDog
    /// </remarks>
    public abstract class Source : WatchDog, ISource
    {
        protected CommonProperties _details;
        protected PSValue<bool> _selected;
        protected SourceType _type;
        protected PSValue<SourceStatus> _status;

        protected Source() { }
        protected Source(Source source)
        {
            fill(source);
        }
        public Source(SourceType type)
        {
            this._type = type;
        }
        protected Source(Source source, ref SerialPort port)
        {
            if (source!=null)
            {
                fill(source);
            }
            
            if (port != null)
            {
                this.Port = port;
            }
        }
        protected Source(Source source, SourceType type)
        {
            this.fill(source);
            this._type = type;
        }

         void fill(Source source)
        {
            if (source != null)
            {

                this.Details = source.Details;
                this.IsAutoUpdate = source.IsAutoUpdate;
                this.IsSelected = source.IsSelected;
                this.Port = source.Port;
                this.ReadyToUpdate = source.ReadyToUpdate;
                this.Status = source.Status;
                this.Type = source.Type;
                this.UpdatePeriod = source.UpdatePeriod;
            }
        }

       protected Source(Source source, ref SerialPort port, SourceType type)
        {
            if(source != null)
            {
                fill(source);
            }

          
                this.Port = port;


            this._type = type;
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

        /// <summary>
        /// True if the current power source is selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this._selected.Value;
            }

            set
            {
                this._selected.Value = value;
            }
        }

        /// <summary>
        /// Power supply Selected property object
        /// </summary>
        public PSValue<bool> Selected
        {
            get
            {
                return this._selected;
            }
            set
            {
                this._selected = value;
            }
        }

        public virtual SourceType Type
        {
            get
            {
                return this._type;
            }

            set
            {
                this._type = value;
            }
        }
        public virtual bool isValidStatus(SourceStatus status) {
            return (status == SourceStatus.Charging || status == SourceStatus.NotPresent || status == SourceStatus.Ready || status == SourceStatus.Sourcing) ? true : false;

        }
      

        public virtual PSValue<SourceStatus> Status
        {
            get
            {
                return this._status;
            }

            set { this._status = (isValidStatus(value.Value))?value:this._status; }
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