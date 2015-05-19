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
            return rootView;
            
        }
    }
}