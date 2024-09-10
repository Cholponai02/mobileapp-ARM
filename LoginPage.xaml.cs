using MauiApp1.ViewModels;
using MauiApp1.Views;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
    private bool _isPasswordVisible = false;
    public LoginPage()
	{
		InitializeComponent();
		this.BindingContext = new LoginPageViewModel();
	    UpdatePasswordVisibility();

    }
  
   private void OnShowPassword(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        UpdatePasswordVisibility();
    }

    private void UpdatePasswordVisibility()
    {
        if (_isPasswordVisible)
        {
            passwordEntry.IsPassword = false;
            showPasswordIcon.Source = "hide_icon.png";
        }
        else
        {
            passwordEntry.IsPassword = true;
            showPasswordIcon.Source = "show_icon.png";
        }
    }
    private void OnButtonRegisterClicked(object sender, EventArgs e)
    {
         Application.Current.MainPage.DisplayAlert("Успешно", "Заявка на регистрацию отправлена. Подойти в офис Компании Салым Финанс для получения логина и пароля", "OK");
    }
}