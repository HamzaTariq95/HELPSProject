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

namespace HELPS
{
    [Activity(Label = "UTS:HELPS", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private DrawerLayout _Layout;
        private ArrayAdapter _MenuAdapter;
        private ListView _Menu;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource.
            SetContentView(Resource.Layout.Main);

            // Set up the menu layout.
            _Layout = FindViewById<DrawerLayout>(Resource.Id.drawerMain);
            _Menu = FindViewById<ListView>(Resource.Id.listMenu);

            _MenuAdapter = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.menu, Android.Resource.Layout.SimpleListItem1);
            _Menu.Adapter = _MenuAdapter;

            // Set up ability to click menu items
            _Menu.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                switch(e.Position)
                {
                    // Profile/Landing page.
                    case 0:
                        StartActivity(typeof(MainActivity));
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

            // Set the "Hello User" text view to display the user's name
            // {Architecture} change the code so that it grabs the user's first name from the db

            var studentData = JsonConvert.DeserializeObject<StudentData>(Intent.GetStringExtra("student"));

            string helloUser = GetString(Resource.String.hello) + " " + studentData.attributes.studentID + "!";
            TextView helloUserText = FindViewById<TextView>(Resource.Id.textCheckMobile);


            helloUserText.Text = helloUser;

            // Set the "Upcoming Sessions" list view to display (upto) the three closest sessions
            // {Architecture} change the code to generate the list view data from the user's data
            List<Session> testList = new List<Session>();
            testList.Add(new Session("Session 1", "Booked", "01/01/2015", "All Students", "B1.05.202", "Mr Tutor", "Session", "Testing"));
            testList.Add(new Session("Session 2", "Booked", "01/01/2015", "All Students", "B1.05.202", "Mr Tutor", "Session", "Testing"));
            testList.Add(new Session("Session 3", "Booked", "01/01/2015", "All Students", "B1.05.202", "Mr Tutor", "Session", "Testing"));

            ListView upcomingList = FindViewById<ListView>(Resource.Id.listUpcoming);

            upcomingList.Adapter = new BookedSessionsBaseAdapter(this, testList);
        }
    }
}

