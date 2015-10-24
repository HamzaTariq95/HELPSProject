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
using HELPS.Views.Activities;

namespace HELPS.Views
{

    public class SearchWorkshopsFragment : Fragment, AdapterView.IOnItemClickListener
    {
        private WorkshopData workshopData;
        private List<Workshop> workshops;
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
            FetchAvailableWorkshops();
            SelectUnBookedWorkshops();
            DisplayWorkshops(view);

            return view;
        }

        private void DisplayWorkshops(View view)
        {

            ListView availableList = view.FindViewById<ListView>(Resource.Id.listAvailable);
            availableList.OnItemClickListener = this;

            //Sort bookings by date
            // workshops.Sort((a, b) => a.Date().ToString().CompareTo(b.Date().ToString()));
            workshops.Sort((a, b) => DateTime.Compare(a.Date() ?? DateTime.MaxValue, b.Date() ?? DateTime.MaxValue));

            availableList.Adapter = new SearchWorkshopsBaseAdapter(Activity, workshops);
        }

        private void SelectUnBookedWorkshops()
        {
            List<string> bookedWorkshopIDs = Server.currentWorkshopBookingData.attributes.Select(y => y.BookingArchived != null ? "" : y.workshopID.ToString()).ToList();
            workshops = workshops.Where(w => !bookedWorkshopIDs.Contains(w.WorkshopId.ToString())).ToList();
        }

        private void FetchAvailableWorkshops()
        {
            // {Architecture} inflate list with available workshops
            workshops = new List<Workshop>();
            addWorkshopToList(workshopData, workshops);
        }

        private void addWorkshopToList(WorkshopData workshopData, List<Workshop> workshops)
        {
            foreach (Workshop workshop in workshopData.Results)
            {
                if (workshop.archived == null)
                    workshops.Add(workshop);
            }
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Workshop workshop = workshops[position];

            string workshopString = JsonConvert.SerializeObject(workshop);

            Intent intent = new Intent(Application.Context, typeof(BookingDetailActivity));
            intent.PutExtra("requestType", "showAvailableWorkshop");
            intent.PutExtra("workshop", workshopString);
            StartActivity(intent);
        }
    }
}