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
            SetContentView(Resource.Layout.RegisterCheck);

            // Sets the font for the title to Din Regular
            TextView title = FindViewById<TextView>(Resource.Id.textRegisterCheckTitle);
            Typeface titleFont = Typeface.CreateFromAsset(this.Assets, "fonts/din-regular.ttf");

            title.SetTypeface(titleFont, TypefaceStyle.Normal);

            com.refractored.fab.FloatingActionButton checkOkButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabCheckOk);
            com.refractored.fab.FloatingActionButton checkCancelButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabCheckCancel);

            // Works the "OK" button
            checkOkButton.Click += delegate
            {
                // Changes the activity's layout to the input form
                SetContentView(Resource.Layout.Register);
            };
            // Works the cancel button
            checkCancelButton.Click += delegate
            {
                // Returns user to the "Log On" activity
                StartActivity(typeof(LogOnActivity));
            };
        }
    }
}