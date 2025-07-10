using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSL.MqttProcessor
{
    public class TagLoggerResponse
    {
        public string PubDeviceID { get; set; }
        public string SubDeviceID { get; set; }
        public List<TagData> TagDetails { get; set; }
    }
    public class TagData
    {
        public string TagID { get; set; }
        public int RSSI { get; set; }
        public string AntennaID { get; set; }
    }
}
