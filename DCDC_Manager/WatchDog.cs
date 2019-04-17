using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    /// <summary>
    /// Class providing auto update maintenence
    /// </summary>
    abstract public class WatchDog
    {
        private System.Timers.Timer _timer;
        private bool _autoUpdate;
        private System.IO.Ports.SerialPort _port;
        private bool _readyToUpdate;

        public bool IsAutoUpdate
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
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

        public System.IO.Ports.SerialPort Port
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
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