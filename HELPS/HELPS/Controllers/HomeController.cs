using System;
using System.Collections.Generic;
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
using Android.Content.PM;
using Android.Graphics;
using System.Net;
using Android.Util;
using System.IO;
using System.Threading.Tasks;

namespace HELPS
{
    public class HomeController {

        public bool login(String username, String password)
        {

            Log.Info("Test"  , "Mai andar hu");

            if(username == null)
            {
                return false;
            }

            String url = "http://GroupThirteen.cloudapp.net/api/student/" + username;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";

            //request.Headers.Add("Key: AppKey");
            //request.Headers.Add("value: 66666");

            request.Headers["AppKey"] = "66666";

           // WebResponse response = request.GetResponse();


            using (WebResponse response = request.GetResponse())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    int numBytesRead = 0;
                    byte[] bytes = new byte[stream.Length + 10];
                    int numBytesToRead = (int)stream.Length;
                    int n = stream.Read(bytes, numBytesRead, 10);
                   
                    do
                    {
                        // Read may return anything from 0 to 10.

                        numBytesRead += n;
                        numBytesToRead -= n;
                    } while (numBytesToRead > 0);
                    stream.Close();

                    //Log.Info("TEST", numBytesRead);

                   
                    // Use this stream to build a JSON document object:
                    //JsonValue jsonDoc = Task.Run(() => JsonObject.Load(stream));
                 //   Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                   // return jsonDoc;
                }
            }

            Log.Info("TEST" , response.GetResponseStream);



            return true;



        /*    if(username.Equals("hamza")  && password.Equals("password"))
            {

                return true;
            }

            else
            {
                return false;   
             
            } */

           


        }

    }

        
}