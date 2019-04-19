using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class DesiredProperties : RealProperties, IWritableProperty
    {
        public DesiredProperties() { }
        public string getWriteQuery()
        {
            return string.Empty;
        }

        public void write()
        { }
    }
}