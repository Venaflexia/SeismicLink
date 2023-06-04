
using CommunityToolkit.Maui.Views;
using SeismicLink.Services.FirebaseServices;

namespace SeismicLink.Mvvm.Views;

public partial class SeismicLinkLoginPage : ContentPage
{
	public SeismicLinkLoginPage()
	{
		InitializeComponent();    
    }

    private  void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
         Navigation.PushModalAsync(new SeismicLinkRegisterPage());
    }

    private async void ForgotPassword_Tapped(object sender, TappedEventArgs e)
    {
        this.ShowPopup(new VerificationPopup());
        await FirebaseAuth.ForgotPasswordUser(email.Text);

    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        if (EntryPassword.IsPassword)
        {
            EntryPassword.IsPassword= false;
        }
        else
        {
            EntryPassword.IsPassword = true;
        }
    }
}