using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSL.MqttProcessor
{
    class ApiResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public DataSection data { get; set; }
    }

    public class DataSection
    {
        public List<MapDevice> MapDevice { get; set; }
        public List<Topic> Topics { get; set; }
        public List<AssetDetails> AssetDetails { get; set; }
    }

    public class AssetDetails
    {
        public string TagID { get; set; }
        public string TagName { get; set; }

    }

    public class Topic
    {
        public string Title { get; set; }
        public string TopicName { get; set; }
    }

    public class MapDevice
    {
        public string TabDeviceID { get; set; }
        public string ReaderDeviceID { get; set; }
    }
}
