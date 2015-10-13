using System;
using System.Collections;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using System.Collections.Generic;
using HELPS.Model;

namespace HELPS
{
    class BookedSessionsBaseAdapter : BaseAdapter<SessionBooking>
    {
        private Activity _Context;
        private List<SessionBooking> _Sessions;
        
        public BookedSessionsBaseAdapter(Activity context, List<SessionBooking> sessions)
        {
            _Context = context;
            _Sessions = sessions;
        }

        public override int Count
        {
            get
            {
                return _Sessions.Count;
            }
        }

        public override SessionBooking this[int position]
        {
            get
            {
                return _Sessions[position];
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
                holder.bookedSessionLocation = view.FindViewById<TextView>(Resource.Id.bookedSessionLocation);
                holder.bookedSessionTutor = view.FindViewById<TextView>(Resource.Id.bookedSessionTutor);
                holder.bookedSessionType = view.FindViewById<TextView>(Resource.Id.bookedSessionType);

                view.Tag = holder;
            }

            // Sets the list row to display the session data.
            holder.bookedSessionTitle.Text = _Sessions[position].Title();
            holder.bookedSessionStatus.Text = _Sessions[position].Status();
            DateTime? date = _Sessions[position].Date();
            holder.bookedSessionDate.Text = (date == null) ? "Not available" : date.ToString();
            holder.bookedSessionLocation.Text = _Sessions[position].Location();
            holder.bookedSessionTutor.Text = _Sessions[position].Tutor();
            holder.bookedSessionType.Text = _Sessions[position].Type();

            return view;
        }
    }
}