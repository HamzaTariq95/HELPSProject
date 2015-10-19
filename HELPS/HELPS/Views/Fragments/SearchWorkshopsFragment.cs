using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using HELPS.Model;
using Newtonsoft.Json;
using HELPS.Controllers;

namespace HELPS.Views
{

    public class SearchWorkshopsFragment : Fragment
    {
        private WorkshopData workshopData;

        public SearchWorkshopsFragment(WorkshopData workshopData)
        {
            this.workshopData = workshopData;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.SearchLayout, container, false);

            //Get student data from intent in parent activity
            //studentData = JsonConvert.DeserializeObject<StudentData>(this.Activity.Intent.GetStringExtra("student"));

            // Set the "Upcoming Sessions" list view to display (upto) the four closest sessions
            DisplayAvailableWorkshops(view);

            return view;
        }

        private void DisplayAvailableWorkshops(View view)
        {
            // {Architecture} inflate list with available workshops
            List<Workshop> workshops = new List<Workshop>();
            addWorkshopToList(workshopData, workshops);

            ListView availableList = view.FindViewById<ListView>(Resource.Id.listAvailable);
            availableList.Adapter = new SearchWorkshopsBaseAdapter(Activity, workshops);
        }

        private void addWorkshopToList(WorkshopData workshopData, List<Workshop> workshops)
        {
            foreach (Workshop workshop in WorkshopData.Results)
            {
                if (cutoff == null && archived == null)
                    workshops.add(workshop);
            }
        }
    }
}