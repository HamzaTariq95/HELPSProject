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

namespace HELPS
{
    [Activity(Label = "UTS:HELPS", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private SessionBookingData sessionBookingData;
        private WorkshopBookingData workshopBookingData;
        private StudentData studentData;

        private SupportToolbar _Toolbar;
        private int _CurrentViewTitle = Resource.String.applicationName;
        private HelpsAppCompatDrawerToggle _DrawerToggle;
        private DrawerLayout _DrawerLayout;
        private ArrayAdapter _MenuAdapter;
        private ListView _Menu;
        private FragmentTransaction _FragmentManager;
        private Fragment _Landing, _Future, _Past, _Search;

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

            //Fetch booking data
            FetchBookingData();

            // Set up the views
            _Landing = new LandingFragment(sessionBookingData, workshopBookingData, studentData);
            _Future = new FutureBookingsFragment(sessionBookingData, workshopBookingData, studentData);
            _Past = new PastBookingsFragment(sessionBookingData, workshopBookingData, studentData);

            // Set up the landing page
            SetView(Resource.Id.fragmentContainer, _Landing, false);

            // Set up action bar
            SetUpSupportActionBar(bundle);
            
            _DrawerToggle.SyncState();

            // Set up the menu layout.
            SetUpMenu();
        }

        private void FetchBookingData()
        {
            studentData = JsonConvert.DeserializeObject<StudentData>(Intent.GetStringExtra("student"));

            SessionController sessionController = new SessionController();
            sessionBookingData = sessionController.GetSessionBookingData(studentData.attributes.studentID);

            WorkshopController workshopController = new WorkshopController();
            workshopBookingData = workshopController.GetWorkshopBookingData(studentData.attributes.studentID);
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


            StudentData studentData = JsonConvert.DeserializeObject<StudentData>(Intent.GetStringExtra("student"));
            string helloUser = GetString(Resource.String.hello) + " " + studentData.attributes.studentID + "!";
            TextView helloUserText = FindViewById<TextView>(Resource.Id.textHelloUser);

            helloUserText.Text = helloUser;


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

            _Menu.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
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
                         //FetchAvailableWorkshopsData
                        _CurrentViewTitle = Resource.String.searchTitle;
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
    }
}
