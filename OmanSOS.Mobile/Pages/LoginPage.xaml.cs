using Microsoft.Maui.Controls;
using System;

namespace OmanSOS.Mobile.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login(object sender, EventArgs args)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void GoToRegisterPage(object sender, EventArgs args)
        {
            Navigation.PushAsync(new RegistrationPage());
        }
    }
}
