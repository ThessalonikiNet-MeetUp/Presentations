using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Json;
using Newtonsoft.Json;

namespace SunshineX
{
    public class ForecastFragment : Fragment
    {
        private String[] default_data = {
                    "Mon 6/23 - Sunny - 31/17",
                    "Tue 6/24 - Foggy - 21/8",
                    "Wed 6/25 - Cloudy - 22/17",
                    "Thurs 6/26 - Rainy - 18/11",
                    "Fri 6/27 - Foggy - 21/10",
                    "Sat 6/28 - TRAPPED IN WEATHERSTATION - 23/18",
                    "Sun 6/29 - Sunny - 20/7"
            };

        private ArrayAdapter<String> forecastAdapter;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);
            // Create some dummy data for the ListView.  Here's a sample weekly forecast

            List<String> weekForecast = new List<string>(default_data.ToList());

            // Now that we have some dummy forecast data, create an ArrayAdapter.
            // The ArrayAdapter will take data from a source (like our dummy forecast) and
            // use it to populate the ListView it's attached to.
            forecastAdapter =
                new ArrayAdapter<String>(
                    this.Activity, // The current context (this activity)
                    Resource.Layout.list_item_forecast, // The name of the layout ID.
                    Resource.Id.list_item_forecast_textview,  // The ID of the textview to populate.


                    weekForecast);
            var rootView = inflater.Inflate(Resource.Layout.MainFragement, container, false);
            // Get a reference to the ListView, and attach this adapter to it.
            ListView listView = (ListView)rootView.FindViewById<ListView>(Resource.Id.listview_forecast);
            listView.Adapter = forecastAdapter;
            
            return rootView;

        }
        public  override  bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_refresh:
                    //do something
                    Refresh();
                    return true;
               
            }
            return base.OnOptionsItemSelected(item);
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.forecastmenu, menu);
            base.OnCreateOptionsMenu(menu, menuInflater);
        }


        public async Task Refresh()
        {
            // Get the latitude and longitude entered by the user and create a query.
            string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q=Thessaloniki&mode=json&units=metric&cnt=7";

            // Fetch the weather information asynchronously, 
            // parse the results, then update the screen:
            JsonValue json = await FetchWeatherAsync(url);
            //var ans = json.ToString();
            Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json.ToString());
            var forecastList = forecast.list.Select(i => i.weather.First().description).ToList();
            if (forecastList.Any())
            {
                
                forecastAdapter.Clear();
                forecastList.ForEach(i => forecastAdapter.Add(i));
                

                // New data is back from the server. Hooray!
            }
        
            
        }

        // Gets weather data from the passed URL.
        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
    }

    public class Forecast
    {
        public string cod { get; set; }
        public float message { get; set; }
        public City city { get; set; }
        public int cnt { get; set; }
        public List[] list { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
    }

    public class Coord
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }

    public class List
    {
    public int dt { get; set; }
    public Temp temp { get; set; }
    public float pressure { get; set; }
    public int humidity { get; set; }
    public Weather[] weather { get; set; }
    public float speed { get; set; }
    public int deg { get; set; }
    public int clouds { get; set; }
    public float rain { get; set; }
    }

    public class Temp
    {
        public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }

    public class Weather
    {
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
    }

}