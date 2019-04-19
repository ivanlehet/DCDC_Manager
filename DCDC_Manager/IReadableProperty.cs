using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public interface IReadableProperty
    {
        string getReadQuery();
        void read();
    }
}