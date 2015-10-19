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
    class SearchWorkshopsBaseAdapter : BaseAdapter<Workshop>
    {
        private Activity _Context;
        private List<Workshop> _Workshops;

        public SearchWorkshopsBaseAdapter(Activity context, List<Workshop> workshops)
        {
            _Context = context;
            _Workshops = workshops;
        }

        public override int Count
        {
            get
            {
                return _Workshops.Count;
            }
        }

        public override Workshop this[int position]
        {
            get
            {
                return _Workshops[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            SearchViewHolder holder = null;
            var view = convertView;

            if (view != null)
            {
                holder = view.Tag as SearchViewHolder;
            }

            if (holder == null)
            {
                holder = new SearchViewHolder();
                view = _Context.LayoutInflater.Inflate(Resource.Layout.SearchWorkshopsRow, null);

                holder.searchWorkshopTitle = view.FindViewById<TextView>(Resource.Id.searchWorkshopTitle);
                holder.searchWorkshopStatus = view.FindViewById<TextView>(Resource.Id.searchWorkshopStatus);
                holder.searchWorkshopDate = view.FindViewById<TextView>(Resource.Id.searchWorkshopDate);
                holder.searchWorkshopTarget = view.FindViewById<TextView>(Resource.Id.searchWorkshopTarget);
                holder.searchWorkshopTutor = view.FindViewById<TextView>(Resource.Id.searchWorkshopTutor);
                holder.searchWorkshopType = view.FindViewById<TextView>(Resource.Id.searchWorkshopType);

                view.Tag = holder;
            }

            // Sets the list row to display the session data.
        /*    holder.searchWorkshopTitle.Text = _Workshops[position].Title();
            holder.searchWorkshopStatus.Text = _Workshops[position].Status();
            DateTime? date = _Workshops[position].Date();
            holder.searchWorkshopDate.Text = (date == null) ? "Not available" : date.ToString();
            // holder.searchWorkshopTarget.Text = _Workshops[position].Target();
            holder.searchWorkshopTutor.Text = _Workshops[position].Tutor();
            holder.searchWorkshopType.Text = _Workshops[position].Type(); */

            holder.searchWorkshopTitle.Text = _Workshops[position].topic;
            string date = "" +_Workshops[position].StartDate;
            holder.searchWorkshopDate.Text = (date == null) ? "Not available" : date.ToString();
            // holder.searchWorkshopTarget.Text = _Workshops[position].Target();
          

            return view;
        }
    }
} 