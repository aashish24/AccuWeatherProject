using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AccuWeatherProject.Models
{
    [System.Runtime.Serialization.DataContract]
    public class obj_LocationFromIP
    {
        [DataMember(Name = "ip")]
        public string ip { get; set; }

        [DataMember(Name = "country_code")]
        public string country_code { get; set; }

        [DataMember(Name = "region_code")]
        public string region_code { get; set; }

        [DataMember(Name = "city")]
        public string city { get; set; }

        [DataMember(Name = "zipcode")]
        public string zipcode { get; set; }

        [DataMember(Name = "latitude")]
        public string latitude { get; set; }

        [DataMember(Name = "longitude")]
        public string longitude { get; set; }

        [DataMember(Name = "metro_code")]
        public string metro_code { get; set; }

        [DataMember(Name = "areacode")]
        public string areacode { get; set; }

        [DataMember(Name = "location_Key")]
        public string location_Key { get; set; }
    }
    //{"ip":"98.235.160.223","country_code":"US","country_name":"United States","region_code":"PA","region_name":"Pennsylvania","city":"State College","zipcode":"","latitude":40.7934,"longitude":-77.86,"metro_code":"574","areacode":"814"}


}