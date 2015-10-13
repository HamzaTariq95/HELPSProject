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

namespace HELPS
{
    class BookingViewHolder : Java.Lang.Object
    {
        public TextView bookedSessionTitle { get; set; }
        public TextView bookedSessionStatus { get; set; }
        public TextView bookedSessionDate { get; set; }
        public TextView bookedSessionLocation { get; set; }
        public TextView bookedSessionTutor { get; set; }
        public TextView bookedSessionType { get; set; }
    }
}