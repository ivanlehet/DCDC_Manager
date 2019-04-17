using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    /// <summary>
    /// Indicate status of the sourcing battery
    /// </summary>
    public enum SourceStatus
    {
        /// <summary>
        /// Indicates the battery is not present
        /// </summary>
        NotPresent,
        /// <summary>
        /// Indicates  the battery is charging
        /// </summary>
        Charging,
        /// <summary>
        /// Indicates the battery is source power
        /// </summary>
        Sourcing,
        /// <summary>
        /// Indicates the battery is charged ready
        /// </summary>
        Ready
    }
}