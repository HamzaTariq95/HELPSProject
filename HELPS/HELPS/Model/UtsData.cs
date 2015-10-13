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

namespace HELPS.Model
{   
    /*
     * This model class corresponds to our Local datasource (utsData.csv) which we use for login.
     *
     */
    class UtsData
    {
        [JsonIgnore]
        public string Passsword { get; set; }
        public string StudentID { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Degree { get; set; }
        public string Status { get; set; }
        public string FirstLanguage { get; set; }
        public string CountryOrigin { get; set; }
        public string PreferredName { get; set; }
        public string AltContact { get; set; }
        public string CreatorId { get; set; }
        public string Faculty { get; set; }
        public string Course {get; set;}
        public string Email { get; set; }
     }
}