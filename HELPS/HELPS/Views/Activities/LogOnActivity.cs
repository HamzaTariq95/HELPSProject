using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
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
using CsvHelper;
using HELPS.Views.Activities;

namespace HELPS
{
    [Activity(Label = "UTS:HELPS", MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LogOnActivity : Activity
    {
        private StudentData studentDataAtHELPS;
        private UtsData studentDataAtUTS;

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
            com.refractored.fab.FloatingActionButton logInButton =
                FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabLogIn);

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
        void LogIn(string studentID, string password)
        {
            if (currentUTSStudent(studentID, password))
            {
                ShowProgressDialog();
                if (registeredAtHELPS(studentID))
                {
                    Log.Info("Inside LogOnActvitity", "Student Data is not Null");
                    Intent mainActivity = new Intent(Application.Context, typeof(MainActivity));
                    // Passing the Student object to the next Activity
                    mainActivity.PutExtra("student", JsonConvert.SerializeObject(studentDataAtHELPS));
                    StartActivity(mainActivity);
                    Finish();
                }
                else
                {
                    Intent registerActivity = new Intent(Application.Context, typeof(RegisterActivity));
                    registerActivity.PutExtra("student", JsonConvert.SerializeObject(studentDataAtUTS));
                    StartActivity(registerActivity);
                    Finish();
                }
            }
            else
            {
                //show wrong id/pass message
                var logInFailAlert = new AlertDialog.Builder(this);
                logInFailAlert.SetMessage(GetString(Resource.String.wrongInformation));
                logInFailAlert.SetNeutralButton("OK", delegate { });
                logInFailAlert.Show();
            }
          
        }

        private bool registeredAtHELPS(string studentID)
        {
            return (studentDataAtHELPS = new HomeController().login(studentID)) != null;
        }

        private bool currentUTSStudent(string studentID, string password)
        {
            Log.Info("Inside LogOnActvitity", "New STUDENT!");
            if (studentID == null || studentID.Equals(""))
                return false;

            // Extracts data from the utsData.csv
            return studentIdFoundInUTSDatabase(studentID, password);
        }

        private bool studentIdFoundInUTSDatabase(string studentID, string password)
        {
            studentDataAtUTS = null;

            var assembly = typeof(LogOnActivity).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("HELPS.utsData.csv");

            using (TextReader reader = new StreamReader(stream))
            {

                CsvReader csv = new CsvReader(reader);
                csv.Configuration.HasHeaderRecord = false;

                List<UtsData> records = csv.GetRecords<UtsData>().ToList();

                // Searches for the Student Details.
                foreach (UtsData data in records)
                {
                    //Checking only student id but not password for sake for login feature demonstration purpose only .
                    //Originally password MUST be checked as well.
                    if (data.StudentId.Trim().Equals(studentID.Trim()))
                    {
                        studentDataAtUTS = data;
                        return true;
                    }
                }
            }
            return false;
        }

        // Controls opening the external UTS website where users can change their password
        private void SendToUtsResetPassword()
            {
                Android.Net.Uri uri = Android.Net.Uri.Parse("https://email.itd.uts.edu.au/webapps/myaccount/passwordreset/");
                Intent intent = Intent.CreateChooser(new Intent(Intent.ActionView, uri), "Open with");
                StartActivity(intent);
        }

        // Controls opening the dialog that displays the "About HELPS" information
        private void ShowAboutHelps()
            {
            // Displays the About UTS:HELPS view
            Intent aboutActivity = new Intent(Application.Context, typeof(AboutActivity));
            StartActivity(aboutActivity);
        }

        private void ShowProgressDialog()
        {
            ProgressDialog progressDialog = new ProgressDialog(this);

            progressDialog.Indeterminate = true;
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.SetMessage("Logging in. Please wait...");
            progressDialog.SetCancelable(false);
            progressDialog.Show();
        }

        private static List<string> SplitRow(string Row)
        {
            List<string> result = new List<string>();
            string[] splitRow = Row.Split(",".ToCharArray());
            result = splitRow.ToList<string>();
            return result;
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           