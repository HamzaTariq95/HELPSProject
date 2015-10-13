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
    class SessionBooking : Booking
    {

        public string LecturerFirstName { get; set; }
        public string LecturerLastName { get; set; }
        public string LecturerEmail { get; set; }
        public string SessionTypeAbb { get; set; }
        public string SessionType { get; set; }
        public string AssignmentType { get; set; }
        public string AppointmentType { get; set; }
        public int BookingId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Campus { get; set; }
        public bool Cancel { get; set; }
        public string Assistance { get; set; }
        public string Reason { get; set; }
        public int? Attended { get; set; }
        public int? WaitingID { get; set; }
        public int? IsGroup { get; set; }
        public string NumPeople { get; set; }
        public string LecturerComment { get; set; }
        public string LearningIssues { get; set; }
        public int? IsLocked { get; set; }
        public string AssignTypeOther { get; set; }
        public string Subject { get; set; }
        public string AppointmentsOther { get; set; }
        public string AssistanceText { get; set; }
        public int SessionId { get; set; }
        public DateTime? archived { get; set; }

       public SessionBooking(bool sessionStatus, DateTime sessionDate,
            string sessionLocation, string sessionTutor,
            string sessionType)
        {
            this.Cancel = sessionStatus;
            this.StartDate = sessionDate;
            this.Campus = sessionLocation;
            this.LecturerFirstName = sessionTutor;
            this.SessionTypeAbb = sessionType;
        }

        public override string Title()
        {
            return "Session";
        }

        public override string Status()
        {
            if (!Cancel)
                return "Booked";

            return "Cancelled";
        }

        public override DateTime? Date()
        {
            return StartDate;
        }

        public override string Location()
        {
            return Campus;
        }

        public override string Tutor()
        {
            return LecturerFirstName + " " + LecturerLastName;
        }

        public override string Type()
        {
            return SessionTypeAbb;
        }
    }
}