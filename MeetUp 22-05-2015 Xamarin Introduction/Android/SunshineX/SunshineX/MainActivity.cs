using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;
using Android.Util;

namespace SunshineX
{
    [Activity(Label = "SunshineApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
           
            // Create a new fragment and a transaction.
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            if (bundle == null)
            {
                ForecastFragment aDifferentDetailsFrag = new ForecastFragment();

                // The fragment will have the ID of Resource.Id.fragment_container.
                fragmentTx.Add(Resource.Id.container, aDifferentDetailsFrag);

                // Commit the transaction.
                fragmentTx.Commit();
            }

            // Get our button from the layout resource,
            // and attach an event to it
            // Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);

           // var shareMenuItem = menu.FindItem(Resource.Id.action_settings);
            //var shareActionProvider =
            //   (ShareActionProvider)shareMenuItem.ActionProvider;
            //shareActionProvider.SetShareIntent(CreateIntent());
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_settings:
                    //do something
                    // Check what fragment is shown, replace if needed.
                    Settings();
                    return true;

                case Resource.Id.action_map :
                    OpenPreferredLocationInMap();

                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }

        private void Settings()
        {
            var intent = new Intent(this, typeof(Settings));
    
                StartActivity(intent);
        }

        private void OpenPreferredLocationInMap()
        {
            ISharedPreferences sharedPrefs =
                PreferenceManager.GetDefaultSharedPreferences(this);
            string location = sharedPrefs.GetString(
                "city",
                "Thessaloniki");

            // Using the URI scheme for showing a location found on a map. This super-handy
            // intent can is detailed in the "Common Intents" page of Android's developer site:
            // http://developer.android.com/guide/components/intents-common.html#Maps

            Android.Net.Uri geoLocation = Android.Net.Uri.Parse("geo:0,0?").BuildUpon()
                .AppendQueryParameter("q", location)
                .Build();

            Intent intent = new Intent(Intent.ActionView);
            intent.SetData(geoLocation);

            if (intent.ResolveActivity(PackageManager) != null)
            {
                StartActivity(intent);
            }
            else
            {
                Log.Debug("SunshineX", "Couldn't call " + location + ", no receiving apps installed!");
            }
        }
    }

      
}

