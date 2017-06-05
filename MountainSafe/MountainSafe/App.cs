using Android.App;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MountainSafe
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {

            MainPage = new MountainSafe.MainPage();

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}

