using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public interface IOutput
    {
        DesiredProperties Desired { get; set; }
        PSValue<bool> IsEnabled { get; set; }
        DesiredProperties Real { get; set; }
    }
}