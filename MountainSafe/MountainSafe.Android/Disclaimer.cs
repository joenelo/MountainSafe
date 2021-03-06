﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Android.Content.PM;

namespace MountainSafe.Droid
{
    [Activity(Label = "Mountain Safe", Icon = "@drawable/Disclaimer", Theme = "@style/MainTheme.Disclaimer", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class Disclaimer : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(Disclaimer).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "Disclaimer.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(3000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting Disclaimer.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}