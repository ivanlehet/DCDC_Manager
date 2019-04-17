using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public interface IWatchDog
    {
        /// <summary>
        /// Write element value to power supply
        /// </summary>
        void write();
    }
}