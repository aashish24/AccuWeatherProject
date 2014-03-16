3/12/2014

Project requirements:
As part of the interview process we would like for you to write a smallASP.NET MVC C# web site using our weather API and post the code to GitHub for us to look at it. 

You won’t be judged on how good this web site looks, but we would like to see (1) your problem solving skills and (2) your ASP.NET MVC C# and HTML/JavaScript skills in practice. The web site should allow a user to perform the following options: 

·         Search for a city (via the locations end point)
·         View the “Current Conditions” for that city (via the current conditions endpoint)
·         View the “Hourly Forecast” for the next 24 hours for that city (via the forecasts endpoint) 

You can find documentation about our API and a sample of the code needed to perform some tasks requested on this link:http://apidev.accuweather.com/developers/samples 

Our API requires a key in order to give you access to our data. Please use API key “hidden”. Below is an example in how to search for the information for a given city (State College in this case) and fetch the current conditions and hourly forecast for this city. 

We suggest that you keep the site simple. We are interested in yourASP.NET MVC C#/HTML/JavaScript skills rather than your ability to find and use jQuery plugins and other UI frameworks. We do not need a link to the running web site, but we must get a link to the source code of the web site.

Actions performed:
I created a responsive single page c# MVC site. 

The applications single view displays the current weather conditions, a 24 hour forecast, and a chart using the 24 hour forecast values for the requested location.


1. When the site first starts the clients public ip address is detected using the site http://checkip.dyndns.org.  If this fails, a default public ip address is used from the State College area.

2. The application then uses the public ip address to get a geographic location of City and State using http://freegeoip.net. When the client searches using the search input, the application uses only the AccuWeather api to get the location key.

3. The application uses the AccuWeather api(s) to 
	a. Retrieve a location Key from the State and City returned from freegeoip.net. 
	b. Retrieve current conditions.
	c. Retrieve a 24 hour forecast.
	d. Retrieve autocomplete location information.

4. The application used a separate class called HelpMethods to perform the api calls. 

5. Multiple object classes were created to correctly and easily deserialize the returned json from the api calls using the JsonConvert.DeserializeObject method.

