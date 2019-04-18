using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using DCDC_Manager;

namespace DCDC_Manager
{
    public class Battery : Source
    {
        public Battery() : base(SourceType.Battery) { }

        public Battery(Source battery) : base(battery,SourceType.Battery) {   }
   

        public Battery(Source battery, ref SerialPort port) : base(battery, ref port,SourceType.Battery) {  }
     

              

        public override SourceType Type
        {
            get
            {
                return SourceType.Battery;
            }

        }

      

       

        public override void read()
        {
            throw new System.NotImplementedException();
        }

        public override void write()
        {
            throw new System.NotImplementedException();
        }

        public override string getReadQuery()
        {
            throw new System.NotImplementedException();
        }

        public override string getWriteQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}