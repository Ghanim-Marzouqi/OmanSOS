using Microsoft.Maui.Controls;
using System;

namespace OmanSOS.Mobile.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private void GoToRequestHelpPage(object sender, EventArgs args)
        {
            Navigation.PushAsync(new RequestHelpPage());
        }
    }
}
