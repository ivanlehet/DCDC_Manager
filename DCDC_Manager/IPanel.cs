using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public interface IPanel
    {
        CommonProperties getDesired();
        CommonProperties setDesired();
        CommonProperties getReal();
        bool setEnabled();
        bool getEnabled();
    }
}