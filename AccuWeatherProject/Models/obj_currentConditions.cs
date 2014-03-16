using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AccuWeatherProject.Models
{
    [System.Runtime.Serialization.DataContract]
    public class obj_currentConditions
    {
        [DataMember(Name = "LocalObservationDateTime")]
        public DateTime LocalObservationDateTime { get; set; }

        [DataMember(Name = "EpochTime")]
        public int EpochTime { get; set; }

        [DataMember(Name = "WeatherText")]
        public string WeatherText { get; set; }

        [DataMember(Name = "WeatherIcon")]
        public int WeatherIcon { get; set; }

        [DataMember(Name = "IsDayTime")]
        public bool IsDayTime { get; set; }

        [DataMember(Name = "Temperature")]
        public obj_currentConditions_Temperature Temperature { get; set; }

        [DataMember(Name = "MobileLink")]
        public string MobileLink { get; set; }

        [DataMember(Name = "Link")]
        public string Link { get; set; }  
    }

    [System.Runtime.Serialization.DataContract]
    public class obj_currentConditions_Temperature
    {
        [DataMember(Name = "Metric")]
        public obj_currentConditions_Temperature_Values Metric { get; set; }

        [DataMember(Name = "Imperial")]
        public obj_currentConditions_Temperature_Values Imperial { get; set; }
    }

    [System.Runtime.Serialization.DataContract]
    public class obj_currentConditions_Temperature_Values
    {
        [DataMember(Name = "Value")]
        public string Value { get; set; }

        [DataMember(Name = "Unit")]
        public string Unit { get; set; }

        [DataMember(Name = "UnitType")]
        public string UnitType { get; set; }
    }
}
