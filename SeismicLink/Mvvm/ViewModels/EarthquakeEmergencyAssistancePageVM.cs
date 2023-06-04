using SeismicLink.Mvvm.Models.EmergencyAssistanceModel;
using SeismicLink.Mvvm.Views;
using SeismicLink.Services.FirebaseServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeismicLink.Mvvm.ViewModels
{
    internal class EarthquakeEmergencyAssistancePageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CmdAddEmergency { get; set; }
        public ObservableCollection<EarthquakeEmergencyAssistanceModel> EarthquakeEmergencyAssistanceList { get; set; }
        public ICommand CmdShareEmegency { get; set; }
        public ICommand RefreshList { get; set; }
        public ICommand CmdSearchEmergency { get; set; }
        public string SearchBarText { get; set; }

        private EarthquakeEmergencyAssistanceModel selectedItem;

        public EarthquakeEmergencyAssistanceModel SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value;OnPropertyChanged("SelectedItem"); }
        }
        public ICommand CmdSelectionChanged { get; set; }

        private string labelHelp;

        public string LabelHelp
        {
            get { return labelHelp; }
            set { labelHelp = value; OnPropertyChanged("LabelHelp"); }
        }


        private bool labelIsVisibleHelp;

        public bool LabelIsVisibleHelp
        {
            get { return labelIsVisibleHelp; }
            set { labelIsVisibleHelp = value; OnPropertyChanged("LabelIsVisibleHelp"); }
        }

        public EarthquakeEmergencyAssistancePageVM()
        {          
            EarthquakeEmergencyAssistanceList = new ObservableCollection<EarthquakeEmergencyAssistanceModel>();
            CmdAddEmergency = new Command(AddEmergencyClicked);
            CmdShareEmegency = new Command(ShareEmergencyClicked, CanShareEmergency);
            RefreshList = new Command(Refresh);
            CmdSearchEmergency = new Command(SearchEmergencyClicked);
            CmdSelectionChanged = new Command(SelectionChangedClicked);
            LabelHelp = "No help request has been submitted yet.";
        }

     

        private async void SelectionChangedClicked(object obj)
        {
            if(SelectedItem != null) 
            {
                await Application.Current.MainPage.Navigation.PushAsync(new SelectedEmergencyPage(SelectedItem) { });
                SelectedItem = null;
            }          
        }


        private async void SearchEmergencyClicked(object obj)
        {
            await SearchEmergency();
        }
        public async Task SearchEmergency()
        {
            EarthquakeEmergencyAssistanceList.Clear();
            if (Microsoft.Maui.Storage.Preferences.ContainsKey(App.SelectedCountryNameSh))
            {
                if (SearchBarText == null)
                {
                    SearchBarText = "";
                }
                IList<EarthquakeEmergencyAssistanceModel> list = await FirestoreFirebase.ReadEarthquakeEmergencyAssistance(SearchBarText.ToLower(), Microsoft.Maui.Storage.Preferences.Get(App.SelectedCountryNameSh, "").ToLower());
                foreach (EarthquakeEmergencyAssistanceModel item in list)
                {
                    EarthquakeEmergencyAssistanceList.Add(item);
                }
                if (EarthquakeEmergencyAssistanceList.Count == 0)
                {
                    LabelIsVisibleHelp = true;
                    LabelHelp = "No help request has been submitted yet.";
                }
                else
                {
                    LabelIsVisibleHelp = false;
                }
            }
            else
            {

                labelIsVisibleHelp = true;
                LabelHelp = "Please select your country from the settings menu.";
            }
        }

        private async void Refresh(object obj)
        {    IsRefreshing = true;
            await SearchEmergency();
            IsRefreshing = false;
        }

        public bool CanShare 
        {
            get
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(CountryName) || string.IsNullOrEmpty(CityName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(HelpDescription) || string.IsNullOrEmpty(TelNo))
                {
                    return false;
                }
                return true;
            }
        }
        private bool CanShareEmergency(object arg)
        {
            return CanShare;
        }
    

        private EarthquakeEmergencyAssistanceModel SharedModel;

        private int _reported;
        private int _supported;
        private string _userName;
        private string _name;
        private string _lastName;
        private DateTime _sharedDate;
        private string _helpDescription;
        private string _email;
        private string _telNo;
        private bool _emailIsVisible;
        private bool _telNoIsVisible;
        private string _id;
        private string _countryName;
        private string _cityName;

        public int Reported
        {
            get { return _reported; }
            set { _reported = value; }
        }

        public int Supported
        {
            get { return _supported; }
            set { _supported = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("CanShare"); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged("CanShare"); }
        }

        public DateTime SharedDate
        {
            get { return _sharedDate; }
            set { _sharedDate = value; }
        }

        public string HelpDescription
        {
            get { return _helpDescription; }
            set { _helpDescription = value; OnPropertyChanged("CanShare"); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("CanShare"); }
        }

        public string TelNo
        {
            get { return _telNo; }
            set { _telNo = value; OnPropertyChanged("CanShare"); }
        }

        public bool EmailIsVisible
        {
            get { return _emailIsVisible; }
            set { _emailIsVisible = value; }
        }

        public bool TelNoIsVisible
        {
            get { return _telNoIsVisible; }
            set { _telNoIsVisible = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string CountryName
        {
            get { return _countryName; }
            set { _countryName = value; OnPropertyChanged("CanShare"); }
        }

        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; OnPropertyChanged("CanShare"); }
        }
        
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged("IsRefreshing"); }
        }

        private void ShareEmergencyClicked(object obj)
        {
            try
            {
                SharedModel = new EarthquakeEmergencyAssistanceModel
                {
                    CityName = _cityName,
                    CountryName = _countryName,
                    EmailIsVisible = _emailIsVisible,
                    Email = _email,
                    TelNo = _telNo,
                    TelNoIsVisible = _telNoIsVisible,
                    HelpDescription = _helpDescription,
                    LastName = _lastName,
                    Name = _name,
                    SharedDate = DateTime.Now
                }; 
              bool result = FirestoreFirebase.InsertEarthquakeEmergencyAssistance(SharedModel);
                Application.Current.MainPage.Navigation.PopAsync();
            }

            catch (Exception ex)
            {
                throw;
            } 
        }

        private async void AddEmergencyClicked(object obj)
        {
          await  Application.Current.MainPage.Navigation.PushAsync(new AddEmergencyPage());
        }

        public async Task GetDefault()
        {
            EarthquakeEmergencyAssistanceList.Clear();
            if (Microsoft.Maui.Storage.Preferences.ContainsKey(App.SelectedCountryNameSh))
            {

                IList<EarthquakeEmergencyAssistanceModel> list = await FirestoreFirebase.ReadEarthquakeEmergencyAssistance("", Microsoft.Maui.Storage.Preferences.Get(App.SelectedCountryNameSh, "").ToLower());
                foreach (EarthquakeEmergencyAssistanceModel item in list)
                {
                    EarthquakeEmergencyAssistanceList.Add(item);
                }
                if (EarthquakeEmergencyAssistanceList.Count == 0)
                {
                    LabelIsVisibleHelp = true;
                    LabelHelp = "No help request has been submitted yet.";
                }
                else
                {
                    LabelIsVisibleHelp = false;
                }
            }
            else
            {
                LabelIsVisibleHelp = true;
                LabelHelp = "Please select your country from the settings menu.";
            }
        }
        public async void GetDefaultListEarthquakeEmergency()
        {
            await GetDefault();
        }
    }
}
