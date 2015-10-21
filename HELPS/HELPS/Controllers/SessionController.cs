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
    public class SessionController
    {

        public SessionBookingData GetSessionBookingData(string studentID)
        {
            // Request Address of the API
            //string url = "http://GroupThirteen.cloudapp.net/api/session/booking/search?studentId=" + studentID;
            string url = Server.url + "api/session/booking/search?studentId=" + studentID;

            // Get student data using web request
            return getSessionBookingData(getRequest(url));
        }

        private HttpWebRequest getRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["AppKey"] = "66666";
            return request;
        }

        private SessionBookingData getSessionBookingData(HttpWebRequest request)
        {
            SessionBookingData sessionBookingData = null;

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
                        sessionBookingData = JsonConvert.DeserializeObject<SessionBookingData>(json);
                    }

                    try
                    {
                        if (sessionBookingData == null)
                        {
                            Log.Info("HELPS", "No bookings found");
                        }

                        else
                        {
                            Log.Info("HELPS:assignType", sessionBookingData.attributes.ToString());
                        }
                    }

                    catch (NullReferenceException ex)
                    {
                        Log.Info("HELPS", "Exception: No bookings found");
                        sessionBookingData = null;
                    }
                }
            }
            return sessionBookingData;
        }
    }
}

