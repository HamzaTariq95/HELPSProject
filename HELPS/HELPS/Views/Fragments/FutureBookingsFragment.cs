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
using HELPS.Views.Activities;
using HELPS.Model.JSONDataClasses;

namespace HELPS.Views
{
    public class FutureBookingsFragment : Fragment, AdapterView.IOnItemClickListener
    {
        private StudentData studentData;
        private SessionBookingData sessionBookingData;
        private WorkshopBookingData workshopBookingData;
        private List<Booking> bookings;
        //private CampusData campusData;

        public FutureBookingsFragment(SessionBookingData sessionBookingData, WorkshopBookingData workshopBookingData, StudentData studentData)
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

            // Set the "Upcoming Sessions" list view to display (upto) the four closest sessions
            DisplayUpcomingBookings(view);

            return view;
        }

        private void DisplayUpcomingBookings(View view)
        {
            bookings = new List<Booking>();

            if (sessionBookingData == null && workshopBookingData == null)
            {
                //Display on screen: no bookings found
            }
            else
            {
                addBookingsToList(bookings, sessionBookingData, workshopBookingData);
            }

            ListView upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);
            upcomingList.OnItemClickListener = this;
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
                if (workshopBooking.starting > DateTime.Now && !workshopBooking.Status().Equals("Canceled booking"))
                    bookings.Add(workshopBooking);
            }
        }

        private void addSessionBookingsToList(SessionBookingData sessionBookingData, List<Booking> bookings)
        {
            foreach (SessionBooking sessionBooking in sessionBookingData.attributes)
            {
                if (sessionBooking.StartDate > DateTime.Now && !sessionBooking.Status().Equals("Canceled booking"))
                    bookings.Add(sessionBooking);
            }
        }

        
            public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Booking booking = bookings[position];

            string bookingString = JsonConvert.SerializeObject(booking);

            string bookingType;

            if (booking.Title().Equals("Session")) bookingType = "Session";
            else bookingType = "Workshop";

            Intent intent = new Intent(Application.Context, typeof(BookingDetailActivity));
            intent.PutExtra("requestType", "showBooking");
            intent.PutExtra("bookingType", bookingType);
            intent.PutExtra("booking", bookingString);
            StartActivity(intent);
        }
    
    }
}