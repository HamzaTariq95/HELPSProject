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
    class Employee
    {

        [JsonProperty("studentID")]
        public string studentID { get; set; }

        [JsonProperty("dob")]
        public Nullable<System.DateTime> dob { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

         [JsonProperty("degree")]
        public string degree { get; set; }

         [JsonProperty("status")]
        public string status { get; set; }

         [JsonProperty("first_language")]
        public string first_language { get; set; }

        [JsonProperty("country_origin")]
        public string country_origin { get; set; }

        [JsonProperty("background")]
        public string background { get; set; }

         [JsonProperty("HSC")]
        public Nullable<bool> HSC { get; set; }

         [JsonProperty("HSC_mark")]
        public string HSC_mark { get; set; }

         [JsonProperty("IELTS")]
        public Nullable<bool> IELTS { get; set; }

         [JsonProperty("IELTS_mark")]
        public string IELTS_mark { get; set; }

         [JsonProperty("TOEFL")]
        public Nullable<bool> TOEFL { get; set; }

         [JsonProperty("TOEFL_mark")]
        public string TOEFL_mark { get; set; }

         [JsonProperty("TAFE")]
        public Nullable<bool> TAFE { get; set; }

         [JsonProperty("TAFE_mark")]
        public string TAFE_mark { get; set; }

         [JsonProperty("CULT")]
        public Nullable<bool> CULT { get; set; }

         [JsonProperty("CULT_mark")]
        public string CULT_mark { get; set; }

         [JsonProperty("InsearchDEEP")]
        public Nullable<bool> InsearchDEEP { get; set; }

         [JsonProperty("InsearchDEEP_mark")]
        public string InsearchDEEP_mark { get; set; }

         [JsonProperty("InserachDiploma")]
        public Nullable<bool> InsearchDiploma { get; set; }

         [JsonProperty("InsearchDiploma_mark")]
        public string InsearchDiploma_mark { get; set; }

         [JsonProperty("foundationcourse")]
        public Nullable<bool> foundationcourse { get; set; }

         [JsonProperty("foundationcourse_mark")]
        public string foundationcourse_mark { get; set; }

         [JsonProperty("created")]
        public Nullable<System.DateTime> created { get; set; }

         [JsonProperty("creatorID")]
        public Nullable<int> creatorID { get; set; }

         [JsonProperty("degree_details")]
        public string degree_details { get; set; }

         [JsonProperty("alternative_contact")]
        public string alternative_contact { get; set; }

        [JsonProperty("preferred_name")]
        public string preferred_name { get; set; }

       




    }
}