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
    public abstract class Booking
    {
        public abstract string Title();
        public abstract string Status();
        public abstract DateTime? Date();
        public abstract string Location();
        public abstract string Tutor();
        public abstract string Type(); 
    }
}