﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class RealProperties : CommonProperties, IReadableProperty
    {
        public RealProperties()
        {
         
        }

        public string getReadQuery()
        {
            return string.Empty;
        }

        public void read()
        {


        }
    }
}