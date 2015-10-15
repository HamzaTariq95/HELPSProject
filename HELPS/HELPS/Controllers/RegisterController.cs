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

      
       
        public void Register(UtsData data)
        {

            string json = JsonConvert.SerializeObject(data);
            Log.Info("Register", json);

           // Request Address of the API    
           String url = "http://GroupThirteen.cloudapp.net/api/student/resigter";

         
          // client.Headers[HttpRequestHeader.ContentType] = "application/json";
          /* String result = null;
        
               using (WebClient wc = new WebClient())
               {
                   wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                   wc.Headers.Add("AppKey", "66666");


                    result = wc.UploadString(url, json);
                   

                   Log.Info("Register SUCCESS", result);

               } */
          
          
         
          
             
            // Setting Request Properties
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = " application/json; charset=utf-8";
            request.Headers["AppKey"] = "66666";

           Log.Info("API CALL TEST", "I am going to call service");
           using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

           Log.Info("TEST POST", "Outside Web Response");

           try
           {
               var httpResponse = (HttpWebResponse)request.GetResponse();

               using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
               {
                   var result = streamReader.ReadToEnd();
                   Log.Info("result", result);
               }
           }
            catch (Exception e)
           {
               Log.Info("TEST POST", e.ToString());
           }
          

          

          

         /*   using (WebResponse response = await request.GetResponseAsync())
            {
                Log.Info("TEST POST", "Inside Web Response");
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                    Log.Info("TEST POST" , jsonDoc.ToString());
                }  

            } */

           
        
        }
    }

}