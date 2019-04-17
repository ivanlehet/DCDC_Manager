using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    /// <summary>
    /// Idicates type of power source
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// No source selected
        /// </summary>
        None,
        /// <summary>
        /// Battery selected
        /// </summary>
        Battery,
        /// <summary>
        /// Line selected
        /// </summary>
        Line
    }
}