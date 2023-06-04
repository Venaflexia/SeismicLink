using CommunityToolkit.Maui.Views;
using SeismicLink.Mvvm.Models.Enums;
using SeismicLink.Mvvm.Views;
using SeismicLink.Services.FirebaseServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeismicLink.Mvvm.ViewModels
{
    class SeismicLinkLoginPageVM:INotifyPropertyChanged
    {
		private bool checkContrat;

		public bool CheckContrat
        {
			get { return checkContrat; }
			set { checkContrat = value; OnPropertyChanged("CheckContrat"); OnPropertyChanged("CanLogin"); OnPropertyChanged("CanRegister"); }
		}

		private string userName;

		public string UserName
		{
			get { return userName; }
			set { userName = value; OnPropertyChanged("CanRegister"); OnPropertyChanged("UserName"); }
		}

		private string confirmPassword;

		public string ConfirmPassword
		{
			get { return confirmPassword; }
			set{ confirmPassword = value; OnPropertyChanged("CanRegister");OnPropertyChanged("ConfirmPassword"); }
		}



		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string PropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

        private string email;

		public string Email
		{
			get { return email; }
			set { email = value; OnPropertyChanged("Email"); OnPropertyChanged("CanLogin"); OnPropertyChanged("CanRegister"); }
		}

		private string password;
        public string Password
		{
			get { return password; }
			set { password = value; OnPropertyChanged("Password");OnPropertyChanged("CanLogin"); OnPropertyChanged("CanRegister"); }
		}

        public bool CanLogin
		{
			get
			{
				if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
				{
					return false;
				}
				return true;
			}		
		}

        public bool CanRegister 
		{
			get 
			{
				if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(UserName) || !CheckContrat)  
				{
					return false;
				}
				return true;
			}
		}

		public ICommand CmdLogin { get; set; }
        public ICommand CmdRegister { get; set; }


        public SeismicLinkLoginPageVM()
        {
			CmdLogin = new Command(LoginClicked, CanLoginClicked);
			CmdRegister = new Command(RegisterClicked, CanRegisterClicked);					
        }
        
        private async void RegisterClicked(object obj)
        {

            bool hasUpperCase = false;
            bool hasLowerCase = false;

            if (password.Length > 7)
			{
                foreach (char i in password)
                {
                    if (Char.IsUpper(i))
                    {
                        hasUpperCase = true;
                    }
                    else if (Char.IsLower(i))
                    {
                        hasLowerCase = true;
                    }
					if (hasLowerCase && hasUpperCase)
					{
						break;
                    }
                }
				if(hasUpperCase && hasLowerCase) 
				{
                    if (confirmPassword == password)
                    {
                        string pass = Password;
                        Password = string.Empty;
                        ConfirmPassword = string.Empty;
                        LoginEnums result = await FirebaseAuth.RegisterUser(userName, email, pass);
                        if (result == LoginEnums.Error)
                        {
                            await Application.Current.MainPage.DisplayAlert("Warning", "An unknown error occurred. Please try again.", "ok");
                        }
                        else if (result == LoginEnums.Unverified)
                        {
                            Application.Current.MainPage.ShowPopup(new VerificationPopup());
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                        else if (result == LoginEnums.Verified)
                        {
                            await Application.Current.MainPage.DisplayAlert("Warning", "Your account has been created.", "ok");
                           await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Warning", "The passwords do not match", "ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Please choose a more complex password.", "ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please enter a longer password", "ok");
            }
			
        }

        private bool CanRegisterClicked(object arg)
        {
			return CanRegister;
        }

        private bool CanLoginClicked(object arg)
        {
			return CanLogin;
        }
        private async void LoginClicked(object obj)
        {
			string pass = Password;
            Password = string.Empty;
            LoginEnums result =await FirebaseAuth.AuthenticateUser(email, pass);      
            if (result==LoginEnums.Error) 
			{
				await Application.Current.MainPage.DisplayAlert("Warning", "You have entered an incorrect username or password!", "ok");
			}
			else if(result==LoginEnums.Unverified)
			{     
                Application.Current.MainPage.ShowPopup(new VerificationPopup());	
                
            }
			else if(result== LoginEnums.Verified)
			{
               await Shell.Current.GoToAsync("//EarthquakeListPage");
                //An email verification code will be sent
            }
        }
    }
}
