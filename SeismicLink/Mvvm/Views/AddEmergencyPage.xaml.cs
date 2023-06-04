using System.Globalization;

namespace SeismicLink.Mvvm.Views;

public partial class AddEmergencyPage : ContentPage
{
	public AddEmergencyPage()
	{
		InitializeComponent();

        var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(x => new RegionInfo(x.Name))
            .OrderBy(x => x.EnglishName)
            .Distinct()
            .ToList();

        var countries = cultures.Select(x => x.EnglishName).Distinct().ToList();
        picker.ItemsSource = countries;
    }
}