using Newtonsoft.Json;
using SeismicLink.Mvvm.Models.EarthquakeListPageModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Services.Api
{
    internal class EarthquakeListPageApi
    {
        public const string BASE_URL = "https://earthquake.usgs.gov";
        public const string QUERY = "/fdsnws/event/1/query?format=geojson&starttime={0}&endtime={1}&minmagnitude={2}&maxmagnitude={3}&minlatitude={4}&minlongitude={5}&maxlatitude={6}&maxlongitude={7}";
        public const string DefaultLocation = "https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&starttime=2023-01-01&endtime={0}&minmagnitude=0&maxmagnitude=10&minlatitude=35.8081&minlongitude=25.6645&maxlatitude=42.3955&maxlongitude=44.8181";
        public static async Task<EarthquakeModel> GetEarthquakes(string DateTimeStart,string DateTimeEnd,double Minmagnitude,double Maxmagnitude,double Minlatitude,double Minlongitude,double Maxlatitude, double Maxlongitude ,int boundingBoxExpansion)
        {
            // Calculate the expansion distance based on the original bounding box
           
            if (Maxmagnitude == 0)
            {
                Maxmagnitude = 10;
            }
            if (DateTimeStart == "01-01-01")
            {
                DateTimeStart = "23-01-01";
            }
            if (DateTimeEnd == "01-01-01")
            {
                DateTimeEnd = DateTime.Now.ToString("yy-MM-dd");
            }

            double latDistance = Maxlatitude - Minlatitude;
            double longDistance = Maxlongitude - Minlongitude;
            double latExpansion = latDistance * boundingBoxExpansion / 100.0;
            double longExpansion = longDistance * boundingBoxExpansion / 100.0;

            double maxFaceArea = 8000; // Set the maximum face area threshold (adjust as needed)
            double currentFaceArea = latDistance * longDistance;
            if (currentFaceArea > maxFaceArea)
            {
               await Application.Current.MainPage.DisplayAlert("Warning", "Please provide a smaller facial measurement or reduce the Bounding Box Expansion value, and make other parameters more specific.", "Ok");
                return null;
            }
            else 
            {
                // Expand the bounding box by the calculated distance
                double newMinlatitude = Minlatitude - latExpansion;
                double newMinlongitude = Minlongitude - longExpansion;
                double newMaxlatitude = Maxlatitude + latExpansion;
                double newMaxlongitude = Maxlongitude + longExpansion;

                // Format the coordinates using periods instead of commas
                string formattedUrl = BASE_URL + string.Format(QUERY,
                    DateTimeStart,
                    DateTimeEnd,
                    Minmagnitude,
                    Maxmagnitude,
                    newMinlatitude.ToString("0.0000", CultureInfo.InvariantCulture),
                    newMinlongitude.ToString("0.0000", CultureInfo.InvariantCulture),
                    newMaxlatitude.ToString("0.0000", CultureInfo.InvariantCulture),
                    newMaxlongitude.ToString("0.0000", CultureInfo.InvariantCulture));

                EarthquakeModel Earthquakes = new EarthquakeModel();

                // string url = BASE_URL + string.Format(QUERY,"2023-05-05",Minlatitude,Minlongitude,Maxlatitude,Maxlongitude);
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(formattedUrl);
                    string json = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Earthquakes = JsonConvert.DeserializeObject<EarthquakeModel>(json);
                    }

                }
                return Earthquakes;
            }
            
           
        }
        public static async Task<EarthquakeModel> GetDefault()
        {
            EarthquakeModel Earthquakes = new EarthquakeModel();
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (HttpClient client = new HttpClient(handler))
            {
                string url = string.Format(DefaultLocation, DateTime.Now.ToString("yy-MM-dd"));
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                Earthquakes = JsonConvert.DeserializeObject<EarthquakeModel>(json);
            }
            return Earthquakes;
        }
        internal static async Task<Deprem> GetInfo(string sehir,double size)
        {           
            HttpClient client = new HttpClient();
            string urlv = $"https://deprem-api.vercel.app/?size={size}&location={sehir}";
            var ddd = await client.GetStringAsync(urlv);
            Deprem result = JsonConvert.DeserializeObject<Deprem>(ddd);
            return result;
        }
    }
}
