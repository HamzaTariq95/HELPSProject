using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Util;
using System.Collections.Generic;

namespace HELPS
{
    [Activity(Label = "UTS:HELPS", Icon = "@drawable/icon", Theme = "@style/helpsTheme")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Set the "Hello User" text view to display the user's name
            // {Architecture} change the code so that it grabs the user's first name from the db
            string helloUser = GetString(Resource.String.hello) + " " + Intent.GetStringExtra("ID") + "!";
            TextView helloUserText = FindViewById<TextView>(Resource.Id.textHelloUser);

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

