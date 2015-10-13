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
using HELPS.Model;

namespace HELPS.Model
{
    public class Student
    {
        public string studentID { get; set; }
        public object dob { get; set; }
        public string gender { get; set; }
        public string degree { get; set; }
        public string status { get; set; }
        public string first_language { get; set; }
        public string country_origin { get; set; }
        public object background { get; set; }
        public object HSC { get; set; }
        public object HSC_mark { get; set; }
        public object IELTS { get; set; }
        public object IELTS_mark { get; set; }
        public object TOEFL { get; set; }
        public object TOEFL_mark { get; set; }
        public object TAFE { get; set; }
        public object TAFE_mark { get; set; }
        public object CULT { get; set; }
        public object CULT_mark { get; set; }
        public object InsearchDEEP { get; set; }
        public object InsearchDEEP_mark { get; set; }
        public object InsearchDiploma { get; set; }
        public object InsearchDiploma_mark { get; set; }
        public object foundationcourse { get; set; }
        public object foundationcourse_mark { get; set; }
        public string created { get; set; }
        public int creatorID { get; set; }
        public object degree_details { get; set; }
        public object alternative_contact { get; set; }
        public object preferred_name { get; set; }
    }
}
