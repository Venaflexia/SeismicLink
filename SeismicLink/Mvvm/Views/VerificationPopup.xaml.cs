using CommunityToolkit.Maui.Views;
using SeismicLink.Services.FirebaseServices;

namespace SeismicLink.Mvvm.Views;

public partial class VerificationPopup : Popup
{
	public VerificationPopup()
	{
		InitializeComponent();
		
	}

    
    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool result =await FirebaseAuth.IsEmailVerified();
        if (result)
        {
            //The user will log in.
            Close();
        }
        else
        {
            Close();
        }
    }
}