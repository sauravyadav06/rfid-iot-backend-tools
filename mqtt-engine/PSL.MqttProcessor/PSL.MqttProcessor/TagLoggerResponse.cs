using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSL.MqttProcessor
{
    public class TagLoggerResponse
    {
        public string messageType { get; set; }
        public string pubDeviceID { get; set; }
        public string subDeviceID { get; set; }
        public object data { get; set; }
    }



    public class tagdata
    {
        public List<taginfo> tagDetails { get; set; }
    }

    public class taginfo {
        public string tagID { get; set; }
        public string tagName { get; set; }
        public int rssi { get; set; }
        public string antennaID { get; set; }
        public int count { get; set; }
    }
}