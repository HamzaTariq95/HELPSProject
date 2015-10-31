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
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;



namespace HELPS.Controllers
{
    class RegisterController
    {

      
       
        public async void Register(UtsData data)
        {
            string json = JsonConvert.SerializeObject(data);
   
            Log.Info("Register", json);

           // Request Address of the API    
            string url = Server.url + "api/student/register";

           String result = null;
        
               using (WebClient wc = new WebClient())
               {
                
                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                wc.Headers.Add("AppKey", "66666");


                result = await wc.UploadStringTaskAsync(new Uri(url), json);
                   

                Log.Info("Register SUCCESS", result);
                HomeController loginController = new HomeController();
                await loginController.login(data.StudentId);

            }

              

        }
    }

}