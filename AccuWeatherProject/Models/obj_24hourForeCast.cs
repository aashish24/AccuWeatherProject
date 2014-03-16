using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AccuWeatherProject.Models
{
    [System.Runtime.Serialization.DataContract]
    public class obj_24hourForeCast
    {
        [DataMember(Name = "hourlyForecast")]
        public List<obj_hourForeCast> hourlyForecast { get; set; }
    }

    [System.Runtime.Serialization.DataContract]
    public class obj_hourForeCast
    {
        [DataMember(Name = "DateTime")]
        public DateTime DateTime { get; set; }

        [DataMember(Name = "EpochDateTime")]
        public int EpochDateTime { get; set; }

        [DataMember(Name = "WeatherIcon")]
        public int WeatherIcon { get; set; }

        [DataMember(Name = "IconPhrase")]
        public string IconPhrase { get; set; }

        [DataMember(Name = "Temperature")]
        public obj_Temps Temperature { get; set; }

        [DataMember(Name = "PrecipitationProbability")]
        public int PrecipitationProbability { get; set; }

        [DataMember(Name = "MobileLink")]
        public string MobileLink { get; set; }

        [DataMember(Name = "Link")]
        public string Link { get; set; }

        [DataMember(Name = "Hour")]
        public string Hour { get; set; }

        [DataMember(Name = "WeatherIconPath")]
        public string WeatherIconPath { get; set; }
    }

    [System.Runtime.Serialization.DataContract]
    public class obj_Temps
    {
        [DataMember(Name = "Value")]
        public Decimal Value { get; set; }

        [DataMember(Name = "Unit")]
        public string Unit { get; set; }

        [DataMember(Name = "UnitType")]
        public int UnitType { get; set; }


    }

}