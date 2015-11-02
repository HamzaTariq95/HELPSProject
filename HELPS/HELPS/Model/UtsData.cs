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
        
        public string Faculty { get; set; }

        
        public string Course { get; set; }

        
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string StudentId { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Degree { get; set; }

        public string Status { get; set; }

        public string FirstLanguage { get; set; }

        public string CountryOrigin { get; set; }

        public string Background { get; set; }

        public String DegreeDetails { get; set; }

        public string AltContact { get; set; }

        public string PreferredName { get; set; }

        public String HSC { get; set; }

        public String HSCMark { get; set; }

        public object IELTS { get; set; }

        public object IELTSMark { get; set; }

        public object TOEFL { get; set; }

        public object TOEFLMark { get; set; }

        public object TAFE { get; set; }

        public object TAFEMark { get; set; }

        public object CULT { get; set; }

        public object CULTMark { get; set; }

        public object InsearchDEEP { get; set; }

        public object InsearchDEEPMark { get; set; }

        public object InsearchDiploma { get; set; }

        public object InsearchDiplomaMark { get; set; }

        public object FoundationCourse { get; set; }

        public object FoundationCourseMark { get; set; }

        public object CreatorId { get; set; }

       
     }
}