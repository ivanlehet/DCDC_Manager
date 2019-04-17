using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    /// <summary>
    /// Class provides interfacing with power supply.
    /// </summary>
    public class PowerSupply : WatchDog 
    {
        private List<Output> _output;
        private List<Source> _source;
        private WiFi _network;
        private List<PanelContainer> _panelContainer;

        public PowerSupply()
        {
         
        }

        /// <summary>
        /// List of outputs
        /// </summary>
        public List<Output> Output
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
        /// List of power sources
        /// </summary>
        public List<Source> Source
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
        /// Wireless network settings
        /// </summary>
        public WiFi Network
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
        /// Container for GUI Panels
        /// </summary>
        public List<PanelContainer> PanelContainer
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
        /// Read all power supply values from MCU
        /// </summary>
        public override void read()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Write all power supply values to MCU
        /// </summary>
        public override void write()
        {
            throw new System.NotImplementedException();
        }

        public override string getWriteQuery()
        {
            throw new System.NotImplementedException();
        }

        public override string getReadQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}