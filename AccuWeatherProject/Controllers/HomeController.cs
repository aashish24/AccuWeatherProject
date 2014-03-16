using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using AccuWeatherProject.Models;
using AccuWeatherProject.Utilties;


namespace BootstrapMvcSample.Controllers
{

    public class HomeController : BootstrapBaseController
    {
        private PageViewModel model;


        #region ControllerActions

        /// <summary>
        /// This is the start of the web app, it takes the current IP address and attempt to find a location, then display AccuWeather.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //on the first pass, get clients location via their ip address from the session object where we set this in the global.asx
            PageViewModel pvm = HelpMethods.GetSessionObject();
            model = GetPageViewModelFromLocation(pvm.locationfromip.city + ", " + pvm.locationfromip.region_code);
            //display the weather         
            if (model.location != null)
            {
                return View(model);
            }
            else
            {
                //sloppy
                return RedirectToAction("ErrorPage");
            }
        }

        /// <summary>
        /// This method gets a location via ajax, then updates all partialpages with the data
        /// </summary>
        /// <param name="locationInput"></param>
        /// <returns>New json weather to view</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GetNewModelForLocation(string locationInput)
        {
            string location = null;
            //load pagemodel from Accuweather api's
            model = GetPageViewModelFromLocation(locationInput);
            if (model.location != null)
            {
                location = model.location.LocalizedName;
                //return a json object to be parsed by js
                return Json(new
                {
                    success = true,
                    Message = "Found weather for " + location,
                    LocationText = location,
                    PV_24HourForecast = RenderPartialViewToString("_24HourForecast", model),
                    PV_Chart = RenderPartialViewToString("_Chart", model),
                    PV_CurrentConditions = RenderPartialViewToString("_CurrentConditions", model)
                });
            }
            else
            {
                return Json(new
               {
                   success = true,
                   Message = "failed"
               });
            }

        }

        /// <summary>
        /// This method performs AutoComplete
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AutoComplete(string term)
        {
            try
            {
                //Fill autocomplete
                obj_AutoComplete model = HelpMethods.AutoCompleteRequestinJson(term);
                if (model.AutoComplete != null)
                {
                    var locations = model.AutoComplete.SelectMany(f => new[] { f.LocalizedName + ", " + f.AdministrativeArea.ID + ", " + f.Country.ID });
                    //return a json object to be parsed by js
                    return Json(new
                    {
                        success = true,
                        Message = "Found " + model.AutoComplete.Count.ToString() + " locations",
                        Locations = locations
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = true,
                        Message = "failed"
                    });
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        /// <summary>
        /// This method uses the search text and create a model to view the page
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns>model</returns>
        private PageViewModel GetPageViewModelFromLocation(string searchText)
        {
            model = new PageViewModel();
            obj_24hourForeCast hourlyforecast = new obj_24hourForeCast();
            //add autodetect location to model
            model = HelpMethods.GetSessionObject();

            try
            {
                //use accuweather API to get location from search parametes and add to model, take the first value
                model.location = (from loc in HelpMethods.AccuWeatherLocationUsingSearchValues(searchText)
                                  select loc).First();
                //if location is found, add to model
                if (model.location != null)
                {
                    //use accuweather API to get current conditions and add to model
                    model.currentConditions = HelpMethods.AccuWeatherCurrentConditionsRequestinJson(model.location.Key);
                    //use accuweather API to get 24 hour forecast
                    hourlyforecast = HelpMethods.AccuWeather24HourlyForecastRequestinJson(model.location.Key);
                    //add 24 hour forecast to model
                    model.hourlyForecast = hourlyforecast.hourlyForecast;
                    HelpMethods.SetSessionObject(model);
                    model.randomForImage = HelpMethods.RandomNumber();
                }
            }
            catch (Exception ex)
            {

            }
            return model;
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

        #endregion

        #region Chart

              
        /// <summary>
        /// Create Chart with temperature and Pecipitation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult MyChart(int? id, string key)
        {            
            obj_24hourForeCast hourForeCast = new obj_24hourForeCast ();
            hourForeCast = HelpMethods.AccuWeather24HourlyForecastRequestinJson(key);

            Dictionary<string, int> datapoints = new Dictionary<string, int>();
            Dictionary<string, int> precippoints = new Dictionary<string, int>();
            try
            {
                foreach (var item in hourForeCast.hourlyForecast)
                {
                    datapoints.Add(item.Hour, Convert.ToInt16(item.Temperature.Value));
                    precippoints.Add(item.Hour, Convert.ToInt16(item.PrecipitationProbability));
                }
            }
            catch (Exception ex)
            {
                string XXX = ex.ToString();
            }
            // Build Chart 
            var chart = ChartUtilities.CreateChart(datapoints, precippoints, SeriesChartType.Line, "24 Hour Forecast Temperatures");
            // Return chart object, wrapped in our custom action result
            ChartActionResult newChart = new ChartActionResult(chart);
            Session["ChartMap"] = newChart;
            return newChart;
        }

        public class ChartActionResult : ActionResult
        {
            private readonly Chart _chart;
            private readonly ChartImageFormat _imageFormat;
            public ChartActionResult(Chart chart, ChartImageFormat imageFormat = ChartImageFormat.Png)
            {
                if (chart == null) { throw new ArgumentNullException("chart"); }
                _chart = chart;
                _imageFormat = imageFormat;
            }
            public override void ExecuteResult(ControllerContext context)
            {
                var response = context.HttpContext.Response;
                response.Clear();
                response.Charset = String.Empty;
                response.ContentType = "image/" + _imageFormat;
                if (_imageFormat == ChartImageFormat.Png)
                {
                    // PNG can only write to a seek-able stream31.      
                    //  Thus we have to go through a memory stream, which permits seeking.
                    using (var mStream = new MemoryStream())
                    {
                        _chart.SaveImage(mStream, _imageFormat);
                        mStream.Seek(0, SeekOrigin.Begin);
                        mStream.CopyTo(response.OutputStream);
                    }
                }
                else
                {
                    // If we don't have to provide a seek-able stream, write directly to

                    //  where the data needs to go.
                    _chart.SaveImage(response.OutputStream, _imageFormat);
                }
                _chart.Dispose();
            }

        #endregion

        }
    }
}
