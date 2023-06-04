using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using SeismicLink.Mvvm.Models.UserInfo;
using SeismicLink.Services.FirebaseServices;
using System.Globalization;

namespace SeismicLink.Mvvm.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        UserInfo ui = FirebaseAuth.GetUserInfo();
        emailInfo.Text = ui.UserEmail;
        userNameInfo.Text= ui.UserName;

        if (Microsoft.Maui.Storage.Preferences.ContainsKey(App.SelectedCountryNameSh))    
        {
            picker.SelectedItem = Microsoft.Maui.Storage.Preferences.Get(App.SelectedCountryNameSh,"");
        }
    }

    private async void btnSignOut_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Attention", "Are you sure you want to log out of your account?", "Yes", "Cancel");
        if (result)
        {
            FirebaseAuth.SignOutUser();
            await Shell.Current.GoToAsync("//SeismicLinkLoginPage");
        }
            
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Attention", "Are you sure you want to delete your account?", "Yes", "Cancel");
        if (result)
        {
            await FirebaseAuth.DeleteUser();
            await Shell.Current.GoToAsync("//SeismicLinkLoginPage");
        }
       
    }
   
    private async void aboutTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
      await Navigation.PushAsync(new Documents(false));
    }

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        int a=picker.SelectedIndex;
        Microsoft.Maui.Storage.Preferences.Set(App.SelectedCountryNameSh,picker.SelectedItem.ToString());
    }
   
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (NewUsernameStacklayout.IsVisible)
        {
            NewUsernameStacklayout.IsVisible = false;
        }
        else
        {
            NewUsernameStacklayout.IsVisible = true;
        }
       
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        bool result = await DisplayAlert("Attention", "Are you sure you want to change your password?", "Yes", "Cancel");
        if (result)
        {
            this.ShowPopup(new VerificationPopup());
            await FirebaseAuth.ForgotPasswordUser(emailInfo.Text);
        } 
    }

    private async void NewUserNameEntry_Completed(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NewUserNameEntry.Text))
        {
            NewUsernameStacklayout.IsVisible = false;
        }
        else
        {
            bool result = await DisplayAlert("Attention", "Are you sure you want to change the username?", "Yes", "Cancel");
            if (result)
            {
                await FirebaseAuth.UpdateUserInfo(NewUserNameEntry.Text);
                NewUsernameStacklayout.IsVisible = false;
            }
        }          
    }
}