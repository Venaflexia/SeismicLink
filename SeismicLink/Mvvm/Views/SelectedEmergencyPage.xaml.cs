using SeismicLink.Mvvm.Models.EmergencyAssistanceModel;
using SeismicLink.Mvvm.ViewModels;
using SeismicLink.Services.FirebaseServices;
using System.Collections.ObjectModel;

namespace SeismicLink.Mvvm.Views;

public partial class SelectedEmergencyPage : ContentPage
{
    EarthquakeEmergencyAssistanceModel vm;
    EarthquakeEmergencyAssistanceModel Parameter;
    public SelectedEmergencyPage(EarthquakeEmergencyAssistanceModel parameter)
	{
		InitializeComponent();
        list = new ObservableCollection<UnitCommunicationHelp>();
        vm = parameter;
       this.BindingContext = vm;
        Parameter= parameter;
        if (vm.IsMatchAuthor)
        {
            btnDelete.IsVisible=true;
        }
        else
        {
            btnDelete.IsVisible=false;
        }
        deneme.ItemsSource = list;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        list.Clear();
        var result = await FirestoreFirebase.ReadEarthquakeEmergencyMessage(new UnitCommunicationHelp { helpRequester = Parameter.Id });
        foreach (var item in result)
        {
            list.Add(item);
        }
        labelCommentsCount.Text = list.Count.ToString();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Attention", "Are you sure you want to report the share?", "Yes", "Cancel");
        if (result)
        {
            vm.Reported++;
            await FirestoreFirebase.UpdateEarthquakeEmergencyAssistance(vm);
        }
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Attention", "Are you sure you want to delete this post?", "Yes", "Cancel");
        if (result)
        {           
           FirestoreFirebase.DeleteEarthquakeEmergencyAssistance(vm);
           await Navigation.PopAsync();
        }
    }

    private async void HelpMessageEntry_Completed(object sender, EventArgs e)
    {

        if(!string.IsNullOrEmpty(HelpMessageEntry.Text))
        {
            bool result = await DisplayAlert("attention", "Are you sure you want to share?", "ok", "cancel");
            if (result)
            {
                FirestoreFirebase.InsertEarthquakeEmergencyMessage(new UnitCommunicationHelp { message = HelpMessageEntry.Text, name = FirebaseAuth.GetUserInfo().UserName, helpRequester = Parameter.Id, sharedDate = DateTime.Now });
                HelpMessageEntry.Text = "";
                list.Clear();
                var resultMessage = await FirestoreFirebase.ReadEarthquakeEmergencyMessage(new UnitCommunicationHelp { helpRequester = Parameter.Id });
                foreach (var item in resultMessage)
                {
                    list.Add(item);
                }
                labelCommentsCount.Text = list.Count.ToString();
            }
         
        }
      
    }

    public ObservableCollection<UnitCommunicationHelp> list { get; set; }
    private async void refreshView_Refreshing(object sender, EventArgs e)
    {
        list.Clear();
       var result = await FirestoreFirebase.ReadEarthquakeEmergencyMessage(new UnitCommunicationHelp { helpRequester = Parameter.Id });
        foreach (var item in result)
        {
            list.Add(item);
        }
        
        refreshView.IsRefreshing = false;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
       bool result=await DisplayAlert("attention", "Are you sure you want to delete your message?", "ok", "cancel");
       var asas= sender as Label;
       var teds= asas.BindingContext as UnitCommunicationHelp;
        if (result)
        { 
            FirestoreFirebase.DeleteEarthquakeEmergencyMessage(teds); 
            list.Clear();
            var resultMessage = await FirestoreFirebase.ReadEarthquakeEmergencyMessage(new UnitCommunicationHelp { helpRequester = Parameter.Id });
            foreach (var item in resultMessage)
            {
                list.Add(item);
            }
            labelCommentsCount.Text = list.Count.ToString();
        }
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
      bool result= await DisplayAlert("attetion", "Are you sure you want to report this message?", "ok", "cancel");
      var asas = sender as Label;
      var teds = asas.BindingContext as UnitCommunicationHelp;
        teds.report += 1;
        
        if (result)
        {
            FirestoreFirebase.UpdateEarthquakeEmergencyMessage(teds);
            list.Clear();
            var resultMessage = await FirestoreFirebase.ReadEarthquakeEmergencyMessage(new UnitCommunicationHelp { helpRequester = Parameter.Id });
            foreach (var item in resultMessage)
            {
                list.Add(item);
            }
            labelCommentsCount.Text = list.Count.ToString();
        }
    }
}