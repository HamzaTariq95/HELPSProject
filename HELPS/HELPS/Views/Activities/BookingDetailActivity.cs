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

namespace HELPS.Views.Activities
{
    [Activity(Label = "BookingDetailActivity")]
    public class BookingDetailActivity : AppCompatActivity
    {
        private SupportToolbar _Toolbar;
        private LinearLayout _Booked;
        private LinearLayout _NotBooked;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "BookingDetail" layout resource.
            SetContentView(Resource.Layout.BookingDetail);

            // Set the toolbar
            _Toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_Toolbar);

            // {Architecture} Pass the workshop title as the toolbar title
            // SupportActionBar.Title(<pass here>)

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _Booked = FindViewById<LinearLayout>(Resource.Id.bookedButtons);
            _Booked.Visibility = ViewStates.Gone;

            _NotBooked = FindViewById<LinearLayout>(Resource.Id.notBookedButtons);
            _NotBooked.Visibility = ViewStates.Gone;

            // Send an intent that tells the activity if the session is booked by the student or not.
            // if (booked)
            // {
                    // _Booked.Visibility = ViewStates.Visible        

                    // Set up notification button
                    Button changeNotificationButton = FindViewById<Button>(Resource.Id.buttonChangeNotification);
                    changeNotificationButton.Click += delegate
                    {
                        DisplayNotificationSettings();
                    };

                    //Set up cancel button
                    Button cancelButton = FindViewById<Button>(Resource.Id.buttonCancelBooking);
                    cancelButton.Click += delegate
                    {
                        CancelBooking();
                    };
            // }
            // else
            // {
            // Display the booking button
                    // _NotBooked.Visibility = ViewStates.Visible 
                    // Button bookButton = FindViewById<Button>(Resource.Id.buttonBook)
                    // bookButton.Click += delegate
                    // {
                    //      Book();
                    // }
            // }
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
            });
            cancelAlert.SetNegativeButton("NO", delegate { });
            cancelAlert.Show();
        }

        private void Book()
        {
            // Code to book the workshop
        }
    }
}