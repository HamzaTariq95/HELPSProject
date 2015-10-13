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

namespace HELPS
{
    class Session
    {
        public string sessionTitle { get; set; }
        public string sessionStatus { get; set; }
        public string sessionDate { get; set; }
        public string sessionTarget { get; set; }
        public string sessionLocation { get; set; }
        public string sessionTutor { get; set; }
        public string sessionType { get; set; }
        public string sessionDescription { get; set; }

        public Session(string sessionTitle, string sessionStatus, string sessionDate,
            string sessionTarget, string sessionLocation, string sessionTutor,
            string sessionType, string sessionDescription)
        {
            this.sessionTitle = sessionTitle;
            this.sessionStatus = sessionStatus;
            this.sessionDate = sessionDate;
            this.sessionTarget = sessionTarget;
            this.sessionLocation = sessionLocation;
            this.sessionTutor = sessionTutor;
            this.sessionType = sessionType;
            this.sessionDescription = sessionDescription;
        }
    }
}