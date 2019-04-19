using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;

namespace DCDC_Manager
{
    public interface IWatchDog
    {
        bool IsAutoUpdate { get; set; }
        SerialPort Port { get; set; }
        bool? ReadyToUpdate { get; set; }
        Timer WatchDogTimer { get; set; }
        
    }
}