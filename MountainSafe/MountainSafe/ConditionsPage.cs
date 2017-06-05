using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using HtmlAgilityPack;
using Xamarin.Forms;

namespace MountainSafe
{

    public class ConditionsPage : ContentPage
    {

        // API key.
        string wunderground_key = "7934ba729ae82e01";
        // Create table view for the informtation for whatever is selected in the picker.
        StackLayout mountainInfoLayout;
        TableView mountainInfo;
         Label avyInfo;
        StackLayout avyInfoLayout;

        // Manually include each mountain coordinates for the picker to be used to call the API.
        Dictionary<string, coordinates> location = new Dictionary<string, coordinates>
            {
               // { "Elk River Trail", new coordinates{ } },
                { "Colonel Foster", new coordinates{Latitude = "49.74972", Longitude = "-125.86750" } },
                { "Elkhorn", new coordinates{Latitude = "49.78999684", Longitude = "-125.82833002" } },
                { "Kings Peak", new coordinates{Latitude = "49.81139", Longitude = "-125.83639" } },
                { "Rambler", new coordinates{Latitude = "49.73333", Longitude = "-125.74667" } },
              
               // { "Alberni Ranges", new coordinates{ } },
                { "Arrowsmith", new coordinates{Latitude = "49.22361", Longitude = "-124.59444" } },
                { "Nine Peaks", new coordinates{Latitude = "49.43194", Longitude = "-125.54917" } },
                { "Triple Peak", new coordinates{Latitude = "49.15750", Longitude = "-125.30222" } },
                { "The Red Pillar", new coordinates{Latitude = "49.52556", Longitude = "-125.38917" } },

                // { "South Strathcona Range", new coordinates{ } },
                { "Golden Hinde", new coordinates{Latitude = "49.66222", Longitude = "-125.74694" } },
                { "Mt Harmston", new coordinates{Latitude = "49.54889", Longitude = "-125.39833" } },
                { "Mt Septimus", new coordinates{Latitude = "49.48056", Longitude = "-125.51361" } },
                
               // { "Sayward Ranges", new coordinates{ } },
                { "Victoria Peak", new coordinates{Latitude = "50.05472", Longitude = "-126.10083" } },
                { "Rugged Mountain", new coordinates{Latitude = "50.02528", Longitude = "-126.67778" } },

            };

        public ConditionsPage()
        {
            Title = "Current Conditions";

            // Create Picker.
            Picker picker = new Picker
            {
                Title = "Select Your Mountain",
                VerticalOptions = LayoutOptions.CenterAndExpand

            };
            picker.BackgroundColor = Color.FromHex("#FAFAFA");
            

            // Add Picker to Stacklayout and removed spacing, padding, and margin.
            StackLayout pickerLayout = new StackLayout
            {
                Padding = 0,
                Margin = 0,
                Spacing = 0,
                // Add each section of the page.
                Children =
                {
                    picker,
                }
            };

            mountainInfo = new TableView
            {
                Intent = TableIntent.Data,
                IsVisible = false,
                HeightRequest = 1750
        };
            mountainInfo.BackgroundColor = Color.FromHex("#78909C");
            mountainInfoLayout = new StackLayout  {
                // Create a Table for the Forecast Information to sit in and make it invisible until a mountain is selected.
               Children = {
                    mountainInfo
                }
            };

            avyInfo = new Label { Margin = new Thickness(10, 0, 10, 0) };
            avyInfoLayout = new StackLayout
            {
                IsVisible = false,
                BackgroundColor = Color.FromHex("#ECEFF1"),
                Children = {
                    avyInfo
                }
            };
      

            // Add the Cooridnates to each mountain.
            foreach (string mountain in location.Keys)
            {
                picker.Items.Add(mountain);
            }

            // Create BoxView for displaying picked mountains
            picker.SelectedIndexChanged += (sender, args) =>
            {
                avyInfoLayout.IsVisible = true;
                string mountainSelected = picker.Items[picker.SelectedIndex];
                coordinates latLong = location[mountainSelected];
                string latitudeLongitude = latLong.Latitude + "," + latLong.Longitude;

                // API URL and KEY + Locations .
                parseConditions("http://api.wunderground.com/api/" + wunderground_key + "/geolookup/conditions/q/" + latitudeLongitude + ".json", mountainInfo);
                parseAvalanche("https://api.rss2json.com/v1/api.json?rss_url=http%3A%2F%2Ffeeds.feedburner.com%2FVancouverIslandAvalancheBulletin%3Fformat%3Dxml", avyInfo);
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Padding = 0,
                Margin = 0,
                Spacing = 0,
                // Add each section of the page
                Children =
                {
                    picker,
                    mountainInfo,
                    new ScrollView
                    {
                        Content = avyInfoLayout
                    }
                }
            };
        }

        private static void parseConditions(string url, TableView mountainInfo)
        {
            // Do the request/response
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            dynamic stuff = JsonConvert.DeserializeObject(response.Content);


            //Pull the data from the JSON file and put them into a Variable to pass into the Array.
            string temperature = stuff.current_observation.temperature_string;
            string feels_like = stuff.current_observation.feelslike_string;
            string weather1 = stuff.current_observation.weather;
            string windString = stuff.current_observation.wind_string;
            string humidity = stuff.current_observation.relative_humidity;
            string pressure_mb = stuff.current_observation.pressure_mb;
            string dewpoint_string = stuff.current_observation.dewpoint_string;
            string visibility_km = stuff.current_observation.visibility_km;
            string latitude = stuff.current_observation.display_location.latitude;
            string longitude = stuff.current_observation.display_location.longitude;
            string obs_time = stuff.current_observation.observation_time;


            //Create a Title to be used and passed into the array in front of each Weather Variable
            string conditions = "Conditions";
            string currentTemp = "Current temperature";
            string feelsLike = "Feels like";
            string weather = "Weather";
            string wind = "Wind";
            string humid = "Humidity";
            string pressure = "Pressure";
            string dewpoint = "Dew Point";
            string visibility = "Visibility";
            string latd = "Latitude";
            string longd = "Longitude";
            string time = "Time data was taken";


            // Create an array to include both the title and the forecast of each Period.
            string[] itemsArray = new string[] { conditions +": ",
                                                 currentTemp + ": " + temperature,
                                                 feelsLike + ": " + feels_like,
                                                 weather + ": " + weather1,
                                                 wind + ": " + windString,
                                                 humid + ": " + humidity,
                                                 pressure + ": " + pressure_mb + "mb",
                                                 dewpoint + ": " + dewpoint_string,
                                                 visibility + ": " + visibility_km + "km",
                                                 latd + ": " + latitude,
                                                 longd + ": " + longitude,
                                                 time + ": " + obs_time,
                                                };

            // Make data Visible.
            mountainInfo.IsVisible = true;
            // Clear space on the page for the table.
            mountainInfo.Root.Clear();
            // Loop through each item in the array and put into individual cells.
            foreach (var item in itemsArray)
            {
                mountainInfo.Root.Add(
                     new TableSection
                     {

                        new TextCell
                        {
                            Detail = item,
                            DetailColor = Color.FromHex ("#ECEFF1")
                        }
                     }
                     );
            }
        }
        private static void parseAvalanche(string url, Label avyInfo)
        {
            // Do the request/response
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            dynamic stuff = JsonConvert.DeserializeObject(response.Content);

            //Pull the data from the JSON file and put them into a Variable to pass into the Array. 
            string content = stuff.items[0].content;


            //Allow the App to read the income HTML File and parse it without showing the HTML tags, load it into the content
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            //string name = htmlDoc.DocumentNode
            //    .SelectNodes("//table/tr/th")
            //    .First()
            //    .Attributes["value"].Value;
            
            

            
            avyInfo.LineBreakMode = LineBreakMode.CharacterWrap;
            avyInfo.Text = htmlDoc.DocumentNode.InnerText;
            avyInfo.TextColor = Color.FromHex("#263238");
            avyInfo.HeightRequest = 1750;
 
        }
    }
}
