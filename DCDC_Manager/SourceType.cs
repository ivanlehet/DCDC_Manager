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
        /// No SourceType selected
        /// </summary>
        None,
        /// <summary>
        /// Represents battery
        /// </summary>
        Battery,
        /// <summary>
        /// Represents wired power source as ACDC converter or power transformer.
        /// </summary>
        Line,
        /// <summary>
        /// Represents solar cell
        /// </summary>
        Solar
    }
}