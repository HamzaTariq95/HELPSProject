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

namespace HELPS.Views
{
    public class PastBookingsFragment : Fragment, AdapterView.IOnItemClickListener
    {
        private StudentData studentData;
        private SessionBookingData sessionBookingData;
        private WorkshopBookingData workshopBookingData;
        private List<Booking> bookings;

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

            sessionBookingData = Server.currentSessionBookingData;
            workshopBookingData = Server.currentWorkshopBookingData;

            // Set the "Past Bookings" list view to display (upto) the four closest sessions
            DisplayPastBookings(view);

            return view;
        }

        private void DisplayPastBookings(View view)
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

            //Sort bookings by date
            //bookings.Sort((b, a) => a.Date().ToString().CompareTo(b.Date().ToString()));
            bookings.Sort((b, a) => DateTime.Compare(a.Date() ?? DateTime.MaxValue, b.Date() ?? DateTime.MaxValue));

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
                if (workshopBooking.starting < DateTime.Now)
                    bookings.Add(workshopBooking);
            }
        }

        private void addSessionBookingsToList(SessionBookingData sessionBookingData, List<Booking> bookings)
        {
            foreach (SessionBooking sessionBooking in sessionBookingData.attributes)
            {
                if (sessionBooking.StartDate < DateTime.Now)
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