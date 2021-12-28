using DevExpress.Maui.Charts;
using DevExpress.Maui.CollectionView;
using DevExpress.Maui.DataGrid;
using DevExpress.Maui.Editors;
using DevExpress.Maui.Navigation;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace OmanSOS.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<DXCollectionView, DXCollectionViewHandler>();
                    handlers.AddHandler<ChartView, ChartViewHandler>();
                    handlers.AddHandler<PieChartView, PieChartViewHandler>();
                    handlers.AddHandler<DataGridView, DataGridViewHandler>();
                    handlers.AddHandler<SimpleButton, SimpleButtonHandler>();
                    handlers.AddHandler<TextEdit, TextEditHandler>();
                    handlers.AddHandler<CheckEdit, CheckEditHandler>();
                    handlers.AddHandler<NumericEdit, NumericEditHandler>();
                    handlers.AddHandler<PasswordEdit, PasswordEditHandler>();
                    handlers.AddHandler<MultilineEdit, MultilineEditHandler>();
                    handlers.AddHandler<ComboBoxEdit, ComboBoxEditHandler>();
                    handlers.AddHandler<DateEdit, DateEditHandler>();
                    handlers.AddHandler<TimeEdit, TimeEditHandler>();
                    handlers.AddHandler<AutoCompleteEdit, AutoCompleteEditHandler>();
                    handlers.AddHandler<DrawerView, DrawerViewHandler>();
                    handlers.AddHandler<TabView, TabViewHandler>();
                    handlers.AddHandler<DrawerPage, DrawerPageHandler>();
                    handlers.AddHandler<TabPage, TabPageHandler>();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("univia-pro-regular.ttf", "Univia-Pro");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                });

            return builder.Build();
        }
    }
}