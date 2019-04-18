using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DCDC_Manager
{
    public interface ISource
    {
        CommonProperties Details { get; set; }
        bool IsSelected { get; set; }
        SourceType Type { get; set; }
        DCDC_Manager.PSValue<SourceStatus> Status { get; set; }
    }
}