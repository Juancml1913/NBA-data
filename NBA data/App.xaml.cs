using NBA_data.ViewModels;
using NBA_data.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBA_data
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public App()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Games = new GamesViewModel();
            this.MainPage = new MasterPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
