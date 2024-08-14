namespace  MauiApp1.Views;

public partial class SetPinPage : ContentPage
{
    public SetPinPage()
    {
        InitializeComponent();
    }

    private async void OnSetPinButtonClicked(object sender, EventArgs e)
    {
        var pin1 = PinEntry1.Text;
        var pin2 = PinEntry2.Text;

        if (!string.IsNullOrWhiteSpace(pin1) && pin1.Length == 4 &&
            !string.IsNullOrWhiteSpace(pin2) && pin2.Length == 4)
        {
            if (pin1 == pin2)
            {
                await SaveSessionAsync(pin1);
                await Shell.Current.GoToAsync(nameof(PinVerificationPage));
            }
            else
            {
                await DisplayAlert("Ошибка", "PIN не совпадают", "OK");
                PinEntry1.Text = string.Empty;
                PinEntry2.Text = string.Empty;
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Введите 4 символа PIN", "OK");
        }
    }

    private async Task SaveSessionAsync(string pin)
    {
        await SecureStorage.SetAsync("pin", pin);
    }

}
