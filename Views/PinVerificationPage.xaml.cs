using MauiApp1.Models;
using MauiApp1.Sevices;

namespace  MauiApp1.Views;

public partial class PinVerificationPage : ContentPage
{
    private string enteredPin = string.Empty;

    public PinVerificationPage()
    {
        InitializeComponent();
        CheckForUpdate();
    }

    private async void OnNumberButtonClicked(object sender, EventArgs e)
    {
        if (enteredPin.Length < 4) // Assuming PIN length is 6
        {
            Button button = sender as Button;
            enteredPin += button.Text;
            PinEntry.Text = new string('*', enteredPin.Length);

            if(enteredPin.Length == 4)
            {
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;
                
                var pin = await SecureStorage.Default.GetAsync("pin");
                string ot_uid = await SecureStorage.Default.GetAsync("username") ?? "";
                string fio = await SecureStorage.Default.GetAsync("fio") ?? "";
                string otdel = await SecureStorage.Default.GetAsync("otdel") ?? "";
                string uniq = "iOS";
                var service = new LoginService();
                if (!string.IsNullOrEmpty(pin) && enteredPin == pin)
                {
                    string result1 = "моб - Успешный вход (пин)";
                    string result = await service.LoginLog(ot_uid, fio, otdel, uniq, result1);

                    await Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    string result1 = "моб - Ошибка входа (пин)";
                    string result = await service.LoginLog(ot_uid, fio, otdel, uniq, result1);
                    await DisplayAlert("Ошибка", "Неправильный ПИН", "OK");
                    enteredPin = string.Empty;
                    PinEntry.Text = string.Empty;
                }

                activityIndicator.IsVisible = false;
                activityIndicator.IsRunning = false;
            }

        }
    }

    private void OnBackspaceButtonClicked(object sender, EventArgs e)
    {
        if (enteredPin.Length > 0)
        {
            enteredPin = enteredPin.Substring(0, enteredPin.Length - 1);
            PinEntry.Text = new string('*', enteredPin.Length);
        }
    }

    private void OnPinEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        entry.Text = new string('�', e.NewTextValue.Length);
    }

    private async void OnSubmitExitClicked(object sender, EventArgs e)
    {
        SecureStorage.Default.RemoveAll();
        Preferences.Default.Clear();
        await Shell.Current.GoToAsync("LoginPage");
    }

    private async Task<(string Username, string Token, string Pin)> GetSessionAsync()
    {
        var username = await SecureStorage.GetAsync("username");
        var token = await SecureStorage.GetAsync("otNom");
        var pin = await SecureStorage.GetAsync("pin");
        return (username, token, pin);
    }

    private async void CheckForUpdate()
    {
        var currentVersion = AppInfo.VersionString; // "1.0.3"
        //var latestVersion = await PlayStoreVersionChecker.GetLatestVersionFromPlayStore();
        var latestVersion = await PlayStoreVersionChecker.GetLatestVersionFromDatabase();

        if (latestVersion == null) return;

        //bool isUpdateAvailable = PlayStoreVersionChecker.IsNewVersionAvailable(currentVersion, latestVersion); // ��� playMarket
        //if (isUpdateAvailable)

        if (!currentVersion.Equals(latestVersion))
        {
            var answer = await Shell.Current.DisplayAlert("Подтвердите действие", "Доступна новая версия приложения, хотите обновить?", "Да", "Нет");
            if (answer)
            {
                Uri uri = new Uri("https://apps.apple.com/kg/app/creditarm/id6673905182");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }
        else
        {
            Console.WriteLine("Вы используете последнюю версию.");

        }
    }

    private bool IsNewVersionAvailable(string currentVersion, string latestVersion)
    {
        return string.Compare(currentVersion, latestVersion) < 0;
    }

  
}
