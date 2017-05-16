using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.IO;
using Xamarin.Forms;

namespace MountainSafe
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            Main();
            MainPage = new MountainSafe.MainPage();
		}

        static void Main()
        {
            //Start program
            // API key
            string wunderground_key = "7934ba729ae82e01";

            // create strings to pass into the GeoLookUp API call
            string elkhorn = "49.78999684,-125.82833002";
           // string arrowsmith = "49.22361,-124.59444";
           // string colonelFoster = "49.74972,-125.86750";
           // string victoriaPeak = "50.05472,-126.10083";
           // string triplePeak = "49.15750,-125.30222";
            

            parse("http://api.wunderground.com/api/" + wunderground_key + "/geolookup/conditions/q/"+elkhorn+".xml");
            //parse("http://api.wunderground.com/api/" + wunderground_key + "/geolookup/conditions/q/"+arrowsmith+".xml");
            //parse("http://api.wunderground.com/api/" + wunderground_key + "/geolookup/conditions/q/"+colonelFoster+".xml");
            //parse("http://api.wunderground.com/api/" + wunderground_key + "/geolookup/conditions/q/"+victoriaPeak+".xml");
            //parse("http://api.wunderground.com/api/" + wunderground_key + "/geolookup/conditions/q/"+triplePeak+".xml");

        }

        //Takes a url request to wunderground, parses it, and displays the data.
        private static void parse(string input_xml)
        {
            //Variables
            string place = "";
            string obs_time = "";
            string weather1 = "";
            string temperature_string = "";
            string relative_humidity = "";
            string wind_string = "";
            string pressure_mb = "";
            string dewpoint_string = "";
            string visibility_km = "";
            string latitude = "";
            string longitude = "";

            

            var cli = new WebClient();
            string weather = cli.DownloadString(input_xml);

            using (XmlReader reader = XmlReader.Create(new StringReader(weather)))
            {
                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.Equals("full"))
                            {
                                reader.Read();
                                place = reader.Value;
                            }
                            else if (reader.Name.Equals("observation_time"))
                            {
                                reader.Read();
                                obs_time = reader.Value;
                            }
                            else if (reader.Name.Equals("weather"))
                            {
                                reader.Read();
                                weather1 = reader.Value;
                            }
                            else if (reader.Name.Equals("temperature_string"))
                            {
                                reader.Read();
                                temperature_string = reader.Value;
                            }
                            else if (reader.Name.Equals("relative_humidity"))
                            {
                                reader.Read();
                                relative_humidity = reader.Value;
                            }
                            else if (reader.Name.Equals("wind_string"))
                            {
                                reader.Read();
                                wind_string = reader.Value;
                            }
                            else if (reader.Name.Equals("pressure_mb"))
                            {
                                reader.Read();
                                pressure_mb = reader.Value;
                            }
                            else if (reader.Name.Equals("dewpoint_string"))
                            {
                                reader.Read();
                                dewpoint_string = reader.Value;
                            }
                            else if (reader.Name.Equals("visibility_km"))
                            {
                                reader.Read();
                                visibility_km = reader.Value;
                            }
                            else if (reader.Name.Equals("latitude"))
                            {
                                reader.Read();
                                latitude = reader.Value;
                            }
                            else if (reader.Name.Equals("longitude"))
                            {
                                reader.Read();
                                longitude = reader.Value;
                            }
                            

                            break;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("********************");
            System.Diagnostics.Debug.WriteLine("Place:             " + place);
            System.Diagnostics.Debug.WriteLine("Observation Time:  " + obs_time);
            System.Diagnostics.Debug.WriteLine("Weather:           " + weather1);
            System.Diagnostics.Debug.WriteLine("Temperature:       " + temperature_string);
            System.Diagnostics.Debug.WriteLine("Relative Humidity: " + relative_humidity);
            System.Diagnostics.Debug.WriteLine("Wind:              " + wind_string);
            System.Diagnostics.Debug.WriteLine("Pressure (mb):     " + pressure_mb);
            System.Diagnostics.Debug.WriteLine("Dewpoint:          " + dewpoint_string);
            System.Diagnostics.Debug.WriteLine("Visibility (km):   " + visibility_km);
            System.Diagnostics.Debug.WriteLine("Location:          " + longitude + ", " + latitude);
        }

        // Start RSS Read from Avalanche Data
            private string ParseRssFile()
        {
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load("http://www.islandavalanchebulletin.com/");

            // Parse the Items in the RSS file
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            StringBuilder rssContent = new StringBuilder();

            // Iterate through the items in the RSS file
            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";

                rssContent.Append("<a href='" + link + "'>" + title + "</a><br>" + description);
            }

            // Return the string that contain the RSS items
            return rssContent.ToString();

        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
