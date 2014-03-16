using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using AccuWeatherProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccuWeatherProject.Utilties
{
    /// <summary>
    /// This class contains utility methods for the website
    /// </summary>
    public static class HelpMethods
    {

        #region Weather

        /// <summary>
        /// This method uses the search Values to get the location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Location Key</returns>
        public static List<obj_Locations> AccuWeatherLocationUsingSearchValues(string searchValues)
        {
            List<obj_Locations> obj_loc = new List<obj_Locations>();
            string url = @"http://apidev.accuweather.com/locations/v1/search?q=" + searchValues + "&apikey=hAilspiKe";
            string json = PerformWebRequest(url);
            try
            {
                //turn json string into array
                obj_loc = JsonConvert.DeserializeObject<List<obj_Locations>>(json);

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return obj_loc;
        }

        /// <summary>
        /// This method uses the location key to get the current weather conditions from accuweather api
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>Custom mapping json object</returns>
        public static obj_currentConditions AccuWeatherCurrentConditionsRequestinJson(string Key)
        {
            List<obj_currentConditions> result = new List<obj_currentConditions>();
            string url = @"http://apidev.accuweather.com/currentconditions/v1/" + Key + ".json?language=en&apikey=hAilspiKe";
            string json = PerformWebRequest(url);
            try
            {
                //turn json string into array
                result = JsonConvert.DeserializeObject<List<obj_currentConditions>>(json);
                return result[0];
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// This method uses the location key to get the 24 hour forecast from accuweather api
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>Custom mapping json object</returns>
        public static obj_24hourForeCast AccuWeather24HourlyForecastRequestinJson(string Key)
        {

            obj_24hourForeCast returnResult = new obj_24hourForeCast();
            returnResult.hourlyForecast = new List<obj_hourForeCast>();
            string url = @"http://apidev.accuweather.com/forecasts/v1/hourly/24hour/" + Key + ".json?language=en&apikey=hAilspiKe";
            string json = PerformWebRequest(url);
            try
            {
                returnResult.hourlyForecast = JsonConvert.DeserializeObject<List<obj_hourForeCast>>(json);
                //fix hours and image path now
                returnResult = FixHoursAndImages(returnResult);
                return returnResult;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }
        /// <summary>
        /// This is the autocomplete method
        /// </summary>
        /// <param name="searchval"></param>
        /// <returns></returns>
        public static obj_AutoComplete AutoCompleteRequestinJson(string searchval)
        {
            obj_AutoComplete returnResult = new obj_AutoComplete();
            returnResult.AutoComplete = new List<obj_Locations>();

            string url = @"http://apidev.accuweather.com/locations/v1/cities/autocomplete?apikey=hAilspiKe&q=" + searchval;
            string json = PerformWebRequest(url);
            try
            {
                returnResult.AutoComplete = JsonConvert.DeserializeObject<List<obj_Locations>>(json);               
                return returnResult;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Helpers

        /// <summary>
        /// This method populates added fields to display the hours and weather icons properly on the view
        /// </summary>
        /// <param name="obj_24FC"></param>
        /// <returns></returns>
        private static obj_24hourForeCast FixHoursAndImages(obj_24hourForeCast obj_24FC)
        {
            foreach (var item in obj_24FC.hourlyForecast)
            {
                int hour = 0;
                string dayofweek = "";
                string ampm = "";
                string weathericonSource = "";
                int Weathericon = item.WeatherIcon;
                //figure out correct image path
                //"../../Images/01-s.png"
                if (Weathericon < 10)
                {
                    weathericonSource = "../../Images/0" + Weathericon + "-s.png";
                }
                else
                {
                    weathericonSource = "../../Images/" + Weathericon + "-s.png";
                }
                item.WeatherIconPath = weathericonSource;

                //get the am/pm correct
                dayofweek = item.DateTime.DayOfWeek.ToString();
                hour = Convert.ToInt16(item.DateTime.Hour);
                ampm = "am";
                if (hour == 0)
                {
                    hour = Convert.ToInt16(item.DateTime.Hour) - 12;
                    hour = 12;
                }
                else if (hour == 12)
                {
                    ampm = "pm";
                }
                else if (hour > 12)
                {
                    hour -= 12;
                    ampm = "pm";
                }
                item.Hour = hour.ToString() + ampm;
            }
            return obj_24FC;
        }

        /// <summary>
        /// This method returns the json string from web request
        /// </summary>
        /// <param name="url"></param>
        /// <returns>string</returns>
        private static string PerformWebRequest(string url)
        {
            string json;
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }
                response.Close();
                return json;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        #endregion

        #region AutoDetectLocatioAndIP

        /// <summary>
        /// This method uses the site freegeoip.net to get the physical location of the client from their ip address
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>Custom mapping json object</returns>
        public static obj_LocationFromIP AutoDetectLocationByIP(string ip)
        {
            string url = @"http://freegeoip.net/json/" + ip;
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(obj_LocationFromIP));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                obj_LocationFromIP jsonResponse = objResponse as obj_LocationFromIP;
                response.Close();
                return jsonResponse;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// This method returns the users ip address
        /// </summary>
        /// <returns>string of ip address/failure message</returns>
        public static string GetPublicIP()
        {
            string ip = string.Empty;
            string url = "http://checkip.dyndns.org";
            try
            {
                string json = PerformWebRequest(url);

                string[] a = json.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                ip = a3[0];
                return ip;
            }
            catch (Exception ex)
            {
                return "failed";
            }

        }

        #endregion

        #region Session
        /// <summary>
        /// This method gets the sesion object 
        /// </summary>
        /// <returns>obj_LocationFromIP</returns>
        public static PageViewModel GetSessionObject()
        {
            var sessionObj = HttpContext.Current.Session["PageViewModel"];
            return (PageViewModel)sessionObj;
        }

        /// <summary>
        /// This method stores the session object
        /// </summary>
        /// <param name="pvm"></param>
        /// <returns></returns>
        public static bool SetSessionObject(PageViewModel pvm)
        {
            bool result = false;
            try
            {
                if (HttpContext.Current.Session["PageViewModel"] == null) HttpContext.Current.Session.Add("PageViewModel", new PageViewModel());
                HttpContext.Current.Session["PageViewModel"] = pvm;
                result = true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return result;
        }

        #endregion

        public static int RandomNumber()
        {
            Random random = new Random();
            return random.Next(0, 1000);
        }

    }
}
