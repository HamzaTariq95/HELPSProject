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

namespace HELPS.Model
{
    public class Workshop
    {

        public string WorkshopId { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public string targetingGroup { get; set; }
        public string campus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int maximum { get; set; }
        public string WorkShopSetID { get; set; }
        public object cutoff { get; set; }
        public string type { get; set; }
        public int reminder_num { get; set; }
        public int reminder_sent { get; set; }
        public object DaysOfWeek { get; set; }
        public int BookingCount { get; set; }
        public object archived { get; set; }

        public string Title()
        {
            return WorkShopSetID;
        }

        public string Status()
        {
            if (BookingCount < maximum)
                return "Book";

            return "Waitlist";
        }

        public DateTime? Date()
        {
            return  StartDate;
        }

        public string Tutor()
        {
            return "N/A"; //No tutor data available in workshop 									related tables
        }

        public string Type()
        {
            return type;
        }


        public string Topic()
        {
            return topic;
        }

        public string Target()
        {
            return targetingGroup;
        }

    }
}