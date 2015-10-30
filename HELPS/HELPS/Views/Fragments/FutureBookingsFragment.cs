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
        private ListView upcomingList;
        private BookingBaseAdapter adapter;
        private TextView _NoDisplay;

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
            //Server.futureBookings.Clear();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.BookingsLayout, container, false);

            sessionBookingData = Server.currentSessionBookingData;
            workshopBookingData = Server.currentWorkshopBookingData;

            _NoDisplay = view.FindViewById<TextView>(Resource.Id.textNoBooking);
            
            // Set the "Upcoming Sessions" list view to display (upto) the four closest sessions
            DisplayUpcomingBookings(view);

            return view;
        }

        private void DisplayUpcomingBookings(View view)
        {
            //Server.futureBookings.Clear();
            bookings = new List<Booking>();
            if (sessionBookingData == null && workshopBookingData == null)
            {
                //Display on screen: no bookings found
               // _NoDisplay.Visibility = ViewStates.Visible;
            }
            else
            {
                //_NoDisplay.Visibility = ViewStates.Gone;
                addBookingsToList(bookings, sessionBookingData, workshopBookingData);
            }

            upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);
            upcomingList.OnItemClickListener = this;

            //Sort bookings by date
            //bookings.Sort((a, b) => a.Date().ToString().CompareTo(b.Date().ToString()));
            bookings.Sort((a, b) => DateTime.Compare(a.Date() ?? DateTime.MaxValue, b.Date() ?? DateTime.MaxValue));
            adapter = new BookingBaseAdapter(Activity, bookings);
            upcomingList.Adapter = adapter;
            //upcomingList.Adapter.RegisterDataSetObserver();
          
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
                Console.WriteLine(workshopBooking.Status());
                if (workshopBooking.starting > DateTime.Now && !workshopBooking.Status().Equals("Canceled booking"))
                {
                    bookings.Add(workshopBooking);
                    //Server.futureBookings.Add(workshopBooking);
                }
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
            //intent.PutExtra("studentId", studentData.attributes.);
            StartActivity(intent);
        }
    }
}