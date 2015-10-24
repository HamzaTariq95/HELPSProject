using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using HELPS.Model;
using Newtonsoft.Json;
using HELPS.Controllers;
using Android.Util;

namespace HELPS.Views.Activities
{
    [Activity(Label = "BookingDetailActivity")]
    public class BookingDetailActivity : AppCompatActivity
    {
        private SupportToolbar _Toolbar;
        private LinearLayout _Booked, _NotBooked;
        private TextView _Title, _Date, _Description;
        private Booking _Booking;
        private Workshop _Workshop;
        private string bookingType;

        //private WorkshopBooking _WorkshopBooking;
        //private string studentId;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Check request type and set appropriate variables
            SetVariables();

            // Set our view from the "BookingDetail" layout resource.
            SetContentView(Resource.Layout.BookingDetail);

            // Set the toolbar
            _Toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_Toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Set up the TextViews
            _Title = FindViewById<TextView>(Resource.Id.detailTitle);
            _Date = FindViewById<TextView>(Resource.Id.detailDate);
            _Description = FindViewById<TextView>(Resource.Id.detailDescription);

            _Booked = FindViewById<LinearLayout>(Resource.Id.bookedButtons);
            _Booked.Visibility = ViewStates.Gone;

            _NotBooked = FindViewById<LinearLayout>(Resource.Id.notBookedButtons);
            _NotBooked.Visibility = ViewStates.Gone;

            // Send an intent that tells the activity if the session is booked by the student or not.
            if (_Booking != null)
            {
                SetBookingView();
            }
            //else if(_WorkshopBooking !=null)
            //{
              //  SetBookingView();
            //}
            else // _workshop != null
            {
                SetWorkshopView();
            }
        }

        private void SetWorkshopView()
        {
            // Display the booking button
            _NotBooked.Visibility = ViewStates.Visible;

            // Populate the TextViews
            SupportActionBar.Title = "Book Workshop";
            _Title.Text = _Workshop.Title();
            DateTime? date = _Workshop.Date();
            _Date.Text = (date == null) ? "Not available" : date.ToString();
            _Description.Text = _Workshop.description;

            Button bookButton = FindViewById<Button>(Resource.Id.buttonBook);
            bookButton.Click += delegate
            {
                Book();
                Server.workshopBookingsAltered = true;
                Finish();
            };
        }

        private void SetBookingView()
        {
            _Booked.Visibility = ViewStates.Visible;

            // Set up the title
            SupportActionBar.Title = "View Booking";
            _Booking.Title();
            // Populate TextViews
            _Title.Text = _Booking.Title();
            DateTime? date = _Booking.Date();
            _Date.Text = (date == null) ? "Not available" : date.ToString();
            _Description.Text = _Booking.Description();

            Button cancelButton = FindViewById<Button>(Resource.Id.buttonCancelBooking);
            cancelButton.Click += delegate
            {
                CancelBooking();


            };
            if (date < DateTime.Now)
                cancelButton.Visibility = ViewStates.Gone;
            
            
        }

        private void SetVariables()
        {
            //studentId = Intent.GetStringExtra("studentId");
            string requestType = Intent.GetStringExtra("requestType");

            if (requestType == "showAvailableWorkshop")
            {
                _Workshop = JsonConvert.DeserializeObject<Workshop>(Intent.GetStringExtra("workshop"));
                
            }
               
            else // requestType == "showBooking"
            {
                bookingType = Intent.GetStringExtra("bookingType");
                string bookingString = Intent.GetStringExtra("booking");

                if (bookingType == "Session")
                {
                    SessionBooking sessionBooking = JsonConvert.DeserializeObject<SessionBooking>(bookingString);
                    _Booking = sessionBooking;
                }
                else // bookingType == "Workshop"
                {
                    WorkshopBooking workshopBooking = JsonConvert.DeserializeObject<WorkshopBooking>(bookingString);
                    _Booking = workshopBooking;
                }
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            OnBackPressed();
            return base.OnOptionsItemSelected(item);
        }

        // Sends the user to the notification settings
        private void DisplayNotificationSettings()
        {
            // Implement method
            // Send the booking to the notification activity to change the notification for the specific booking
        }

        // Warns user that they will delete this booking and removes the booking if user presses okay
        private void CancelBooking()
        {
            AlertDialog.Builder cancelAlert = new AlertDialog.Builder(this);

            cancelAlert.SetTitle(GetString(Resource.String.cancelBooking));
            cancelAlert.SetMessage(GetString(Resource.String.areYouSureCancel));
            cancelAlert.SetPositiveButton("YES", delegate
            {
                // Code to cancel booking.
                WorkshopController workshopController = new WorkshopController();

                if(!workshopController.CancelBooking(_Booking.ID()))
                {
                    //show error, stay on page;
                }

                else
                {
                    //show dialog saying cancled
                    var SuccesDialog = new AlertDialog.Builder(this);
                    SuccesDialog.SetMessage("Booking has been Cancled!");
                    SuccesDialog.SetNeutralButton("OK", delegate { Finish(); });
                    SuccesDialog.Show();

                    if (bookingType.Equals("Session"))
                        Server.sessionBookingsAltered = true;
                    else
                        Server.workshopBookingsAltered = true;
                }

            });
            cancelAlert.SetNegativeButton("NO", delegate { });
            cancelAlert.Show();
        }

        private void Book()
        {
            // Code to book the workshop
            WorkshopController workshopController = new WorkshopController();
            if (!workshopController.Book(_Workshop.WorkshopId))
            {
                //show error , stay on page;
            }
            else
            { //show dialog saying booked and return}
            }
        }
    }
}