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
        private StudentData _StudentData;
        private SessionBookingData _SessionBookingData;
        private WorkshopBookingData _WorkshopBookingData;
        private List<Booking> _Bookings;
        private TextView _NoDisplay;

        public PastBookingsFragment(SessionBookingData sessionBookingData, WorkshopBookingData workshopBookingData, StudentData studentData)
        {
            this._SessionBookingData = sessionBookingData;
            this._WorkshopBookingData = workshopBookingData;
            this._StudentData = studentData;
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
            _StudentData = JsonConvert.DeserializeObject<StudentData>(this.Activity.Intent.GetStringExtra("student"));

            _SessionBookingData = Server.currentSessionBookingData;
            _WorkshopBookingData = Server.currentWorkshopBookingData;

            _NoDisplay = view.FindViewById<TextView>(Resource.Id.textNoBooking);
            _NoDisplay.Visibility = ViewStates.Gone;
            // Set the "Past Bookings" list view to display (upto) the four closest sessions
            DisplayPastBookings(view);

            return view;
        }

        private void DisplayPastBookings(View view)
        {
            _Bookings = new List<Booking>();

            if (_SessionBookingData == null && _WorkshopBookingData == null)
            {
                //Display on screen: no bookings found          
                _NoDisplay.Visibility = ViewStates.Visible;
            }
            else
            {
                addBookingsToList(_Bookings, _SessionBookingData, _WorkshopBookingData);
            }

            ListView upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);
            upcomingList.OnItemClickListener = this;

            //Sort bookings by date
            //bookings.Sort((b, a) => a.Date().ToString().CompareTo(b.Date().ToString()));
            _Bookings.Sort((b, a) => DateTime.Compare(a.Date() ?? DateTime.MaxValue, b.Date() ?? DateTime.MaxValue));

            //show last 10 bookings
            _Bookings = _Bookings.Take(10).ToList();

            upcomingList.Adapter = new BookingBaseAdapter(Activity, _Bookings);
        }
           

        private void addBookingsToList(List<Booking> bookings, SessionBookingData _SessionBookingData, WorkshopBookingData _WorkshopBookingData)
        {
            addSessionBookingsToList(_SessionBookingData, bookings);
            addWorkshopBookingsToList(_WorkshopBookingData, bookings);
        }
        private void addWorkshopBookingsToList(WorkshopBookingData _WorkshopBookingData, List<Booking> bookings)
        {
            foreach (WorkshopBooking workshopBooking in _WorkshopBookingData.attributes)
            {
                if (workshopBooking.starting < DateTime.Now)
                    bookings.Add(workshopBooking);
            }
        }

        private void addSessionBookingsToList(SessionBookingData _SessionBookingData, List<Booking> bookings)
        {
            foreach (SessionBooking sessionBooking in _SessionBookingData.attributes)
            {
                if (sessionBooking.StartDate < DateTime.Now)
                    bookings.Add(sessionBooking);
            }
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Booking booking = _Bookings[position];
            
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