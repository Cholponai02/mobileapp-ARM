using MauiApp1.Views;
namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutingPages();
        }
        private void RegisterRoutingPages()
        {
            Routing.RegisterRoute("SetPinPage", typeof(SetPinPage));
            Routing.RegisterRoute("PinVerificationPage", typeof(PinVerificationPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegistPage", typeof(RegistPage));
            //Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }
    }
}
