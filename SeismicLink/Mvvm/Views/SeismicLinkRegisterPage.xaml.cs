namespace SeismicLink.Mvvm.Views;

public partial class SeismicLinkRegisterPage : ContentPage
{
	public SeismicLinkRegisterPage()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
       await Navigation.PopModalAsync();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Documents.checkbox11 && Documents.checkbox22)
        {
           
        }
        else
        {
            checkbox.IsChecked = false;
        }
    }
    private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (checkbox.IsChecked)
        {
            await Navigation.PushModalAsync(new Documents(true));
        }
      
    }

    private void TapGestureRecognizer_Tapped_Password(object sender, TappedEventArgs e)
    {
        if (password.IsPassword)
        {
            password.IsPassword = false;
        }
        else
        {
            password.IsPassword = true;
        }
    }

    private void TapGestureRecognizer_Tapped_PasswordConfirm(object sender, TappedEventArgs e)
    {
        if (passwordConfirm.IsPassword)
        {
            passwordConfirm.IsPassword = false;
        }
        else
        {
            passwordConfirm.IsPassword = true;
        }
    }
}