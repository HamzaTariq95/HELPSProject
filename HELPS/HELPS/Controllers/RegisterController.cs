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
using System.Net.Http;



namespace HELPS.Controllers
{
    class RegisterController
    {
        public void Register(UtsData data)
        {
            string json = JsonConvert.SerializeObject(data);
   
            Log.Info("Register", json);

           // Request Address of the API    
            string url = "http://groupthirteen.cloudapp.net/api/student/register";
            
            string resJSON;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("AppKey", "66666");
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
                resJSON = client.UploadString(url, "POST", json);
            }
            Console.WriteLine("done");
        }
    }

}