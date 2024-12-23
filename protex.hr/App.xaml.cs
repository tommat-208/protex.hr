using protex.hr.Views;
using protex.hr.Views.Presenze;
using protex.hr.Views.Utenze;

namespace protex.hr
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            
            
            //RoutePresenze();
            //RouteUtenze();
            
            //MainPage = new AppShell();

        }

        private void RouteUtenze()
        { // Login page in <ShellContent>
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(RegistratiPage), typeof(RegistratiPage));
        }

        private void RoutePresenze()
        {
            Routing.RegisterRoute(nameof(GestionePresenzePage), typeof(GestionePresenzePage));
            Routing.RegisterRoute(nameof(InserisciPresenzaPage), typeof(InserisciPresenzaPage));
            Routing.RegisterRoute(nameof(StoricoPresenzePage), typeof(StoricoPresenzePage));
        }
    }
}
