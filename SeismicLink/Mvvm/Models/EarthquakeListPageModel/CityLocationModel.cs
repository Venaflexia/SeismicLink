using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Mvvm.Models.EarthquakeListPageModel
{
    internal class CityLocationModel
    {
        public List<Location> results { get; set; }
        public int total_results { get; set; }
      
    }

    internal class Location
    {
        public LocationBounding bounds { get; set; }
        public LocationCenter geometry { get; set; }
    }
    internal class LocationBounding
    {
        public BoundingNortheast northeast { get; set; }
        public BoundingSouthwest southwest { get; set; }
        internal class BoundingNortheast
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
        internal class BoundingSouthwest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
   
    internal class LocationCenter
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
   
}
