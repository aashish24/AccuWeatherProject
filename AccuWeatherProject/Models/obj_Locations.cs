using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AccuWeatherProject.Models
{
    [System.Runtime.Serialization.DataContract]
    public class obj_Locations
    {
        [DataMember(Name = "Version")]
        public string Version { get; set; }

        [DataMember(Name = "Key")]
        public string Key { get; set; }

        [DataMember(Name = "PrimaryPostalCode")]
        public string PrimaryPostalCode { get; set; }

        [DataMember(Name = "LocalizedName")]
        public string LocalizedName { get; set; }

        [DataMember(Name = "Country")]
        public obj_Locations_Country Country { get; set; }


        [DataMember(Name = "AdministrativeArea")]
        public obj_Locations_AdministrativeArea AdministrativeArea { get; set; }
    }

    [System.Runtime.Serialization.DataContract]
    public class obj_Locations_Country
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "LocalizedName")]
        public string LocalizedName { get; set; }

        [DataMember(Name = "EnglishName")]
        public string EnglishName { get; set; }      
    }

    [System.Runtime.Serialization.DataContract]
    public class obj_Locations_AdministrativeArea
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "LocalizedName")]
        public string LocalizedName { get; set; }

        [DataMember(Name = "EnglishName")]
        public string EnglishName { get; set; }
    }

}