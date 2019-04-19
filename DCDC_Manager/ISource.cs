using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DCDC_Manager
{
    public interface ISource
    {
        
       
        SourceType Type { get; set; }
        PSValue<SourceStatus>
            Status { get; set; }
    }
}