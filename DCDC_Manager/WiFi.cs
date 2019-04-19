using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class WiFi : WatchDog, IWritableProperty, IReadableProperty
    {
        private String _ssid;
        private String _password;
        private int _intensity;
        private bool _isConnected;

        public PSValue<String> SSID
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<String> Password
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<int> Intensity
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public PSValue<bool> IsConnected
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

        public void write()
        {
            throw new System.NotImplementedException();
        }

        public override string getReadQuery()
        {
            throw new System.NotImplementedException();
        }

        public  string getWriteQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}