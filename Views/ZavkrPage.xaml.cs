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