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

namespace HELPS.Views
{
    [Activity(Label = "FutureActivity")]
    public class FutureActivity : AppCompatActivity
    {
        private SupportToolbar _Toolbar;
        private HelpsAppCompatDrawerToggle _DrawerToggle;
        private DrawerLayout _DrawerLayout;
        private ArrayAdapter _MenuAdapter;
        private ListView _Menu;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource.
            SetContentView(Resource.Layout.Main);
            _DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerMain);

            // Set the toolbar
            _Toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            _DrawerToggle = new HelpsAppCompatDrawerToggle(this, _DrawerLayout, Resource.String.menuTitle, Resource.String.applicationName);

            _DrawerLayout.SetDrawerListener(_DrawerToggle);

            SetSupportActionBar(_Toolbar);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _DrawerToggle.SyncState();

            // Not the first time activity has been run.
            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.menuTitle);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.applicationName);
                }
            }

            // Set up the menu layout.
            _Menu = FindViewById<ListView>(Resource.Id.listMenu);

            _MenuAdapter = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.menu, Resource.Layout.MenuRow);
            _Menu.Adapter = _MenuAdapter;

            // Set up ability to click menu items
            _Menu.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                switch (e.Position)
                {
                    // Profile/Landing page.
                    case 0:
                        break;
                    // Search sessions.
                    case 1:
                    // Future bookings.
                    case 2:
                    // Past bookings.
                    case 3:
                    // Record notes.
                    case 4:
                    // Settings.
                    case 5:
                        Console.WriteLine("Not implemented");
                        break;
                    // Log out.
                    case 6:
                        // {Architecture} Log Out function
                        StartActivity(typeof(LogOnActivity));
                        Finish();
                        break;
                }
            };
        }
    }
}