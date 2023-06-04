using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Mvvm.Models.EarthquakeListPageModel
{
    internal class EarthquakeModel
    {  
          public List<Earthquakes> features { get; set; }
        public GeneralInfoEarthquake metadata { get; set; }
    }
    public class Earthquakes
    {    
            public EarthquakeProperties properties { get; set; }

    }
    internal class Deprem
    {
        public List<UnitDeprem> earthquakes { get; set; }
    }
    public class UnitDeprem
    {
        public string date { get; set; }

        public string location { get; set; }
        public DepremSize size { get; set; }

    }
    public class DepremSize
    {
        public string mw { get; set; }
        public string ml { get; set; }
        public string md { get; set; }
    }
    public class EarthquakeProperties
    {
        public double mag { get; set; }
        public string place { get; set; }
        public long time { get; set; }
        public string date { get; set; }
        public string url { get; set; }

        public double tsunami { get; set; }

        public string title { get; set; }
        public string magType { get; set; }

        public EarthquakeLocation geometry { get; set; }
       
        public double dmin { get; set; }
        public double rms { get; set; }
        public double gap { get; set; }

    }
    public class GeneralInfoEarthquake
    {
        public int count { get; set; }
    }

    public class EarthquakeLocation
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }

}
