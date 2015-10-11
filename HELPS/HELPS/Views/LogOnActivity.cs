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
using HELPS.Model;
using Android.Util;
using Newtonsoft.Json;
using System.IO;


namespace HELPS
{
    [Activity(Label = "UTS:HELPS", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/helpsFullscreenTheme")]
    public class LogOnActivity : Activity
    {
        private TextView wrongInput;

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

            // Sets the text telling the user that their information is incorrect to invisible
            wrongInput = FindViewById<TextView>(Resource.Id.textWrongInput);
            wrongInput.Visibility = ViewStates.Gone;

            com.refractored.fab.FloatingActionButton logInButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabLogIn);

            // Works the "Log-in" button
            logInButton.Click += delegate
            {
                LogIn(username.Text, password.Text);
            };

            // Works the "Forgotten Password" button
            Button forgottenPasswordButton = FindViewById<Button>(Resource.Id.buttonForgotPassword);
            forgottenPasswordButton.Click += delegate
            {
                // Opens external UTS website where users can change their password
                SendToUtsResetPassword();
            };

            // Works the "About HELPS" button
            Button aboutHelpsButton = FindViewById<Button>(Resource.Id.buttonWhatHelps);
            aboutHelpsButton.Click += (IntentSender, e) =>
            {
                ShowAboutHelps();
            };
        }

        // Controls what happens when the user clicks the Log In button
        void LogIn(String username, String password)
        {
            // {Architecture} replace with log in authentication method

            HomeController homeController = new HomeController();

            StudentData studentData = homeController.login(username, password);

            // {Architecture} replace with method to check if it's user's first log-in.
            // If first log-in, go to RegisterActivity, else go to MainActivity
            // If the information doesn't exist in the database, display message to user.
            if (studentData != null)
            {
                Intent mainActivity = new Intent(Application.Context, typeof(MainActivity));

                // Passing the Student object to the next Activity
                mainActivity.PutExtra("student", JsonConvert.SerializeObject(studentData));

                StartActivity(mainActivity);

            }

            // {Architecture} If case for when it is the user's first log-in
            // Go to the Register Activity
            // Need to fetch data from UTSstudentData.csv and pass that on to the next Intent.

            // Else the user's creditials are not in the database or wrong
            else
            {
                wrongInput.Visibility = ViewStates.Visible;
            }
        }

        // Controls opening the external UTS website where users can change their password
        void SendToUtsResetPassword()
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("https://email.itd.uts.edu.au/webapps/myaccount/passwordreset/");
            Intent intent = Intent.CreateChooser(new Intent(Intent.ActionView, uri), "Open with");
            StartActivity(intent);
        }

        // Controls opening the dialog that displays the "About HELPS" information
        void ShowAboutHelps()
        {
            // Creates the alert displaying the "About HELPS" information
            AlertDialog.Builder aboutHelpsAlert = new AlertDialog.Builder(this);

            aboutHelpsAlert.SetTitle(GetString(Resource.String.whatHelps));
            aboutHelpsAlert.SetMessage(GetString(Resource.String.aboutHelps));
            aboutHelpsAlert.SetNeutralButton("OK", delegate { });

            Dialog aboutHelpsDialog = aboutHelpsAlert.Create(); ;
            aboutHelpsDialog.Show();
        }


        static List<string> SplitRow(string Row)
        {
            List<string> result = new List<string>();
            string[] splitRow = Row.Split(",".ToCharArray());
            result = splitRow.ToList<string>();
            return result;
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           