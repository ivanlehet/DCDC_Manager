using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public interface IPanel
    {
        /// <summary>
        /// Real property values
        /// </summary>
        CommonProperties _real { get; set; }
        /// <summary>
        /// Indicates if object is enabled
        /// </summary>
        bool _enabled { get; set; }
        /// <summary>
        /// Desired property values
        /// </summary>
        CommonProperties _desired { get; set; }

        /// <summary>
        /// Methot sets common property value
        /// </summary>
        void setCommonProperties();
    }
}