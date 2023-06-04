using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Mvvm.Models.EmergencyAssistanceModel
{
    public class EarthquakeEmergencyAssistanceModel
    {
        public bool IsMatchAuthor { get; set; }
        public string Author { get; set; }
        public int Reported { get; set; }
        public int Supported { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime SharedDate { get; set; }
        public string HelpDescription { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public bool EmailIsVisible { get; set; }
        public bool TelNoIsVisible { get; set; }
        public string Id { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
}
