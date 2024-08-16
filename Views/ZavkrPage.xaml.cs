using MauiApp1.Models;

namespace MauiApp1.Views;

public partial class ZavkrPage : ContentPage
{
    private Zavkr _selectedZavkr;

    public ZavkrPage(Zavkr selectedZavkr)
	{
		InitializeComponent();
        _selectedZavkr = selectedZavkr;

        titleLabel.Text = _selectedZavkr.Title;
        //descriptionLabel.Text = _selectedZavkr.LoanReferenceName;

        BindingContext = this;
        InitializeLocationAsync();
    }

    private async void InitializeLocationAsync()
    {
        try
        {
            var location = await Geolocation.GetLocationAsync();

            if (location != null)
            {
                string latitude = location.Latitude.ToString();
                string longitude = location.Longitude.ToString();
                await SecureStorage.SetAsync("Latitude", latitude);
                await SecureStorage.SetAsync("Longitude", longitude);
            }
            else
            {
                await DisplayAlert("Прелупреждение", "Геолокация у Вас не работает. Фотографии будут бещ местоположения", "OK");
            }
        }
        catch (FeatureNotEnabledException ex)
        {
            await DisplayAlert("Ошибка", "Не удалось получить местоположение", "OK");
            await DisplayAlert("Подсказка", "Включите геолокацию", "OK");
            await Shell.Current.GoToAsync("//HomePage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Сообщение: {ex.Message}", "OK");
        }
    }

    private void OnButtonPhotoClientClicked2(object sender, EventArgs e, int i)
    {
        Navigation.PushAsync(new PhotoClient(_selectedZavkr));
    }
    private void OnButtonZhitelstvaClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ResidencePhoto(_selectedZavkr));
    }

    private void OnButtonDeatelnostiClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ListActivityPhoto(_selectedZavkr));
    }

    private void OnButtonZalogClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Zalog(_selectedZavkr));
    }
    private void OnButtonPhotoClientClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PhotoClient(_selectedZavkr));
    }
}