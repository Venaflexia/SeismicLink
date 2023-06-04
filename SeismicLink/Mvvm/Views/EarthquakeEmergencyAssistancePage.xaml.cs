using SeismicLink.Mvvm.ViewModels;

namespace SeismicLink.Mvvm.Views;

public partial class EarthquakeEmergencyAssistancePage : ContentPage
{
    EarthquakeEmergencyAssistancePageVM vm;
    public EarthquakeEmergencyAssistancePage()
	{
		InitializeComponent();
        vm = Resources["vm"] as EarthquakeEmergencyAssistancePageVM;
        vm.GetDefaultListEarthquakeEmergency();
    }
}