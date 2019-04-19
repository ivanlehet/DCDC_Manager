using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;
namespace DCDC_Manager
{
    /// <summary>
    /// Class providing auto update maintenence
    /// </summary>
    abstract public class WatchDog : IWatchDog, IReadableProperty
    {
        protected Timer _timer;
        protected bool _autoUpdate;
        protected SerialPort _port;
        protected bool? _readyToUpdate = null;

        protected WatchDog()
        {
            this._timer = new Timer(1000);
            this._timer.Enabled = false;
        }

        public bool IsAutoUpdate
        {
            get
            {
                return this._timer.Enabled;
            }

            set
            {
                this._timer.Enabled = value;

            }
        }

        public Timer WatchDogTimer
        {
            get { return this._timer; }
            set { this._timer = value; }
        }

        /// <summary>
        /// Update period in milliseconds
        /// </summary>
        public double UpdatePeriod
        {
            get
            {
                return this._timer.Interval;
            }

            set
            {
                if (value > 0 && value <= double.MaxValue)
                {
                    this._timer.Interval = value;

                }
            }
        }

        public SerialPort Port
        {
            get
            {
                return this._port;
            }

            set
            {
                this._port = value;
            }
        }

        /// <summary>
        /// Indicates if its possible to update value
        /// </summary>
        public bool? ReadyToUpdate
        {
            get
            {
                return this._readyToUpdate;
            }

            set
            {
                this._readyToUpdate = value;

            }
        }

        public virtual string getReadQuery()
        {
            throw new System.NotImplementedException();
        }
        public virtual void read() { }
    }
}