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
    class Employee
    {

        public string studentID { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string gender { get; set; }
        public string degree { get; set; }
        public string status { get; set; }
        public string first_language { get; set; }
        public string country_origin { get; set; }
        public string background { get; set; }
        public Nullable<bool> HSC { get; set; }
        public string HSC_mark { get; set; }
        public Nullable<bool> IELTS { get; set; }
        public string IELTS_mark { get; set; }
        public Nullable<bool> TOEFL { get; set; }
        public string TOEFL_mark { get; set; }
        public Nullable<bool> TAFE { get; set; }
        public string TAFE_mark { get; set; }
        public Nullable<bool> CULT { get; set; }
        public string CULT_mark { get; set; }
        public Nullable<bool> InsearchDEEP { get; set; }
        public string InsearchDEEP_mark { get; set; }
        public Nullable<bool> InsearchDiploma { get; set; }
        public string InsearchDiploma_mark { get; set; }
        public Nullable<bool> foundationcourse { get; set; }
        public string foundationcourse_mark { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<int> creatorID { get; set; }
        public string degree_details { get; set; }
        public string alternative_contact { get; set; }
        public string preferred_name { get; set; }

       




    }
}