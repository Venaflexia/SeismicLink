using Newtonsoft.Json;
using SeismicLink.Mvvm.Models.EarthquakeListPageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Services.Api
{
    internal class CityLocationApi
    {
        public const string BASE_URL = "https://api.opencagedata.com";
        public const string QUERY = "/geocode/v1/json?q=URI-ENCODED-{0},+{1}&key=";
        public const string ApiKey = "***";
        public static async Task<CityLocationModel> GetBoundingBox(string CityName, string CountryName)
        {
            if (CountryName == null) 
            {
            CountryName = string.Empty;
            }
            CityLocationModel cityLocationModel;

            string url = BASE_URL + string.Format(QUERY, CityName , CountryName)+ApiKey;
           
            try
            {
                HttpClient client = new HttpClient();
                string response = await client.GetStringAsync(url);
                cityLocationModel = JsonConvert.DeserializeObject<CityLocationModel>(response);
                return cityLocationModel;
            }
            catch (Exception)
            {
              await Application.Current.MainPage.DisplayAlert("Error", "An unexpected error has occurred", "Ok");
                return new CityLocationModel();
            }
        }
    }
}
