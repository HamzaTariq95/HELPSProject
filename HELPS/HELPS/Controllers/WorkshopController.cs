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
using System.Net;

namespace HELPS.Controllers
{
    class WorkshopController
    {

        public void viewSessions()
        {
            String url = "http://GroupThirteen.cloudapp.net/api/session/booking/search";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Key: AppKey");
            request.Headers.Add("value: 66666");

            
            

           

                

        }


    }
}