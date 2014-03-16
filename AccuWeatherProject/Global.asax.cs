using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AccuWeatherProject.Models;
using AccuWeatherProject.Utilties;

namespace AccuWeatherProject
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
            BootstrapMvcSample.ExampleLayoutsRouteConfig.RegisterRoutes(RouteTable.Routes);         
        }
        void Session_Start(object sender, EventArgs e)
        {
            //get users physical location, default to local if failed
            string ip = "98.235.160.223";
            string getIP = HelpMethods.GetPublicIP();   
            if (getIP != "failed") getIP = ip;
            PageViewModel pvm = new PageViewModel();
            obj_LocationFromIP loc = HelpMethods.AutoDetectLocationByIP(ip);
            pvm.locationfromip = loc;
            //save to session
            HelpMethods.SetSessionObject(pvm);
        }
    }
}