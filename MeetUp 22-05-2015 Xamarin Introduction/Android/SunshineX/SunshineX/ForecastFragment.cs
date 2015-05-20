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

namespace SunshineX
{
    public class ForecastFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Create some dummy data for the ListView.  Here's a sample weekly forecast
            String[] data = {
                    "Mon 6/23 - Sunny - 31/17",
                    "Tue 6/24 - Foggy - 21/8",
                    "Wed 6/25 - Cloudy - 22/17",
                    "Thurs 6/26 - Rainy - 18/11",
                    "Fri 6/27 - Foggy - 21/10",
                    "Sat 6/28 - TRAPPED IN WEATHERSTATION - 23/18",
                    "Sun 6/29 - Sunny - 20/7"
            };
            List<String> weekForecast = new List<string>(data.ToList());

            // Now that we have some dummy forecast data, create an ArrayAdapter.
            // The ArrayAdapter will take data from a source (like our dummy forecast) and
            // use it to populate the ListView it's attached to.
            ArrayAdapter<String> forecastAdapter =
                new ArrayAdapter<String>(
                    this.Activity, // The current context (this activity)
                    Resource.Layout.list_item_forecast, // The name of the layout ID.
                   Resource.Id.list_item_forecast_textview,  // The ID of the textview to populate.
                     
                  
                    weekForecast);
            var rootView = inflater.Inflate(Resource.Layout.MainFragement, container, false);
            // Get a reference to the ListView, and attach this adapter to it.
            ListView listView = (ListView)rootView.FindViewById<ListView>(Resource.Id.listview_forecast);
            listView.Adapter = forecastAdapter;
            OnRefresh();
            return rootView;
            
        }

        public async Task OnRefresh()
        {
            // Get the latitude and longitude entered by the user and create a query.
            string url = "http://api.geonames.org/findNearByWeatherJSON?lat=" +
                         "47.7" +
                         "&lng=" +
                         "22.2" +
                         "&username=demo";

            // Fetch the weather information asynchronously, 
            // parse the results, then update the screen:
            JsonValue json = await FetchWeatherAsync(url);
            var ans = json.ToString();
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
}