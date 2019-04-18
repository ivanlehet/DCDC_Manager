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
    abstract public class WatchDog
    {
        private Timer _timer = new Timer(1000);
        private bool _autoUpdate = false;
        private SerialPort _port;
        private bool _readyToUpdate;

        public bool IsAutoUpdate
        {
            get
            {
                return this._autoUpdate;  
            }

            set
            {
                this._autoUpdate = value;

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
        public int UpdatePeriod
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
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
        public bool ReadyToUpdate
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        abstract public void read();

        abstract public void write();

        public abstract string getReadQuery();

        public abstract string getWriteQuery();
    }
}