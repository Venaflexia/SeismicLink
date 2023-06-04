namespace SeismicLink.Mvvm.Views;

public partial class Documents : ContentPage
{
	public Documents(bool parameter1)
	{
		InitializeComponent();
        sl1.IsVisible = parameter1;
        sl2.IsVisible = parameter1;
        btn.IsVisible = parameter1;
	}
    public static bool checkbox11 { get; set; }
    public static bool checkbox22 { get; set; }
    private void Button_Clicked(object sender, EventArgs e)
    {
        checkbox22 = checkbox2.IsChecked;
        checkbox11 = checkbox1.IsChecked;
        Navigation.PopModalAsync();
      
    }

    private void checkbox2_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        checkbox22 = checkbox2.IsChecked;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        checkbox22 = checkbox2.IsChecked;
        checkbox11 = checkbox1.IsChecked;
       
    }
    private void checkbox1_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        checkbox11 = checkbox1.IsChecked;
    }
}