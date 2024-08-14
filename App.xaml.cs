using MauiApp1.Models;
using MauiApp1.Views;
using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class App : Application
    {
        public static UserInfo UserIndo;
        public static AppSettings Settings { get; private set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            // MainPage = new NavigationPage(new MainPage());
            InitializeAsync();
            Settings = new AppSettings()
            {
                FontSize = 16,
            };
        }

        private async void InitializeAsync(){
            var session = await SessionManager.GetSessionAsync();
            if (!string.IsNullOrEmpty(session.Pin))
            {
                MainPage = new AppShell();
                await Shell.Current.GoToAsync($"PinVerificationPage");
            }
            else
            {
                MainPage = new AppShell();
                await Shell.Current.GoToAsync($"LoginPage");
            }
        }
    }
}

