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
using Android.Content.PM;
using Android.Util;

namespace HELPS
{
    [Activity(Label = "UTS:HELPS", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private SupportToolbar _Toolbar;
        private HelpsAppCompatDrawerToggle _DrawerToggle;
        private DrawerLayout _DrawerLayout;
        private ArrayAdapter _MenuAdapter;
        private ListView _Menu;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource.
            SetContentView(Resource.Layout.Main);
            _DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerMain);

            // Set the toolbar
            _Toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);

            _DrawerToggle = new HelpsAppCompatDrawerToggle(this, _DrawerLayout, Resource.String.menuTitle, Resource.String.applicationName);
            _DrawerLayout.SetDrawerListener(_DrawerToggle);

            // Set up action bar
            SetUpSupportActionBar(bundle);

            _DrawerToggle.SyncState();

            // Set up the menu layout.
            SetUpMenu();

            // Set the "Hello User" text view to display the user's name
            DisplayUserName();

            // Set the "Upcoming Sessions" list view to display (upto) the four closest sessions
            DisplayUpcommingSessions();
        }

        private void DisplayUpcommingSessions()
        {
            // {Architecture} change the code to generate the list view data from the user's data
            List<Booking> sessionsList = new List<Booking>();

            sessionsList.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));
            sessionsList.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));
            sessionsList.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));

            sessionsList.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));
            sessionsList.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));
            sessionsList.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));

            ListView upcomingList = FindViewById<ListView>(Resource.Id.listUpcoming);

            upcomingList.Adapter = new BookingBaseAdapter(this, sessionsList);
        }

        private void DisplayUserName()
        {

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
                    SupportActionBar.SetTitle(Resource.String.applicationName);
                }
            }
            // First time activity has been run.
            else
            {
                SupportActionBar.SetTitle(Resource.String.applicationName);
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
                        break;
                    // Search sessions.
                    case 1:
                    // Future bookings.
                    case 2:
                    // Past bookings.
                    case 3:
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
            _DrawerToggle.SyncState();
        }
    }
}