using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class LinePower : Source, IReadableProperty, IWritableProperty
    {


        public LinePower()
        {
            this._type = SourceType.Battery;
        }

        public LinePower(LinePower line) : base(line) { }


        public LinePower(Source line, System.IO.Ports.SerialPort port)
        {

        }

        public override SourceType Type
        {
            get
            {
                return base._type;
            }

        }

        public override PSValue<SourceStatus> Status
        {
            get { return base.Status; }
            set { base.Status = (isValidStatus(value.Value)) ? value : this._status; }
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