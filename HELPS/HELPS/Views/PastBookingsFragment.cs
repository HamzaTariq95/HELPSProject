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
using Newtonsoft.Json;
using HELPS.Model;
using HELPS.Controllers;

namespace HELPS.Views
{
    public class PastBookingsFragment : Fragment
    {
        private StudentData studentData;
        private SessionBookingData sessionBookingData;
        private WorkshopBookingData workshopBookingData;

        public PastBookingsFragment(SessionBookingData sessionBookingData, WorkshopBookingData workshopBookingData, StudentData studentData)
        {
            this.sessionBookingData = sessionBookingData;
            this.workshopBookingData = workshopBookingData;
            this.studentData = studentData;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.BookingsLayout, container, false);
            //Get student data from intent in parent activity
            studentData = JsonConvert.DeserializeObject<StudentData>(this.Activity.Intent.GetStringExtra("student"));

            // Set the "Past Bookings" list view to display (upto) the four closest sessions
            DisplayPastBookings(view);

            return view;
        }

        private void DisplayPastBookings(View view)
        {
            List<Booking> bookings = new List<Booking>();

            if (sessionBookingData == null && workshopBookingData == null)
            {
                //Display on screen: no bookings found
            }
            else
            {
                addBookingsToList(bookings, sessionBookingData, workshopBookingData);
            }

            ListView upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);
            upcomingList.Adapter = new BookingBaseAdapter(Activity, bookings);
        }

        private void addBookingsToList(List<Booking> bookings, SessionBookingData sessionBookingData, WorkshopBookingData workshopBookingData)
        {
            addSessionBookingsToList(sessionBookingData, bookings);
            addWorkshopBookingsToList(workshopBookingData, bookings);
        }
        private void addWorkshopBookingsToList(WorkshopBookingData workshopBookingData, List<Booking> bookings)
        {
            foreach (WorkshopBooking workshopBooking in workshopBookingData.attributes)
            {
                if (workshopBooking.starting < DateTime.Now &&
                    workshopBooking.Status().Equals("Booked") &&
                    workshopBooking.BookingArchived == null &&
                    workshopBooking.WorkshopArchived == null)
                    bookings.Add(workshopBooking);
            }
        }

        private void addSessionBookingsToList(SessionBookingData sessionBookingData, List<Booking> bookings)
        {
            foreach (SessionBooking sessionBooking in sessionBookingData.attributes)
            {
                if (sessionBooking.StartDate < DateTime.Now &&
                    sessionBooking.Status().Equals("Booked") &&
                    sessionBooking.archived == null)
                    bookings.Add(sessionBooking);
            }
        }
    }
}