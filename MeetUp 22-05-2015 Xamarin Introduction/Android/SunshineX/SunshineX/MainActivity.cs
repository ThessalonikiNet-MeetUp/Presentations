using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

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

            var shareMenuItem = menu.FindItem(Resource.Id.action_settings);
            //var shareActionProvider =
            //   (ShareActionProvider)shareMenuItem.ActionProvider;
            //shareActionProvider.SetShareIntent(CreateIntent());
            return true;
        }

        Intent CreateIntent()
        {
            var sendPictureIntent = new Intent(Intent.ActionSend);
            //sendPictureIntent.SetType("image/*");
            //var uri = Android.Net.Uri.FromFile(GetFileStreamPath("monkey.png"));
            //sendPictureIntent.PutExtra(Intent.ExtraStream, uri);
            return sendPictureIntent;
        }
    }
}

