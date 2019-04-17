using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    /// <summary>
    /// Implements power source for power supply
    /// </summary>
    /// <remarks>
    /// SourceStatus
    /// SourceType
    /// CommonProperties
    /// WatchDog
    /// </remarks>
    public class Source : WatchDog, ISource
    {
        private CommonProperties _details;
        private bool _selected;
        private SourceType _type;

        public CommonProperties Details
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<bool> IsSelected
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public SourceType Type
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<SourceStatus> Status
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