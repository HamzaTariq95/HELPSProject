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
    class Workshop
    {
        public int WorkshopId { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public string targetingGroup { get; set; }
        public string campus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? maximum { get; set; }
        public int? WorkShopSetID { get; set; }
        public int? cutoff { get; set; }
        public string type { get; set; }
        public int? reminder_num { get; set; }
        public int? reminder_sent { get; set; }
        public string DaysOfWeek { get; set; }
        public int? BookingCount { get; set; }
        public DateTime? archived { get; set; }


    /*
        public Workshop(int workshopId, string topic, string description,
            string targetingGroup, string campus, DateTime? startDate,
            DateTime? endDate, int? maximum, int? workshopSetId, int? cutoff,
            string type, int? reminder_num, int? reminders_sent, string daysOfWeek,
            int? bookingCount, DateTime? archived)
        {
            this.WorkshopId = workshopTitle;
            this.topic = workshopStatus;
            this.description = workshopDate;
            this.targetingGroup = workshopTarget;
            this.campus = workshopLocation;
            this.StartDate = workshopTutor;
            this.EndDate = workshopType;
            this.maximum = workshopDescription;
            this.WorkShopSetID = workshopDescription;
            this.cutoff = workshopDescription;
            this.type = workshopDescription;
            this.reminder_num = workshopDescription;
            this.reminder_sent = workshopDescription;
            this.DaysOfWeek = workshopDescription;
            this.BookingCount = workshopDescription;
            this.archived = workshopDescription;
        }*/
    }
}