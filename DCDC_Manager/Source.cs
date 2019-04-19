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
    public abstract class Source : Output, ISource, IWritableProperty, IReadableProperty
    {
        
 
        protected SourceType _type;
        protected PSValue<SourceStatus> _status;




        protected Source() : base() { }
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
            if (source != null)
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



        public void fill(Source source)
        {
            if (source != null)
            {

               
                this.IsAutoUpdate = source.IsAutoUpdate;
               
                this.Port =  source.Port;
                this.ReadyToUpdate = source.ReadyToUpdate;
                this.Status = source.Status;
                this.Type = source.Type;
                this.UpdatePeriod = source.UpdatePeriod;
            }
        }

        protected Source(Source source, ref SerialPort port, SourceType type)
        {
            if (source != null)
            {
                fill(source);
            }


            this.Port = port;


            this._type = type;
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
        public virtual bool isValidStatus(SourceStatus status)
        {
            return (status == SourceStatus.Charging || status == SourceStatus.NotPresent || status == SourceStatus.Ready || status == SourceStatus.Sourcing) ? true : false;

        }


        public virtual PSValue<SourceStatus> Status
        {
            get
            {
                return this._status;
            }

            set { this._status = (isValidStatus(value.Value)) ? value : this._status; }
        }


        new public virtual void read()
        {
            throw new System.NotImplementedException();
        }

        new public virtual void write()
        {
            throw new System.NotImplementedException();
        }

        new public virtual string getReadQuery()
        {
            throw new System.NotImplementedException();
        }

        new public virtual string getWriteQuery()
        {
            throw new System.NotImplementedException();
        }





    }
}