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
        private StudentData studentData;
        private int maxBookings = 0;
        private int sessionBookings = 0;
        private int workShopBookings = 0;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.MainLayout, container, false);

            //Get student data from intent in parent activity
            studentData = JsonConvert.DeserializeObject<StudentData>(this.Activity.Intent.GetStringExtra("student"));

            // Set the "Hello User" text view to display the user's name
            DisplayUserName(view);

            // Set the "Upcoming Sessions" list view to display (upto) the four closest sessions
            DisplayUpcomingBookings(view);

            return view;
        }

        private void DisplayUpcomingBookings(View view)
        {
            SessionController sessionController = new SessionController();
            SessionBookingData sessionBookingData = sessionController.GetSessionBookingData(studentData.attributes.studentID);
            List<Booking> bookings = new List<Booking>();

            if (sessionBookingData == null /* && workShopBookingData == null*/)
            {
                //Display on screen: no bookings found
            }
            else
            {
                addBookingsToList(bookings, sessionBookingData /*,workShopBookingData*/);
            }
         
            ListView upcomingList = view.FindViewById<ListView>(Resource.Id.listUpcoming);
            upcomingList.Adapter = new BookingBaseAdapter(Activity, bookings);
        }

        private void addBookingsToList(List<Booking> bookings, SessionBookingData sessionBookingData)
        {
            addSessionBookingsToList(sessionBookingData, bookings);
            addWorkshopBookingsToList(/*sessionBookingData,*/ bookings);
        }

        private void addWorkshopBookingsToList(/*sessionBookingData,*/ List<Booking> bookings)
        {
            /*foreach (SessionBooking sessionBooking in sessionBookingData.attributes)
            {
                if (sessionBooking.StartDate > DateTime.Now && sessionBooking.Status().Equals("Booked"))
                    bookings.Add(sessionBooking);
            }*/
            bookings.Add(new WorkshopBooking(1, Convert.ToDateTime("01/01/2015"), 123, 456));
        }

        private void addSessionBookingsToList(SessionBookingData sessionBookingData, List<Booking> bookings)
        {
            foreach (SessionBooking sessionBooking in sessionBookingData.attributes)
            {
                if (sessionBooking.StartDate > DateTime.Now && sessionBooking.Status().Equals("Booked"))
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