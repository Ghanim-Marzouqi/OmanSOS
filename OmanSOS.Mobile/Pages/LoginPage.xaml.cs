using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System;
using System.Windows.Input;

namespace OmanSOS.Mobile.Pages
{
    public partial class LoginPage : ContentPage
    {
        private ICommand GoToRegisterPageCommand => new Command(() => GoToRegisterPage());

        public LoginPage()
        {
            InitializeComponent();
        }

        private void GoToRegisterPage()
        {

        }
    }
}
