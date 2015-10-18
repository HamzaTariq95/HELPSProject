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
        public Object StartDate { get; set; }
        public string EndDate { get; set; }
        public string maximum { get; set; }
        public string WorkShopSetID { get; set; }
        public object cutoff { get; set; }
        public string type { get; set; }
        public string reminder_num { get; set; }
        public string reminder_sent { get; set; }
        public object DaysOfWeek { get; set; }
        public string BookingCount { get; set; }
        public string archived { get; set; }


        /*public int WorkshopId { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public string targetingGroup { get; set; }
        public string campus { get; set; }
        public Object StartDate { get; set; }
        public string EndDate { get; set; }
        public int maximum { get; set; }
        public int WorkShopSetID { get; set; }
        public object cutoff { get; set; }
        public string type { get; set; }
        public int reminder_num { get; set; }
        public int reminder_sent { get; set; }
        public object DaysOfWeek { get; set; }
        public int BookingCount { get; set; }
        public string archived { get; set; } */
  
    }
}