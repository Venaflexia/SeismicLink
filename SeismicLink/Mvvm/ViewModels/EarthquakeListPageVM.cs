using CommunityToolkit.Maui.Views;
using SeismicLink.Mvvm.Models.EarthquakeListPageModel;
using SeismicLink.Mvvm.Views;
using SeismicLink.Services.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeismicLink.Mvvm.ViewModels
{
    public class EarthquakeListPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand CmdSearch { get; set; }

        private Earthquakes selectedItem;

        public Earthquakes SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;OnPropertyChanged("SelectedItem"); 
            }
        }


        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged("IsRefreshing"); }
        }

        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set { cityName = value;OnPropertyChanged("CityName"); }
        }

       
        private bool kandilliRasathanesi;

        public bool KandilliRasathanesi 
        {
            get { return kandilliRasathanesi; }
            set { kandilliRasathanesi = value; if (kandilliRasathanesi) { SearchClicked(""); BoundingBoxExpansion = 0; } else { GetDefaultEarthquakeList(); BoundingBoxExpansion = 150; } }
        }
        public string CountryName 
        {
            get { return countryName; }
            set
            {             
                countryName = value;                
                OnPropertyChanged("CountryName");
            } 
        }
        private string countryName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double MagValueMin { get; set; }
        private int boundingBoxExpansion;

        public int BoundingBoxExpansion
        {
            get { return boundingBoxExpansion; }
            set { boundingBoxExpansion = value;OnPropertyChanged("BoundingBoxExpansion"); }
        }


        private DateTime todayDate;

        public DateTime TodayDate
        {
            get { return todayDate; }
            set { todayDate = value;OnPropertyChanged("TodayDate"); }
        }

    
        private double magValueMax;

        public double MagValueMax
        {
            get { return magValueMax; }
            set { magValueMax = value;OnPropertyChanged("MagValueMax"); }
        }


        private bool labelIsVisible;

        public bool LabelIsVisible
        {
            get { return labelIsVisible; }
            set { labelIsVisible = value; OnPropertyChanged("LabelIsVisible"); }
        }

        private int countEarthquake;

        public int CountEarthquake
        {
            get { return countEarthquake; }
            set { countEarthquake = value ; OnPropertyChanged("CountEarthquake"); }
        }

       public async void RefreshClicked(object obj)
        {

            if (string.IsNullOrEmpty(CityName))
            {
               await GetDefaultEarthquakeList();
                IsRefreshing = false;
            }
            else
            {
               await SearchEarthquake();
                IsRefreshing = false;
            }
        }

        public async Task SearchEarthquake()
        {
            if (KandilliRasathanesi)
            {
                EarthquakeList.Clear();
                Deprem resultEarthquake = await EarthquakeListPageApi.GetInfo(CityName, MagValueMin);
                foreach (var item in resultEarthquake.earthquakes)
                {
                    EarthquakeList.Add(new Earthquakes { properties = new EarthquakeProperties { title = item.location, magType = "Ml Size=" + item.size.ml, date = item.date } });
                }
                CountEarthquake = resultEarthquake.earthquakes.Count;
                if (EarthquakeList.Count == 0)
                {
                    LabelIsVisible = true;
                }
                else
                {
                    LabelIsVisible = false;
                }
                IsRefreshing = false;
            }
            else
            {
              string CN=CountryName == "All Countries" ? " " : CountryName;
                CityLocationModel result = await CityLocationApi.GetBoundingBox(CityName, CN);
                if (result.total_results == 0)
                {
                    LabelIsVisible = true;
                    EarthquakeList.Clear();
                    CountEarthquake = 0;
                }
                else
                {

                    EarthquakeModel resultEarthquake = await EarthquakeListPageApi.GetEarthquakes(StartTime.ToString("yy-MM-dd"), EndTime.ToString("yy-MM-dd"), MagValueMin, MagValueMax, result.results[0].bounds.southwest.lat, result.results[0].bounds.southwest.lng, result.results[0].bounds.northeast.lat, result.results[0].bounds.northeast.lng, boundingBoxExpansion);
                    if (resultEarthquake == null)
                    {
                        EarthquakeList.Clear();
                        LabelIsVisible = true;
                        CountEarthquake = 0;
                    }
                    else
                    {
                        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        EarthquakeList.Clear();
                        foreach (var feature in resultEarthquake.features)
                        {
                            feature.properties.date = epoch.AddMilliseconds(feature.properties.time).ToLocalTime().ToString();
                            EarthquakeList.Add(feature);
                        }
                        CountEarthquake = resultEarthquake.metadata.count;
                        if (CountEarthquake == 0)
                        {
                            LabelIsVisible = true;
                        }
                        else
                        {
                            LabelIsVisible = false;
                        }
                    }
                
                }
                IsRefreshing = false;
            }

        }
        private async void SearchClicked(object obj)
        {
            await SearchEarthquake();
        }
        public ObservableCollection<Earthquakes> EarthquakeList { get; set; }
        public ICommand CmdSelectionChanged{ get; set; }
        public ICommand CmdRefreshEarthquake { get; set; }
        public ICommand CmdFilter { get; set; }
        public EarthquakeListPageVM()
        {
            EarthquakeList = new ObservableCollection<Earthquakes>();
            CmdSearch = new Command(SearchClicked);
            CmdFilter = new Command(FilterClicked);
            LabelIsVisible = false;
            TodayDate = DateTime.Today;
            MagValueMax = 10;
            CmdSelectionChanged = new Command(SelectClicked);
            BoundingBoxExpansion = 150;
            CmdRefreshEarthquake = new Command(RefreshClicked);
        }
       
        private async void SelectClicked(object obj)
        {
            if (!KandilliRasathanesi&&SelectedItem!=null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new SelectedEarthquakePage() { BindingContext = SelectedItem });
             
                SelectedItem = null;
            }
        }
    

        private  void FilterClicked(object obj)
        {                   
            Application.Current.MainPage.ShowPopup(new FilterPopupPage(this));
        }

        public async Task GetDefaultEarthquakeList()
        {
            if (KandilliRasathanesi)
            {
                EarthquakeList.Clear();
                Deprem resultEarthquake = await EarthquakeListPageApi.GetInfo(CityName, MagValueMin);
                foreach (var item in resultEarthquake.earthquakes)
                {
                    EarthquakeList.Add(new Earthquakes { properties = new EarthquakeProperties { title = item.location, magType = "Ml Size=" + item.size.ml, date = item.date } });
                }
                CountEarthquake = resultEarthquake.earthquakes.Count;
                if (EarthquakeList.Count == 0)
                {
                    LabelIsVisible = true;
                }
                else
                {
                    LabelIsVisible = false;
                }           
            }
            else
            {
                EarthquakeList.Clear();
                EarthquakeModel result = await EarthquakeListPageApi.GetDefault();
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                foreach (var feature in result.features)
                {
                    feature.properties.date = epoch.AddMilliseconds(feature.properties.time).ToLocalTime().ToString();
                    EarthquakeList.Add(feature);
                }
                CountEarthquake = result.metadata.count;
                if (CountEarthquake > 0)
                {
                    LabelIsVisible = false;
                }
                else
                {
                    LabelIsVisible = true;
                }
            }
           
        }
        public async void GetEarthquakeList()
        {
            await GetDefaultEarthquakeList();            
        }


    }
}
