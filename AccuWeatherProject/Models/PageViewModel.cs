using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccuWeatherProject.Models
{
    public class PageViewModel
    {
        public List<obj_hourForeCast> hourlyForecast { get; set; }
        public obj_currentConditions currentConditions { get; set; }
        public obj_LocationFromIP locationfromip { get; set; }
        public obj_Locations location { get; set; }
        public int randomForImage { get; set; }
    }
}