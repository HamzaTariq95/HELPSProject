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
using HELPS.Model;
using Newtonsoft.Json;
using Android.Util;

namespace HELPS.Controllers
{
    class RegisterController
    {
        public void Register(UtsData data)
        {
           
       string json = JsonConvert.SerializeObject(data);
       Log.Info("Register" , json);

        }	


        }
 }
