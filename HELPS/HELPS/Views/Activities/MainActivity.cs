using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Util;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Newtonsoft.Json;
using HELPS.Model;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using HELPS.Views;
using HELPS.Controllers;
using HELPS.Model.JSONDataClasses;
using System.Threading.Tasks;

namespace HELPS
{
    
[Activity(Label = "UTS:HELPS", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private SessionBookingData sessionBookingData;
        private WorkshopBookingData workshopBookingData;
        private StudentData studentData;
        private WorkshopData workshopData;
        private CampusData campusData = null;

        private SupportToolbar _Toolbar;
        private int _CurrentViewTitle = Resource.String.applicationName;
        private HelpsAppCompatDrawerToggle _DrawerToggle;
        private DrawerLayout _DrawerLayout;
        private ArrayAdapter _MenuAdapter;
        private ListView _Menu;
        private FragmentTransaction _FragmentManager;
        private Fragment _Landing, _Future, _Past, _Search;
        private ProgressDialog progressDialog;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource.
            SetContentView(Resource.Layout.Main);
            _DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerMain);

            // Set the toolbar
            _Toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);

            _DrawerToggle = new HelpsAppCompatDrawerToggle(this, _DrawerLayout, Resource.String.menuTitle, _CurrentViewTitle);
            _DrawerLayout.SetDrawerListener(_DrawerToggle);

            // Set up action bar
            SetUpSupportActionBar(bundle);
            
            _DrawerToggle.SyncState();

            // Set up the menu layout.
            SetUpMenu();
            
            //Fetch booking data
            FetchBookingData();
        }

        private void ShowProgressDialog(ProgressDialog progressDialog, string message, bool show)
        {
            if (show)
            {
                progressDialog.Indeterminate = true;
                progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                progressDialog.SetMessage(message);
                progressDialog.SetCancelable(false);
                progressDialog.Show();
            }
            else
                progressDialog.Hide();
        }

        private async void FetchBookingData()
        {
            progressDialog = new ProgressDialog(this);

            await FetchWorkshopBookingData();
            await FetchSessionBookingData();

            progressDialog.Hide();

            // Set up the views
            _Landing = new LandingFragment(sessionBookingData, workshopBookingData, studentData);
            _Future = new FutureBookingsFragment(sessionBookingData, workshopBookingData, studentData);
            _Past = new PastBookingsFragment(sessionBookingData, workshopBookingData, studentData);

            // Set up the landing page
            SetView(Resource.Id.fragmentContainer, _Landing, false);

            string helloUser = GetString(Resource.String.hello) + " " + studentData.attributes.studentID + "!";
            TextView helloUserText = FindViewById<TextView>(Resource.Id.textHelloUser);

            helloUserText.Text = helloUser;
        }

        private async Task FetchWorkshopBookingData()
        {
            studentData = JsonConvert.DeserializeObject<StudentData>(Intent.GetStringExtra("student"));

            ShowProgressDialog(progressDialog, "Fetching bookings. Please wait...", true);

            WorkshopController workshopController = new WorkshopController();
            workshopBookingData = await workshopController.GetWorkshopBookingData(studentData.attributes.studentID);
            Server.currentWorkshopBookingData = workshopBookingData;
            if (workshopBookingData != null && workshopBookingData.attributes.Count > 0)
            {
                FetchCampusData();
                FetchWorkshopSetData();
            }

            ShowProgressDialog(progressDialog, "Fetching bookings. Please wait...", false);
        }

        private async Task FetchSessionBookingData()
        {
            ShowProgressDialog(progressDialog, "Fetching bookings. Please wait...", true);

            SessionController sessionController = new SessionController();
            sessionBookingData = await sessionController.GetSessionBookingData(studentData.attributes.studentID);
            Server.currentSessionBookingData = sessionBookingData;

            ShowProgressDialog(progressDialog, "Fetching bookings. Please wait...", false);
        }

        private void FetchWorkshopSetData()
        {
            WorkshopController workshopController = new WorkshopController();
            WorkshopSetData workshopSetData = (WorkshopSetData)workshopController.GetWorkshopSetData();
            Constants.WORKSHOP_SETS = workshopSetData.attributes;
        }

        private void FetchCampusData()
        {
            WorkshopController workshopController = new WorkshopController();
            campusData = (CampusData)workshopController.GetCampusData();
            Constants.CAMPUSES = campusData.attributes;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            _DrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (_DrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
        }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }
            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {

            base.OnPostCreate(savedInstanceState);


            //StudentData studentData = JsonConvert.DeserializeObject<StudentData>(Intent.GetStringExtra("student"));
            /*string helloUser = GetString(Resource.String.hello) + " " + studentData.attributes.studentID + "!";
            TextView helloUserText = FindViewById<TextView>(Resource.Id.textHelloUser);

            helloUserText.Text = helloUser;*/


            // Set the "Hello User" text view to display the user's name
            // {Architecture} change the code so that it grabs the user's first name from the db

        }

        private void SetUpSupportActionBar(Bundle bundle)
        {
            SetSupportActionBar(_Toolbar);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Not the first time activity has been run.
            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.menuTitle);
                }
                else
                {
                    SupportActionBar.SetTitle(_CurrentViewTitle);
                }
            }
            // First time activity has been run.
            else
            {
                SupportActionBar.SetTitle(_CurrentViewTitle);
            }
        }

        private void SetUpMenu()
        {
            _Menu = FindViewById<ListView>(Resource.Id.listMenu);

            _MenuAdapter = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.menu, Resource.Layout.MenuRow);
            _Menu.Adapter = _MenuAdapter;

            _Menu.ItemClick += async (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                switch (e.Position)
                {
                    // Profile/Landing page.
                    case 0:
                        _CurrentViewTitle = Resource.String.applicationName;
                        SetView(Resource.Id.fragmentContainer, _Landing, true);
                        break;
                    // Search workshops.
                    case 1:
                        //Fetch workshop data for available workshops
                        await FetchAvailableWorkshops();
                        _Search = new SearchWorkshopsFragment(workshopData);
                        _CurrentViewTitle = Resource.String.searchTitle;
                        SetView(Resource.Id.fragmentContainer, _Search, true);
                        break;
                    // Future bookings.
                    case 2:
                        _CurrentViewTitle = Resource.String.futureBookingsTitle;
                        SetView(Resource.Id.fragmentContainer, _Future, true);
                        break;
                    // Past bookings.
                    case 3:
                        _CurrentViewTitle = Resource.String.pastBookingsTitle;
                        SetView(Resource.Id.fragmentContainer, _Past, true);
                        break;
                    // Record notes.
                    case 4:
                    // Settings.
                    case 5:
                        Console.WriteLine("Not implemented");
                        break;
                    // Log out.
                    case 6:
                        // {Architecture} Log Out function
                        StartActivity(typeof(LogOnActivity));
                        Finish();
                        break;
                }
            };
        }

private async Task FetchAvailableWorkshops()
{
            ShowProgressDialog(progressDialog, "Fetching available workshops. Please wait...", true);

 	WorkshopController workshopController = new WorkshopController();
            workshopData = await workshopController.searchWorkshops(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            ShowProgressDialog(progressDialog, "Fetching available workshops. Please wait...", false);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void SetView(int fragmentResource, Fragment view, bool retainView)
        {
            _DrawerToggle.SetClosedResource(_CurrentViewTitle);

            _FragmentManager = FragmentManager.BeginTransaction();
            _FragmentManager.Replace(fragmentResource, view);

            // If true, allows the user to return to that fragment.
            // Otherwise it is destroyed.
            if(retainView)
            {
                _FragmentManager.AddToBackStack(null);
        }

            _FragmentManager.Commit();

            _DrawerLayout.CloseDrawers();
        }

        protected override async void OnResume()
        {
            base.OnResume();

            if (Server.workshopBookingsAltered)
            {
                await FetchWorkshopBookingData();
                Server.workshopBookingsAltered = false;
            }

            if (Server.sessionBookingsAltered)
            {
                await FetchSessionBookingData();
                Server.sessionBookingsAltered = false;
            }

            if (_CurrentViewTitle.Equals(Resource.String.searchTitle))
                await FetchAvailableWorkshops();
            SetCurrentFragment();
        }

        private void SetCurrentFragment()
        {
            switch (_CurrentViewTitle)
            {
                case Resource.String.applicationName :
                    _Landing = new LandingFragment(sessionBookingData, workshopBookingData,studentData);
                    SetView(Resource.Id.fragmentContainer, _Landing, true);
                    break;
                case Resource.String.searchTitle:
                    //Fetch workshop data for available workshops
                    //FetchAvailableWorkshops();
                    _Search = new SearchWorkshopsFragment(workshopData); 
                    SetView(Resource.Id.fragmentContainer, _Search, true);
                    break;
                // Future bookings.
                case Resource.String.futureBookingsTitle:
                    _Future = new FutureBookingsFragment(sessionBookingData, workshopBookingData, studentData);
                    SetView(Resource.Id.fragmentContainer, _Future, true);
                    break;
                // Past bookings.
                case Resource.String.pastBookingsTitle:
                    _Past = new PastBookingsFragment(sessionBookingData, workshopBookingData, studentData);
                    SetView(Resource.Id.fragmentContainer, _Past, true);
                    break;
            }
        }
    }
}


