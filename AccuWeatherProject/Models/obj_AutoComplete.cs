using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AccuWeatherProject.Models
{
    [System.Runtime.Serialization.DataContract]
    public class obj_AutoComplete
    {
        [DataMember(Name = "AutoComplete")]
        public List<obj_Locations> AutoComplete { get; set; }

    }
}