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
    public class WorkshopBooking :Booking
    {

        public int BookingId { get; set; }
        public int? workshopID { get; set; }
        public string studentID { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public string targetingGroup { get; set; }
        public int? campusID { get; set; }
        public DateTime? starting { get; set; }
        public DateTime? ending { get; set; }
        public int? maximum { get; set; }
        public int? cutoff { get; set; }
        public int? canceled { get; set; }
        public int? attended { get; set; }
        public int? WorkShopSetID { get; set; }
        public string type { get; set; }
        public int? reminder_num { get; set; }
        public int? reminder_sent { get; set; }
        public DateTime? WorkshopArchived { get; set; }
        public DateTime? BookingArchived { get; set; }

        public WorkshopBooking(int sessionStatus, DateTime sessionDate,
            int sessionLocation, int sessionType)
        {
            this.canceled = sessionStatus;
            this.starting = sessionDate;
            this.campusID = sessionLocation;
            this.WorkShopSetID = sessionType;
        }

        public override string Title()
        {
            return "Workshop";
        }

        public override string Status()
        {
            if (canceled != null)
                return "Booked";

            return "Canceled";
        }

        public override DateTime? Date()
        {
            return starting;
        }

        public override string Location()
        {
            return "" + campusID; //TO DO: get campus name from db
        }

        public override string Tutor()
        {
            return "N/A"; //No tutor data available in workshop related tables
        }

        public override string Type()
        {
            return "" + WorkShopSetID; //TO DO: get type from workshopset db
        }
    }
}