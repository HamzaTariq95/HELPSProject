using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using HELPS.Model;
using Newtonsoft.Json;
using HELPS.Views;

namespace HELPS
{
    public class LandingFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.MainLayout, container, false);

            // Set the "Hello User" text view to display the user's name
            DisplayUserName(view);

            // Set the "Upcoming Sessions" list view to display (upto) the four closest sessions
            DisplayUpcomingBookings(view);

            return view;
        }

        private void DisplayUpcomingBookings(View view)
        {
            // {Architecture} change the code to generate the list view data from the user's data
            List<Booking> sessionsList = new List<Booking>();

            sessionsList.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));
            sessionsList.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));
            sessionsList.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));

            sessionsList.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));
            sessionsList.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));
            sessionsList.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));

            ListView upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);

            upcomingList.Adapter = new BookingBaseAdapter(this.Activity, sessionsList);
        }

        private void DisplayUserName(View view)
        {
            // {Architecture} Get from the database.
            var studentData = JsonConvert.DeserializeObject<StudentData>(this.Activity.Intent.GetStringExtra("student"));

            string helloUser = GetString(Resource.String.hello) + " " + studentData.attributes.studentID + "!";
            TextView helloUserText = view.FindViewById<TextView>(Resource.Id.textHelloUser);

            helloUserText.Text = helloUser;
        }
    }
}