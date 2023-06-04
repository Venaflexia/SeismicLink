using SeismicLink.Mvvm.Models.EmergencyAssistanceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Services.FirebaseServices
{

    public interface IFirestore
    {
        bool InsertEarthquakeEmergencyAssistance(EarthquakeEmergencyAssistanceModel earthquakeEmergencyAssistanceModel);
        bool InsertEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp);
        bool DeleteEarthquakeEmergencyAssistance(EarthquakeEmergencyAssistanceModel earthquakeEmergencyAssistanceModel);
        Task<bool> UpdateEarthquakeEmergencyAssistance(EarthquakeEmergencyAssistanceModel earthquakeEmergencyAssistanceModel);
        bool UpdateEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp);
        bool DeleteEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp);
        Task<IList<EarthquakeEmergencyAssistanceModel>> ReadEarthquakeEmergencyAssistance(string cityName,string countryName);
        Task<IList<UnitCommunicationHelp>> ReadEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp);
    }
    internal class FirestoreFirebase 
    {

        public static IFirestore firestoreFirebase = new SeismicLink.Platforms.FirebaseFirestorePlatformSpesific();
        public static bool DeleteEarthquakeEmergencyAssistance(EarthquakeEmergencyAssistanceModel earthquakeEmergencyAssistanceModel)
        {
            return firestoreFirebase.DeleteEarthquakeEmergencyAssistance(earthquakeEmergencyAssistanceModel);
        }
        public static bool DeleteEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp)
        {
            return firestoreFirebase.DeleteEarthquakeEmergencyMessage(unitCommunicationHelp);
        }
        public static bool InsertEarthquakeEmergencyAssistance(EarthquakeEmergencyAssistanceModel earthquakeEmergencyAssistanceModel)
        {
            return firestoreFirebase.InsertEarthquakeEmergencyAssistance(earthquakeEmergencyAssistanceModel);
        }
        public static bool InsertEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp)
        {
            return firestoreFirebase.InsertEarthquakeEmergencyMessage(unitCommunicationHelp);
        }

        public static async Task<IList<EarthquakeEmergencyAssistanceModel>> ReadEarthquakeEmergencyAssistance(string cityName,string countryName)
        {
            return await firestoreFirebase.ReadEarthquakeEmergencyAssistance(cityName,countryName);
        }
        public static async Task<IList<UnitCommunicationHelp>> ReadEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp)
        {
            return await firestoreFirebase.ReadEarthquakeEmergencyMessage(unitCommunicationHelp);
        }

        public static async Task<bool> UpdateEarthquakeEmergencyAssistance(EarthquakeEmergencyAssistanceModel earthquakeEmergencyAssistanceModel)
        {
            return await firestoreFirebase.UpdateEarthquakeEmergencyAssistance(earthquakeEmergencyAssistanceModel);
        }
        public static bool UpdateEarthquakeEmergencyMessage(UnitCommunicationHelp unitCommunicationHelp)
        {
            return  firestoreFirebase.UpdateEarthquakeEmergencyMessage(unitCommunicationHelp);
        }
    }
}
