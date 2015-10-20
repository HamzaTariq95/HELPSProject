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
using HELPS.Controllers;

namespace HELPS
{
    public class LandingFragment : Fragment
    {
        private StudentData studentData;
        private SessionBookingData sessionBookingData;
        private WorkshopBookingData workshopBookingData;
       
        public LandingFragment(SessionBookingData sessionBookingData, WorkshopBookingData workshopBookingData, StudentData studentData)
        {
            this.sessionBookingData = sessionBookingData;
            this.workshopBookingData = workshopBookingData;
            this.studentData = studentData;
        }
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
            List<Booking> bookings = new List<Booking>();

            if (sessionBookingData == null && workshopBookingData == null)
            {
                //show message
            }
            else
            {
                addBookingsToList(bookings, sessionBookingData, workshopBookingData);
            }

            ListView upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);
            upcomingList.Adapter = new BookingBaseAdapter(Activity, bookings.Take(4).ToList());
        }

        private void addBookingsToList(List<Booking> bookings, SessionBookingData sessionBookingData, WorkshopBookingData workshopBookingData)
        {
            addSessionBookingsToList(sessionBookingData, bookings);
            addWorkshopBookingsToList(workshopBookingData, bookings);

            //Sort bookings by date
            bookings.Sort((a, b) => a.Date().ToString().CompareTo(b.Date().ToString()));
        }

        private void addWorkshopBookingsToList(WorkshopBookingData workshopBookingData,  List<Booking> bookings)
        {
            foreach (WorkshopBooking workshopBooking in workshopBookingData.attributes)
            {
                if (workshopBooking.starting > DateTime.Now)
                    bookings.Add(workshopBooking);
            }
        }

        private void addSessionBookingsToList(SessionBookingData sessionBookingData, List<Booking> bookings)
        {
            foreach (SessionBooking sessionBooking in sessionBookingData.attributes)
            {
                if (sessionBooking.StartDate > DateTime.Now && 
                    sessionBooking.Status().Equals("Booked") &&
                    sessionBooking.archived == null)
                    bookings.Add(sessionBooking);
            }
        }

        private void DisplayUserName(View view)
        {
            // {Architecture} Get from the database.
            string helloUser = GetString(Resource.String.hello) + " " + studentData.attributes.studentID + "!";
            TextView helloUserText = view.FindViewById<TextView>(Resource.Id.textHelloUser);

            helloUserText.Text = helloUser;
        }
    }
}