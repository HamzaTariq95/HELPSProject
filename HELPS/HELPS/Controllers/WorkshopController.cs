using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using Newtonsoft.Json;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Graphics;

using Android.Util;
using System.IO;
using System.Threading.Tasks;
using HELPS.Model;

namespace HELPS.Controllers
{
    public class WorkshopController
    {

        public WorkshopBookingData GetWorkshopBookingData(string studentID)
        {

            // Request Address of the API
            string url = "http://GroupThirteen.cloudapp.net/api/workshop/booking/search?studentId=" + studentID;

            // Get student data using web request
            return getWorkshopBookingData(getRequest(url));
        }

        private HttpWebRequest getRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["AppKey"] = "66666";
            return request;
        }

        private WorkshopBookingData getWorkshopBookingData(HttpWebRequest request)
        {
            WorkshopBookingData workshopBookingData = null;

            // Generating JSON Response and Converting it to Student Object.
            using (WebResponse response = request.GetResponse())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string json = sr.ReadToEnd();

                        // Convert JSON Response to Student Object
                        workshopBookingData = JsonConvert.DeserializeObject<WorkshopBookingData>(json);
                    }

                    try
                    {
                        if (workshopBookingData == null)
                        {
                            Log.Info("HELPS", "No bookings found");
                        }

                        else
                        {
                            //Log.Info("HELPS:assignType", workshopBookingData.attributes.ElementAt(0).description);
                        }
                    }

                    catch (NullReferenceException ex)
                    {
                        Log.Info("HELPS", "Exception: No bookings found");
                        workshopBookingData = null;
                    }
                }
            }
            return workshopBookingData;
        }

        public WorkshopData searchWorkshops(String startDate)
        {

            string url = "http:GroupThirteen.cloudapp.net/api/workshop/search?startingDtBegin=" + startDate + "&startingDtEnd=2060-12-20&active=true";

            //Setting Request Properties
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["AppKey"] = "66666";

            WorkshopData workshops = null;

            //Generating JSON Response and Converting it to Student Object.
            using (WebResponse response = request.GetResponse())
            {
                //Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string json = sr.ReadToEnd();

                        //Convert JSON Response to Student Object
                        workshops = JsonConvert.DeserializeObject<WorkshopData>(json);
                    }

                }
            }

            Log.Info("Bookings Result", workshops.Results[0].WorkshopId.ToString());

            return workshops;
        }
    }
}

