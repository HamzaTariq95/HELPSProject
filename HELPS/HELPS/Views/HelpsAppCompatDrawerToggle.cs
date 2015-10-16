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

using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace HELPS.Views
{
    class HelpsAppCompatDrawerToggle : SupportActionBarDrawerToggle
    {
        private AppCompatActivity _HostActivity;
        private int _OpenedResource;
        private int _ClosedResource;

        public HelpsAppCompatDrawerToggle(AppCompatActivity host, DrawerLayout drawerLayout, int openedResource, int closedResource) 
            : base(host, drawerLayout, openedResource, closedResource)
        {
            _HostActivity = host;
            _OpenedResource = openedResource;
            _ClosedResource = closedResource;
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            _HostActivity.SupportActionBar.SetTitle(_OpenedResource);
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            _HostActivity.SupportActionBar.SetTitle(_ClosedResource);
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }

        public void SetClosedResource(int resource)
        {
            _ClosedResource = resource;
        }
    }
}