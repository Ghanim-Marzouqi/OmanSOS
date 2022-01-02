using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using OmanSOS.Mobile.Pages;
using Application = Microsoft.Maui.Controls.Application;

namespace OmanSOS.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new LoginPage { Title = "Oman SOS" });
        }
    }
}
