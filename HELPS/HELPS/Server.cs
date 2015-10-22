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
using HELPS.Model;

namespace HELPS
{
    public static class Server
    {
        public static string url = "http://groupthirteen.cloudapp.net/";
        public static bool workshopBookingsAltered = false;
        public static bool sessionBookingsAltered = false;
        public static List<WorkshopBooking> futureBookings = new List<WorkshopBooking>();
    }
}