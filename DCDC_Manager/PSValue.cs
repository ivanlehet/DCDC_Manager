using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public abstract class PSValue<T> : WatchDog
    {
        private T _value;
        private double _timeStamp;

        public T Value
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public double TimeStamp
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
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