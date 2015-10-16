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
        }
    }
}