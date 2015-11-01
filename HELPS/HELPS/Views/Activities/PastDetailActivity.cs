using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using HELPS.Model;
using Android.Content.PM;

namespace HELPS.Views.Activities
{
    [Activity(Label = "PastDetailActivity", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/icon")]
    public class PastDetailActivity : AppCompatActivity
    {
        private SupportToolbar _Toolbar;
        private TextView _Title, _Date, _Location, _Tutor, _Type, _Description;
        private Booking _Booking;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "BookingDetail" layout resource.
            SetContentView(Resource.Layout.PastDetail);

            // Set the toolbar
            _Toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_Toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Set up the TextViews
            _Title = FindViewById<TextView>(Resource.Id.detailTitle);
            _Date = FindViewById<TextView>(Resource.Id.detailDate);
            _Location = FindViewById<TextView>(Resource.Id.detailLocation);
            _Tutor = FindViewById<TextView>(Resource.Id.detailTutor);
            _Type = FindViewById<TextView>(Resource.Id.detailType);
            _Description = FindViewById<TextView>(Resource.Id.detailDescription);

            // Retrive the booking
            GetBooking();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            OnBackPressed();
            return base.OnOptionsItemSelected(item);
        }

        private void GetBooking()
        {
            // Code to check intents.
        }
    }
}