using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public interface IWritableProperty
    {
        /// <summary>
        /// Method create query string to set value in MCU
        /// </summary>
        string getWriteQuery();
        /// <summary>
        /// Method reads data to the MCU through serial
        /// </summary>
        void write();
    }
}