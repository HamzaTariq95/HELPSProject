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
using Newtonsoft.Json;

namespace HELPS.Model.JSONDataClasses
{
    public class CampusData
    {
        [JsonProperty("Results")]
        public List<Campus> attributes { get; set; }


        public bool IsSuccess { get; set; }


        public object DisplayMessage { get; set; }



        public int DescribeContents()
        {
            return 0;
        }
    }
}