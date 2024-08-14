using MauiApp1.Models;
using MauiApp1.Sevices;
using MauiApp1.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace MauiApp1.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        readonly ILoginRepository loginRepository = new LoginService();

        [ICommand]
        public async void Login()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
            {
                User userInfo = await loginRepository.Login(UserName, Password);
                if (userInfo != null && userInfo?.UserNumber != 0) 
                {
                    await SaveSessionAsync(UserName, userInfo.UserNumber.ToString(), userInfo.otFio, userInfo.DepartmentId.ToString());
                    await Shell.Current.GoToAsync($"SetPinPage");
                    try
                    {
                        var statusLoc = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                        if (statusLoc != PermissionStatus.Granted)
                        {
                            statusLoc = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                            if (statusLoc != PermissionStatus.Granted)
                            {
                                Console.WriteLine("Отклонено в разрешении на доступ к локации");
                                await Application.Current.MainPage.DisplayAlert("Отказано в разрешении", "нету доступа к локации", "OK");

                                return;
                            }
                        }
                        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                        if (status != PermissionStatus.Granted)
                        {
                            status = await Permissions.RequestAsync<Permissions.Camera>();
                            if (status != PermissionStatus.Granted)
                            {
                                Console.WriteLine("Отклонено разрешение на использование Камеры");
                                await Application.Current.MainPage.DisplayAlert("Отказано в разрешении", "Отклонено разрешение на использование Камеры", "OK");
                                await Application.Current.MainPage.DisplayAlert("Включите разрешение", "Дайте доступ к камере", "OK");
                                return;
                            }
                        }
                        var status2 = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                        if (status2 != PermissionStatus.Granted)
                        {
                            status2 = await Permissions.RequestAsync<Permissions.StorageWrite>();
                            if (status2 != PermissionStatus.Granted)
                            {
                                Console.WriteLine("Отклонено Write External Storage");
                                await Application.Current.MainPage.DisplayAlert("Отказано в разрешении", "Отклонено разрешение на Хранилище", "OK");
                                await Application.Current.MainPage.DisplayAlert("Включите разрешение", "Дайте доступ к Хранилище", "OK");
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка входа", "Пользователь не найден или неверный пароль", "OK");
                    Console.WriteLine("Пользователь не найден или неверный пароль");
                }

            }
        }

        public async Task SaveSessionAsync(string username, string otNom, string fio, string otdel)
        {
            await SecureStorage.SetAsync("username", username);
            await SecureStorage.SetAsync("otNom", otNom);
            await SecureStorage.SetAsync("fio", fio);
            await SecureStorage.SetAsync("otdel", otdel);
            await SecureStorage.SetAsync("Latitude", "");
            await SecureStorage.SetAsync("Longitude", "");
            //await SecureStorage.SetAsync("pin", pin);
        }
    }
}
