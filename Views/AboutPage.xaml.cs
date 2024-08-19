using MauiApp1.Models;
using MauiApp1.Resources.Localization;
namespace MauiApp1.Views
{
    public partial class AboutPage : ContentPage
    {
        private User _user;
        private bool isLargeFont = false;
        public AboutPage(User user)
        {
            InitializeComponent();
            _user = user;
            BindingContext = this;
            versionLabel.Text = $"Версия {AppInfo.VersionString}";
        FontSizePicker.SelectedIndex = 0;


        //   var languages = new List<Language>
        //     {
        //         new Language { Code = "ru-RU", Name = "русский" },
        //         new Language { Code = "ky-KY", Name = "английский" }
        //     };

        // foreach (var language in languages)
        // {
        //     LanguagePicker.Items.Add(language.Name);
        // }

        // LanguagePicker.SelectedIndex = 0;

        // LocalizationResourceManager = LocalizationResourceManager.Instance;
        }
        
        private async void OnExit(object sender, EventArgs e)
        { 
            SecureStorage.Default.RemoveAll();
            // Preferences.Default.Clear();
            Application.Current.Quit();
            //await Shell.Current.GoToAsync($"//{nameof(LoginPageViewModel)}");
        }
    // private void LanguageChanged(object sender, EventArgs e)
    // {
    //     var selectedLanguage = LanguagePicker.SelectedItem.ToString();
    //     var languages = new Dictionary<string, string>
    //         {
    //             { "russina", "ru-RU" },
    //             { "kyrgyz", "ky-KY" }
    //         };

    //     if (languages.TryGetValue(selectedLanguage, out var cultureCode))
    //     {
    //        // LocalizationResourceManager.Instance.SetCulture(new CultureInfo(cultureCode));
    //     }
    // }

    // public LocalizationResourceManager LocalizationResourceManager { get; }

     private void OnFontSizePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (FontSizePicker.SelectedIndex == 0)
        {
            App.Settings.FontSize = 14; 
        }
        else if (FontSizePicker.SelectedIndex == 1)
        {
            App.Settings.FontSize = 22; 
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //var session = await SessionManager.GetSessionAsync();
        //titleLabel.Text = session.Fio;
        titleLabel.Text = SessionManager.GetoFio();
    }

    }
}
