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
using AlertDialog = Android.Support.V7.App.AlertDialog;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;

namespace HELPS.Views.Activities
{
    [Activity(Label = "BookingDetailActivity")]
    public class BookingDetailActivity : AppCompatActivity
    {
        private SupportToolbar _Toolbar;

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

            // Set up notification button
            Button changeNotificationButton = FindViewById<Button>(Resource.Id.buttonChangeNotification);
            changeNotificationButton.Click += delegate
            {
                DisplayNotificationSettings();
            };

            //Set up cancel button
            Button cancelButton = FindViewById<Button>(Resource.Id.buttonCancelBooking);
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
    }
}