using Microsoft.Maui.Controls;
using OmanSOS.Mobile.Pages;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace OmanSOS.Mobile.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        #region Private Properties
        UserViewModel _user;
        bool _isLoggedIn;
        INavigation _navigation;
        #endregion

        #region Public Properties
        public UserViewModel User
        {
            set { SetProperty(ref _user, value); }
            get { return _user; }
        }
        public bool IsLoggedIn
        {
            set { SetProperty(ref _isLoggedIn, value); }
            get { return _isLoggedIn; }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get; private set; } 
        public ICommand RegisterCommand { get; private set; }
        #endregion

        #region Constuctors
        public LoginPageViewModel(INavigation navigation)
        {
            _navigation = navigation;

            LoginCommand = new Command(
                execute: () =>
                {
                    _user = new UserViewModel();
                    _user.PropertyChanged += OnUserPropertyChanged;
                    Login(_user);
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return _user != null &&
                           _user.Email != null &&
                           _user.Email.Length > 0 &&
                           _user.Passowrd != null &&
                           _user.Passowrd.Length > 0;
                }
            );

            RegisterCommand = new Command(() => Register());
        }

        #endregion

        #region Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnUserPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            (LoginCommand as Command).ChangeCanExecute();
        }

        void RefreshCanExecutes()
        {
            (LoginCommand as Command).ChangeCanExecute();
            (RegisterCommand as Command).ChangeCanExecute();
        }
        #endregion

        #region Private Methods
        private void Login(UserViewModel user)
        {
            if (!_isLoggedIn)
            {
                if (user.Email != "optimist_gm@hotmail.com" && user.Passowrd != "Optimist_GM9")
                {
                    return;
                }

                // Go To Home Page
                _navigation.PushAsync(new HomePage());
            }
        }

        private void Register()
        {
            _navigation.PushAsync(new RegistrationPage());
        }
        #endregion
    }
}
