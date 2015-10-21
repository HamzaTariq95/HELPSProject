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
using HELPS.Model;

namespace HELPS.Views
{
    class BookingBaseAdapter : BaseAdapter<Booking>
    {
        private Activity _Context;
        private List<Booking> _Bookings;

        public BookingBaseAdapter(Activity context, List<Booking> bookings)
        {
            _Context = context;
            _Bookings = bookings;
        }

        public override int Count
        {
            get
            {
                return _Bookings.Count;
            }
        }

        public override Booking this[int position]
        {
            get
            {
                return _Bookings[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            BookingViewHolder holder = null;
            var view = convertView;

            if (view != null)
            {
                holder = view.Tag as BookingViewHolder;
            }

            if (holder == null)
            {
                holder = new BookingViewHolder();
                view = _Context.LayoutInflater.Inflate(Resource.Layout.BookedSessionsRow, null);
                
                holder.bookedSessionTitle = view.FindViewById<TextView>(Resource.Id.bookedSessionTitle);
                holder.bookedSessionStatus = view.FindViewById<TextView>(Resource.Id.bookedSessionStatus);
                holder.bookedSessionDate = view.FindViewById<TextView>(Resource.Id.bookedSessionDate);
                //holder.bookedSessionLocation = view.FindViewById<TextView>(Resource.Id.bookedSessionLocation);
                //holder.bookedSessionTutor = view.FindViewById<TextView>(Resource.Id.bookedSessionTutor);
                holder.bookedSessionType = view.FindViewById<TextView>(Resource.Id.bookedSessionType);

                view.Tag = holder;
            }

            // Sets the list row to display the session data.
            holder.bookedSessionTitle.Text = _Bookings[position].Title();
            holder.bookedSessionStatus.Text = _Bookings[position].Status();
            DateTime? date = _Bookings[position].Date();
            holder.bookedSessionDate.Text = (date == null) ? "Not available":date.ToString();
            //holder.bookedSessionLocation.Text = _Bookings[position].Location();
            //holder.bookedSessionTutor.Text = _Bookings[position].Tutor();
            holder.bookedSessionType.Text = _Bookings[position].Type();

            return view;
        }
    }
}