using System;
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

namespace HELPS.Controllers
{
    class RegisterController
    {
       
        public void Register(UtsData data)
        {

            string json = JsonConvert.SerializeObject(data);
            Log.Info("Register", json);


         /*   // Request Address of the API
            String url = "http://GroupThirteen.cloudapp.net/api/student/resigter";

            // Setting Request Properties
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["AppKey"] = "66666";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {oo655
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Log.Info("REGISTER", result);
            }

          * */

        }
    }

}