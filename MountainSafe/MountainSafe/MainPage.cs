using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Xamarin.Forms;

namespace MountainSafe

{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Title = "Mountain Safe";

            Children.Add(new ConditionsPage());
            Children.Add(new ForecastPage());

        }
    }
}
