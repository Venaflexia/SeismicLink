using CommunityToolkit.Maui.Views;
using SeismicLink.Mvvm.ViewModels;
using System.Globalization;

namespace SeismicLink.Mvvm.Views;

public partial class FilterPopupPage : Popup
{
    EarthquakeListPageVM Vm;
    public FilterPopupPage(EarthquakeListPageVM vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
        Vm = vm;
        EndTimePicker.Date = DateTime.Now;
        if (Microsoft.Maui.Storage.Preferences.ContainsKey("PickerCountryLocal"))
        {
          myPicker.SelectedItem=Microsoft.Maui.Storage.Preferences.Get("PickerCountryLocal", "");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (Convert.ToDouble(MagValueMaxEntry.Text) > (Convert.ToDouble(MagValueMinEntry.Text))) 
        {
            if(!string.IsNullOrEmpty(Vm.CityName)) 
            {
                await Vm.SearchEarthquake();
            }
          
            Close();
        }
        else
        {
         await Application.Current.MainPage.DisplayAlert("Warning", "The minimum value cannot be greater than the maximum value.", "OK");
        }

    }
    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Microsoft.Maui.Storage.Preferences.Set("PickerCountryLocal", myPicker.SelectedItem.ToString());
    }
}