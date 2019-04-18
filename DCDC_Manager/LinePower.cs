using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class LinePower : Source
    {
 

        public LinePower()
        {
            
        }
        
        public LinePower(LinePower line)
        {
            throw new System.NotImplementedException();
        }

        public LinePower(LinePower line, System.IO.Ports.SerialPort port)
        {
            throw new System.NotImplementedException();
        }





        public  SourceType Type
        {
            get
            {
                return SourceType.Line;
            }


        }



        public override PSValue<SourceStatus> Status { get => base.Status; set => base.Status = (isValidStatus(value.Value)) ? value : this._status; }


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