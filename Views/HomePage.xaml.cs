using MauiApp1.Models;
using Newtonsoft.Json;
using MauiApp1.Views;
using System.Windows.Input;
using MauiApp1.Constants;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Views;

public partial class HomePage : ContentPage, INotifyPropertyChanged
{
    public ICommand FrameTappedCommand { get; }
    private ObservableCollection<Zavkr> _zavkrList;
    private bool _isPasswordVisible = true;

    public ObservableCollection<Zavkr> ZavkrList
    {
        get => _zavkrList;
        set
        {
            _zavkrList = value;
            OnPropertyChanged();
        }
    }

    private long _savedNumber;
    public HomePage(User user)
	{
		InitializeComponent();
        ZavkrList = new ObservableCollection<Zavkr>();
        BindingContext = this;
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            string departmentId = await SecureStorage.Default.GetAsync("otdel");
            // string ot_nom = "1049";
            using var httpClient = new HttpClient();
            string url = ServerConstants.SERVER_ROOT_URL + "api/LoanReference/GetReferencesOfDepartment?departmentId=" + departmentId;
            //string url = ServerConstants.SERVER_ROOT_URL + "api/LoanReference/GetReferencesOfEmployee?otNom=" + ot_nom;

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ZavkrList.Clear();
                var dataList = JsonConvert.DeserializeObject<List<Zavkr>>(responseData);
                //BindingContext = dataList;

                //var top3Data = dataList.Take(2).ToList();
                //BindingContext = top3Data;
                foreach (var item in dataList.Take(10).OrderByDescending(s => s.PositionalNumber)) 
                {
                    ZavkrList.Add(item);
                }
                OnPropertyChanged(nameof(ZavkrList));

            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
    private async void OnFrameTapped(object sender, EventArgs e)
    {
        var selectedZavkr = ((sender as StackLayout)?.BindingContext as Zavkr);
        if (selectedZavkr != null)
        {
            await Navigation.PushAsync(new ZavkrPage(selectedZavkr));
        }
    }

    private void OnSaveNumberClicked(object sender, EventArgs e)
    {
        if (long.TryParse(numberEntry.Text, out long number))
        {
            _savedNumber = number;
            GetDataFromFilter(_savedNumber);
        }
        else
        {
            DisplayAlert("Ошибка", "Введите числа!", "OK");
        }
    }

    private async void GetDataFromFilter(long pozn)
    {
        try
        {
            // string departmentId = UserData.CurrentUser.DepartmentId.ToString();
            using var httpClient = new HttpClient();
            string url = ServerConstants.SERVER_ROOT_URL  + "api/LoanReference/GetOneZalog?pozn=" + pozn;

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ZavkrList.Clear();

                var dataList = JsonConvert.DeserializeObject<List<Zavkr>>(responseData);
                foreach (var item in dataList)
                {
                    ZavkrList.Add(item);

                }
                if(dataList.Count > 0)
                {
                    DisplayAlert("Успешно", "Заявка найдена", "OK");
                }
                OnPropertyChanged(nameof(ZavkrList));
                
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
    private void ClearFilter(object sender, EventArgs e)
    {
        LoadData();
        numberEntry.Text = string.Empty;
        ZavkrList.Clear();
    }
    private void HideProperty(object sender, EventArgs e)
    {
        if (ZavkrList.Count > 0)
        {
            bool hide = ZavkrList.All(z => !z.IsSecretHidden);
            foreach (var zavkr in ZavkrList)
            {
                zavkr.IsSecretHidden = hide;
            }
            _isPasswordVisible = !_isPasswordVisible;
            UpdatePasswordVisibility();
        }
    }
    private void UpdatePasswordVisibility()
    {
        if (_isPasswordVisible)
        {
            showPasswordIcon.Source = "hide_icon.png";
        }
        else
        {
            showPasswordIcon.Source = "show_icon.png";
        }
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
   
}