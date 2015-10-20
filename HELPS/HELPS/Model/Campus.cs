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
    public class Campus
    {
        public int id { get; set; }
        public string campus { get; set; }
        public DateTime? archived
        {
            get;
        }
    }
}