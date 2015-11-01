using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using HELPS.Model;
using HELPS.Controllers;
using Android.Content.PM;
using SupportAlert = Android.Support.V7.App.AlertDialog;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Views;

namespace HELPS
{
    [Activity(Label = "RegisterActivity", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/icon")]
    public class RegisterActivity : AppCompatActivity
    {
        private SupportToolbar _Toolbar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Sets the layout to the "Register Check" layout
            SetContentView(Resource.Layout.Register);

            // Set the toolbar
            _Toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);

            // Set up action bar
            SetSupportActionBar(_Toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

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

            // Set up the Check Boxes
            CheckBox hsc = FindViewById<CheckBox>(Resource.Id.checkHsc);
            CheckBox ielts = FindViewById<CheckBox>(Resource.Id.checkIelts);
            CheckBox toefl = FindViewById<CheckBox>(Resource.Id.checkToefl);
            CheckBox tafe = FindViewById<CheckBox>(Resource.Id.checkTafe);
            CheckBox cult = FindViewById<CheckBox>(Resource.Id.checkCult);
            CheckBox deep = FindViewById<CheckBox>(Resource.Id.checkDeep);
            CheckBox diploma = FindViewById<CheckBox>(Resource.Id.checkDiploma);
            CheckBox foundation = FindViewById<CheckBox>(Resource.Id.checkFoundation);

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
                    if (gender.CheckedRadioButtonId == Resource.Id.radioMale)
                    {
                        studentData.Gender = "M";
                    }
                    else if (gender.CheckedRadioButtonId == Resource.Id.radioFemale)
                    {
                        studentData.Gender = "F";
                    }
                    else if (gender.CheckedRadioButtonId == Resource.Id.radioOther)
                    {
                        studentData.Gender = "X";
                    }
                    studentData.Status = statusChecked.Text;
                    studentData.FirstLanguage = languages.SelectedItem.ToString();
                    studentData.CountryOrigin = countries.SelectedItem.ToString();
                    studentData.HSC = hsc.Checked.ToString();
                    studentData.IELTS = ielts.Checked;
                    studentData.TOEFL = toefl.Checked;
                    studentData.TAFE = tafe.Checked;
                    studentData.CULT = cult.Checked;
                    studentData.InsearchDEEP = deep.Checked;
                    studentData.InsearchDiploma = diploma.Checked;
                    studentData.FoundationCourse = foundation.Checked;

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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            OnBackPressed();
            return base.OnOptionsItemSelected(item);
        }

        private void ShowFailedAlert(string msg)
        {
            var registerFailAlert = new SupportAlert.Builder(this);
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

        private async void ShowLandingPage(string studentId)
        {
            StudentData studentDataAtHELPS = await new HomeController().login(studentId);

            Intent mainActivity = new Intent(Application.Context, typeof(MainActivity));
            // Passing the Student object to the next Activity
            mainActivity.PutExtra("student", JsonConvert.SerializeObject(studentDataAtHELPS));
            StartActivity(mainActivity);
            Finish();
        }

        // Controls sending the user back to the "Log On" activity.
        void Cancel()
        {
            OnBackPressed();
        }
    }
}