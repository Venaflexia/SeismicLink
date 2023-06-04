namespace SeismicLink.Mvvm.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private async void BtnSettings_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new SettingsPage());
    }

    private async void BtnContactMe_Clicked(object sender, EventArgs e)
    {
        //await Email.ComposeAsync("hello", "", "bazbabursah1221@gmail.com");
        await Launcher.OpenAsync(new Uri("mailto:help@airteriae.com?subject=SeismicLink&body=How can I help you?"));
    }

    

    private async void BtnGitHub_Clicked(object sender, EventArgs e)
    {
       await Browser.OpenAsync("https://github.com/Venaflexia/SeismicLink.git");
    }  
}