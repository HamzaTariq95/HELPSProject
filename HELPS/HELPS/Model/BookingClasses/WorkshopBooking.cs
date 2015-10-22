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
            if (starting > DateTime.Now && BookingArchived == null && WorkshopArchived == null)
                return "Booked";
            if (BookingArchived != null)
                return "Canceled booking";
            if (WorkshopArchived != null)
                return "Canceled";
            //if (ending < DateTime.Now && canceled == null && attended == null && BookingArchived == null)
              //  return "Did not attend";
            //if (attended != null && BookingArchived == null && ending < DateTime.Now && canceled != null)
              //  return "Attended";
            return "";
        }

        public override DateTime? Date()
        {
            return starting;
        }

        public override string Location()
        {
            string campus;
            try
            {
                campus = Constants.CAMPUSES.Where(x => x.id == campusID).First().campus;
            }
            catch (InvalidOperationException e)
            {
                return "TBA";
            }

            return campus;
        }

        public override string Tutor()
        {
            return ""; //No tutor data available in workshop related tables
        }

        public override string Type()
        {
            /* string workshop;
             try
             {
                 workshop = Constants.WORKSHOP_SETS.Where(x => x.id == WorkShopSetID).First().name;
             }
             catch (InvalidOperationException e)
             {
                 return "TBA";
             }

             return workshop;
             */
            return type;
        }

        public override string Description()
        {
            return description;
        }

        public override string ID()
        {
            return workshopID.ToString();
        }
    }
}