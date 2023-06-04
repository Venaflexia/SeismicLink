using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Mvvm.Models.EmergencyAssistanceModel
{
   public class UnitCommunicationHelp
    {
        public string message { get; set; }
        public string name { get; set; }
        public DateTime sharedDate { get; set; }
        public int report { get; set; }
        public string userId { get; set; }
        public string helpRequester { get; set; }
        public bool garbageIsVisible { get; set; }
        public string docId { get; set; }
    }
}
