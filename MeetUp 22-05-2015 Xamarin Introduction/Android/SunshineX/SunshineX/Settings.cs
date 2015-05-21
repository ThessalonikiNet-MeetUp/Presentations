using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;

namespace SunshineX
{
    [Activity(Label = "Settings")]
    public class Settings : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Settings);
            var textView = this.FindViewById<EditText>(Resource.Id.dialog_text_view);
            ISharedPreferences sharedPrefs =
                      PreferenceManager.GetDefaultSharedPreferences(this);
            string location = sharedPrefs.GetString(
                    "city",
                    "Thessaloniki");
            textView.Text = location;
            this.FindViewById<Button>(Resource.Id.dialog_button).Click += delegate
            {

                var editor = sharedPrefs.Edit();
                editor.PutString("city", "Thessaloniki");

            };
            // Set up a handler to dismiss this DialogFragment when this button is clicked.
        
            

            // Create your application here
        }
    }
}