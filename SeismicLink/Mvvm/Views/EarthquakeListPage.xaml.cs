using SeismicLink.Mvvm.ViewModels;
using SeismicLink.Services.FirebaseServices;

namespace SeismicLink.Mvvm.Views;

public partial class EarthquakeListPage : ContentPage
{
    EarthquakeListPageVM vm;
    public EarthquakeListPage()
	{
		InitializeComponent();
        vm = Resources["vm"] as EarthquakeListPageVM;
        vm.GetEarthquakeList();     
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
       bool result= await FirebaseAuth.IsEmailVerified();
        if (!FirebaseAuth.IsAuthenticated()||!result)
        {
          await Shell.Current.GoToAsync("//SeismicLinkLoginPage");
        }
    
    }

}