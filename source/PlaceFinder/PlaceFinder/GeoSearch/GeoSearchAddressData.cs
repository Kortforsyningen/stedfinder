using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PlaceFinder.GeoSearch
{
    [DataContract]
    public class GeoSearchAddressData
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public List<GeoSearchAddress> data { get; set; }
    }
}