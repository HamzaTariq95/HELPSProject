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

namespace HELPS
{
    public class HomeController {

        public  StudentData login(string studentID)
        {       

            // Request Address of the API
            String url = "http://GroupThirteen.cloudapp.net/api/student/" + studentID;


            // Setting Request Properties
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["AppKey"] = "66666";

            StudentData studentData = null;
            

            // Generating JSON Response and Converting it to Student Object.
            using (WebResponse response = request.GetResponse())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        String json = sr.ReadToEnd();
                    
                              // Convert JSON Response to Student Object
                             studentData = JsonConvert.DeserializeObject<StudentData>(json);                 
                    }

                    try
                    {
                        if (studentData == null)
                        {
                            Log.Info("HELPS", "This student does not exist");
                        }

                        else
                        {
                            Log.Info("HELPS", studentData.attributes.studentID);
                        }
                    }

                catch (NullReferenceException ex)
                    {
                        Log.Info("HELPS", "Exception: This student does not exist");
                        studentData = null;
                    }
                }
            }

            return studentData;
        }
    }
}

