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

namespace HELPS.Views
{
    public class PastBookingsFragment : Fragment
    {
        private StudentData studentData;
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

            // Set the "Upcoming Sessions" list view to display (upto) the four closest sessions
            DisplayUpcomingBookings(view);

            return view;
        }

        private void DisplayUpcomingBookings(View view)
        {
            SessionController sessionController = new SessionController();
            SessionBookingData sessionBookingData = sessionController.GetSessionBookingData(studentData.attributes.studentID);
            List<Booking> bookings = new List<Booking>();

            if (sessionBookingData == null)
            {
                //Display on screen: no bookings found
            }
            else
            {
                addSessionBookingToList(sessionBookingData, bookings);
            }
            // {Architecture} change the code to generate the list view data from the user's data



            //bookings.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));
            //bookings.Add(new SessionBooking(false, Convert.ToDateTime("01/01/2015"), "B1.05.202", "Mr Tutor", "type"));

            bookings.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));
            bookings.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));
            bookings.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));

            ListView upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);
            upcomingList.Adapter = new BookingBaseAdapter(Activity, bookings);
        }

        private void addSessionBookingToList(SessionBookingData sessionBookingData, List<Booking> bookings)
        {
            foreach (SessionBooking sessionBooking in sessionBookingData.attributes)
            {
                if (sessionBooking.EndDate < DateTime.Now)
                    bookings.Add(sessionBooking);
            }
        }
    }
}