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
using Android.Graphics;

namespace HELPS
{
    [Activity(Label = "RegisterActivity", Icon = "@drawable/icon", Theme = "@style/helpsFullscreenTheme")]
    public class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Sets the layout to the "Register Check" layout
            SetContentView(Resource.Layout.Register);

            // Sets the font for the title to Din Regular
            TextView title = FindViewById<TextView>(Resource.Id.textRegisterTitle);
            Typeface titleFont = Typeface.CreateFromAsset(this.Assets, "fonts/din-regular.ttf");

            title.SetTypeface(titleFont, TypefaceStyle.Normal);

            // Works the buttons on the "Check" view
            com.refractored.fab.FloatingActionButton checkOkButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabCheckOk);
            com.refractored.fab.FloatingActionButton checkCancelButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabCheckCancel);

            // Works the "OK" button
            checkOkButton.Click += delegate
            {
                // Changes the activity's layout to the input form
                ViewSwitcher viewSwitcher = FindViewById<ViewSwitcher>(Resource.Id.resourceSwitch);
                viewSwitcher.ShowNext();
            };
            // Works the cancel button
            checkCancelButton.Click += delegate
            {
                // Returns user to the "Log On" activity
                Cancel();
            };

            // Works the buttons on the "Input" view.
            com.refractored.fab.FloatingActionButton inputOkButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabInputOk);
            com.refractored.fab.FloatingActionButton inputCancelButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabInputCancel);

            // Works the "OK" button.
            inputOkButton.Click += delegate
            {
                // {Architecture} Save user input to the database and change user to registered.
                // Sends user to the landing page.
                // Stops user from re-entering the register page with the back button.
                
                StartActivity(typeof(MainActivity));
                Finish();
            };
            // Works the cancel button.
            inputCancelButton.Click += delegate
            {
                // Returns user to the "Log On" activity.
                Cancel();
            };
        }

        // Controls sending the user back to the "Log On" activity.
        void Cancel()
        {
            StartActivity(typeof(LogOnActivity));
            Finish();
        }
    }
}