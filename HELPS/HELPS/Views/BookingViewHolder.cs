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