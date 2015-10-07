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
using Android.Content.PM;
using Android.Graphics;

namespace HELPS
{
    [Activity(Label = "UTS:HELPS", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/helpsFullscreenTheme")]
    public class LogOnActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Sets the layout to the "Log On" layout
            SetContentView(Resource.Layout.LogOn);

            // Sets the font for the title to Din Regular
            TextView title = FindViewById<TextView>(Resource.Id.textUtsHelps);
            Typeface titleFont = Typeface.CreateFromAsset(this.Assets, "fonts/din-regular.ttf");

            title.SetTypeface(titleFont, TypefaceStyle.Normal);

            EditText username = FindViewById<EditText>(Resource.Id.editUtsId);
            EditText password = FindViewById<EditText>(Resource.Id.editPassword);

            // Works the "Log-in" button
            com.refractored.fab.FloatingActionButton logInButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabLogIn);
            logInButton.Click += delegate
            {
                // {Architecture} replace with log in authentication method

                // {Architecture} replace with method to check if it's user's first log-in.
                // If first log-in, go to RegisterActivity, else go to MainActivity
                StartActivity(typeof(RegisterActivity));
                Finish();

                // {Architecture}
            };

            // Works the "Forgotten Password" button
            Button forgottenPasswordButton = FindViewById<Button>(Resource.Id.buttonForgotPassword);
            forgottenPasswordButton.Click += delegate
            {
                // Opens external UTS website where users can change their password
                Android.Net.Uri uri = Android.Net.Uri.Parse("https://email.itd.uts.edu.au/webapps/myaccount/passwordreset/");
                Intent intent = Intent.CreateChooser(new Intent(Intent.ActionView, uri), "Open with");
                StartActivity(intent);
            };

            // Works the "About HELPS" button
            Button aboutHelpsButton = FindViewById<Button>(Resource.Id.buttonWhatHelps);
            aboutHelpsButton.Click += (IntentSender, e) =>
            {
                // Creates the alert displaying the "About HELPS" information
                AlertDialog.Builder aboutHelpsAlert = new AlertDialog.Builder(this);

                aboutHelpsAlert.SetTitle(GetString(Resource.String.whatHelps));
                aboutHelpsAlert.SetMessage(GetString(Resource.String.aboutHelps));
                aboutHelpsAlert.SetNeutralButton("OK", delegate { });

                Dialog aboutHelpsDialog = aboutHelpsAlert.Create(); ;
                aboutHelpsDialog.Show();
            };
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           