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
    class WorkshopBooking
    {

        public int BookingId { get; set; }
        public Nullable<int> workshopID { get; set; }
        public string studentID { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public string targetingGroup { get; set; }
        public Nullable<int> campusID { get; set; }
        public Nullable<System.DateTime> starting { get; set; }
        public Nullable<System.DateTime> ending { get; set; }
        public Nullable<int> maximum { get; set; }
        public Nullable<int> cutoff { get; set; }
        public Nullable<int> canceled { get; set; }
        public Nullable<int> attended { get; set; }
        public Nullable<int> WorkShopSetID { get; set; }
        public string type { get; set; }
        public Nullable<int> reminder_num { get; set; }
        public Nullable<int> reminder_sent { get; set; }
        public Nullable<System.DateTime> WorkshopArchived { get; set; }
        public Nullable<System.DateTime> BookingArchived { get; set; }


    }
}