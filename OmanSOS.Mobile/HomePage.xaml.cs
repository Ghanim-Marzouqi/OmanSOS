using DevExpress.Maui.Editors;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System;
using System.ComponentModel;

namespace OmanSOS.Mobile
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void TextEdit_Tap(object sender, HandledEventArgs e)
        {
            (sender as TextEdit).Text = "You tapped me!";
        }

        private void TextEdit_DoubleTap(object sender, HandledEventArgs e)
        {
            (sender as TextEdit).Text = "You double tapped me!";
        }

        private void TextEdit_LongPress(object sender, HandledEventArgs e)
        {
            (sender as TextEdit).Text = "You pressed me!";
        }
    }
}
