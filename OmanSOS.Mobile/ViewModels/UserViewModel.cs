using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OmanSOS.Mobile.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        #region Private Properties
        string name;
        string email;
        string phone;
        string passowrd;
        string confirmPassword;
        #endregion

        #region Public Properties
        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        public string Email
        {
            set { SetProperty(ref email, value); }
            get { return email; }
        }

        public string Phone
        {
            set { SetProperty(ref phone, value);}
            get { return phone; }
        }

        public string Passowrd
        {
            set { SetProperty(ref passowrd, value); }
            get { return passowrd; }
        }

        public string ConfirmPassword
        {
            set { SetProperty(ref confirmPassword, value); }
            get { return confirmPassword; }
        }
        #endregion

        #region Methods & Event Handlers
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
        #endregion
    }
}
