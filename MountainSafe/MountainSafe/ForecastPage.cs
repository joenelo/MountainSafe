using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MountainSafe
{
     public class ForecastPage : ContentPage
    {
        
        StackLayout ForecastLayout;

        // API key.
        string wunderground_key = "7934ba729ae82e01";


        // Manually include each mountain coordinates for the picker to be used to call the API.
        Dictionary<string, coordinates> location = new Dictionary<string, coordinates>
            {
            
                //{ "Elk River Trail", new coordinates{ } },
                { "Colonel Foster", new coordinates{Latitude = "49.74972", Longitude = "-125.86750" } },
                { "Elkhorn", new coordinates{Latitude = "49.78999684", Longitude = "-125.82833002" } },
                { "Kings Peak", new coordinates{Latitude = "49.81139", Longitude = "-125.83639" } },
                { "Rambler", new coordinates{Latitude = "49.73333", Longitude = "-125.74667" } },
              
               // { "Alberni Ranges", new coordinates{  } },
                { "Arrowsmith", new coordinates{Latitude = "49.22361", Longitude = "-124.59444" } },
                { "Nine Peaks", new coordinates{Latitude = "49.43194", Longitude = "-125.54917" } },
                { "Triple Peak", new coordinates{Latitude = "49.15750", Longitude = "-125.30222" } },
                { "The Red Pillar", new coordinates{Latitude = "49.52556", Longitude = "-125.38917" } },

               // { "South Strathcona Range", new coordinates{   } },
                { "Golden Hinde", new coordinates{Latitude = "49.66222", Longitude = "-125.74694" } },
                { "Mt Harmston", new coordinates{Latitude = "49.54889", Longitude = "-125.39833" } },
                { "Mt Septimus", new coordinates{Latitude = "49.48056", Longitude = "-125.51361" } },
                
                //{ "Sayward Ranges", new coordinates{    } },
                { "Victoria Peak", new coordinates{Latitude = "50.05472", Longitude = "-126.10083" } },
                { "Rugged Mountain", new coordinates{Latitude = "50.02528", Longitude = "-126.67778" } },


            };

        public ForecastPage()
        {
            // Make ForecastPage in a StackLayout and give it colour
            ForecastLayout = new StackLayout {
                BackgroundColor = Color.FromHex("#B0BEC5")
            };

            Title = "3 Day Forcast";

            // Create Picker.
            Picker picker = new Picker
            {
                Title = "Select Your Mountain",
                VerticalOptions = LayoutOptions.CenterAndExpand,
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


            // Add the Cooridnates to each mountain.
            foreach (string mountain in location.Keys)
            {
                picker.Items.Add(mountain);
            }

            picker.SelectedIndexChanged += (sender, args) =>
            {
                string mountainSelected = picker.Items[picker.SelectedIndex];
                coordinates latLong = location[mountainSelected];
                string latitudeLongitude = latLong.Latitude + "," + latLong.Longitude;

                // API URL and KEY + Locations .
                parseForecast("http://api.wunderground.com/api/" + wunderground_key + "/geolookup/forecast/q/" + latitudeLongitude + ".json");
            };

            // Build the page by putting the picker and the Information Scroll View.
            this.Content = new StackLayout
            {
                Padding = 0,
                Margin = 0,
                Spacing = 0,
                // Add each section of the page.
                Children =
                {
                    picker,
                    new ScrollView{
                        Content = ForecastLayout
                    },
                }
             };          
        }

        // Start putting info inside of the Table.
        public void parseForecast(string url)
        {
            // Do the request/response and Convery the Json Call.
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            dynamic stuff = JsonConvert.DeserializeObject(response.Content);


            // Pull the data from the JSON file and put them into a Variable to pass into the Array. 
            string Day = stuff.forecast.txt_forecast.forecastday[0].title;
            string fctDay = stuff.forecast.txt_forecast.forecastday[0].fcttext_metric;

            string Night = stuff.forecast.txt_forecast.forecastday[1].title;
            string fctNight = stuff.forecast.txt_forecast.forecastday[1].fcttext_metric;

            string Day1 = stuff.forecast.txt_forecast.forecastday[2].title;
            string fctDay1 = stuff.forecast.txt_forecast.forecastday[2].fcttext_metric;

            string Night1 = stuff.forecast.txt_forecast.forecastday[3].title;
            string fctNight1 = stuff.forecast.txt_forecast.forecastday[3].fcttext_metric;

            string Day2 = stuff.forecast.txt_forecast.forecastday[4].title;
            string fctDay2 = stuff.forecast.txt_forecast.forecastday[4].fcttext_metric;

            string Night2 = stuff.forecast.txt_forecast.forecastday[5].title;
            string fctNight2 = stuff.forecast.txt_forecast.forecastday[5].fcttext_metric;

            string Day3 = stuff.forecast.txt_forecast.forecastday[6].title;
            string fctDay3 = stuff.forecast.txt_forecast.forecastday[6].fcttext_metric;

            string Night3 = stuff.forecast.txt_forecast.forecastday[7].title;
            string fctNight3 = stuff.forecast.txt_forecast.forecastday[7].fcttext_metric;

            // Create a Title to be used and passed into the array in front of each Weather Variable.
            string forecast = "3 day forcast";

            // Create an array to include both the title and the forecast of each Period.
            string[] itemsArray = new string[] {    forecast + ": ",
                                                Day +": " + fctDay,
                                                Night +": " + fctNight,
                                                Day1 +": " + fctDay1,
                                                Night1 +": " + fctNight1,
                                                Day2 +": " + fctDay2,
                                                Night2 +": " + fctNight2,
                                                Day3 +": " + fctDay3,
                                                Night3 +": " + fctNight3
                                               };

            // Loop through each item in the array, pass it into an ITEMS Variable and put into individual Labels.
            foreach (var item in itemsArray)
            {
                ForecastLayout.Children.Add(
                    new Frame
                    {
                        // Frame styling/ Properties
                        OutlineColor = Color.FromHex("#757575"),
                        BackgroundColor = Color.FromHex("#FAFAFA"),

                        Content = new Label
                        {
                            Text = item,
                            TextColor = Color.FromHex("#263238"),
                            
                        }

                    }
                    ); 
                        
                        
            }

        }

     }  

}
