using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using Newtonsoft.Json;



using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Graphics;
using System.Net;
using Android.Util;
using System.IO;
using System.Threading.Tasks;
using HELPS.Model;

namespace HELPS
{
    public class HomeController {

        public bool login(String username, String password)
        {

            if(username == null)
            {
                return false;
            }

            String url = "http://GroupThirteen.cloudapp.net/api/student/" + username;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["AppKey"] = "66666";



            using (WebResponse response = request.GetResponse())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        String json = sr.ReadToEnd();

                        Students emp = null;
                        try
                        {     
                  
                            emp = JsonConvert.DeserializeObject<Students>(json);
                             Log.Info("TEST", emp.studentID);
                        }
                        catch(Exception e)
                        {

                            Log.Info("ERROR", e.ToString());
                        }
               
                        Log.Info("TEST", json);
                      //  Log.Info("OBJECT ID", emp.studentID);                  
                       
                    }
              
                }
            }


            return true;
        }

    }

        
}

public class DataJsonAttributeContainer
{
    [JsonProperty("My Book List")]
    public List<Students> attributes { get; set; }
}