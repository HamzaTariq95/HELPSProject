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
using Newtonsoft.Json;
using HELPS.Model;
using Android.Util;
using HELPS.Controllers;
using Android.Content.PM;

namespace HELPS
{
    [Activity(Label = "RegisterActivity", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/icon")]
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

            // Receives the Student object from Login Activity
            var studentData = JsonConvert.DeserializeObject<UtsData>(Intent.GetStringExtra("student"));

            // The text views for the check student data
            TextView email = FindViewById<TextView>(Resource.Id.textCheckEmail);
            TextView textDOB = FindViewById<TextView>(Resource.Id.textCheckDOB);
            TextView faculty = FindViewById<TextView>(Resource.Id.textCheckFaculty);
            TextView course = FindViewById<TextView>(Resource.Id.textCheckCourse);

            // Display student information for them to check it
            email.Text = GetString(Resource.String.checkEmail) + studentData.Email;
            textDOB.Text = GetString(Resource.String.checkDOB) + studentData.DateOfBirth;
            faculty.Text = GetString(Resource.String.checkFaculty) + studentData.Faculty;
            course.Text = GetString(Resource.String.checkCourse) + studentData.Course;
 
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

            // Set up the EditTexts
            EditText preferredName = FindViewById<EditText>(Resource.Id.editPreferredName);
            EditText preferredNumber = FindViewById<EditText>(Resource.Id.editPreferredNumber);

            // Set up the radio buttons
            RadioGroup gender = FindViewById<RadioGroup>(Resource.Id.radioGender);
            RadioGroup status = FindViewById<RadioGroup>(Resource.Id.radioStatus);
            
            // Set up the spinners
            Spinner languages = FindViewById<Spinner>(Resource.Id.spinnerLanguage);
            Spinner countries = FindViewById<Spinner>(Resource.Id.spinnerCountry);

            ArrayAdapter languageAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.languages, Android.Resource.Layout.SimpleSpinnerItem);
            languageAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            languages.Adapter = languageAdapter;

            ArrayAdapter countryAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.countries, Android.Resource.Layout.SimpleSpinnerItem);
            countryAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            countries.Adapter = countryAdapter;

            // Works the buttons on the "Input" view.
            com.refractored.fab.FloatingActionButton inputOkButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabInputOk);
            com.refractored.fab.FloatingActionButton inputCancelButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.fabInputCancel);

            // Works the "OK" button.
            inputOkButton.Click += delegate
            {
                // {Architecture} Save user input to the database and change user to registered.
                // Sends user to the landing page.
                // Stops user from re-entering the register page with the back button.
                RadioButton statusChecked = FindViewById<RadioButton>(status.CheckedRadioButtonId);
                RadioButton genderChecked = FindViewById<RadioButton>(gender.CheckedRadioButtonId);

                if (status.CheckedRadioButtonId == -1)
                {
                    ShowFailedAlert("You must select your residency status.");
                }

                else if (languages.SelectedItem.ToString()  == "First language...")
                {
                    ShowFailedAlert("You must choose your first language.");
                }
                else if (countries.SelectedItem.ToString() == "Country of origin...")
                {
                    ShowFailedAlert("You must choose your country of origin.");
                }
                else
                {
                    ShowProgressDialog();

                    // Populate the student data fields
                    studentData.PreferredName = preferredName.Text;
                    studentData.AltContact = preferredNumber.Text;
                    if (gender.CheckedRadioButtonId == -1)
                    {
                        studentData.Gender = genderChecked.Text;
                    }
                    studentData.Status = statusChecked.Text;
                    studentData.FirstLanguage = languages.SelectedItem.ToString();
                    studentData.CountryOrigin = countries.SelectedItem.ToString();
                    

                    RegisterController registerController = new RegisterController();
                    registerController.Register(studentData);

                    //set student data and go to main activity
                    ShowLandingPage(studentData.StudentId);
                }
                
            };
            // Works the cancel button.
            inputCancelButton.Click += delegate
            {
                // Returns user to the "Log On" activity.
                Cancel();
            };
        }

        private void ShowFailedAlert(string msg)
        {
            var registerFailAlert = new AlertDialog.Builder(this);
            registerFailAlert.SetMessage(msg);
            registerFailAlert.SetNeutralButton("OK", delegate { });
            registerFailAlert.Show();
        }

        private void ShowProgressDialog()
        {
            ProgressDialog progressDialog = new ProgressDialog(this);

            progressDialog.Indeterminate = true;
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.SetMessage("Registering. Please wait...");
            progressDialog.SetCancelable(false);
            progressDialog.Show();
        }

        private void ShowLandingPage(string studentId)
        {
            StudentData studentDataAtHELPS = new HomeController().login(studentId);

            Intent mainActivity = new Intent(Application.Context, typeof(MainActivity));
            // Passing the Student object to the next Activity
            mainActivity.PutExtra("student", JsonConvert.SerializeObject(studentDataAtHELPS));
            StartActivity(mainActivity);
            Finish();
        }

        // Controls sending the user back to the "Log On" activity.
        void Cancel()
        {
            StartActivity(typeof(LogOnActivity));
            Finish();
        }
    }
}