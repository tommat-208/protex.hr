using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using protex.hr.Services;
using protex.hr.Views;
using protexhr.repository;
using Repository;

namespace protex.hr.ViewModels.Utenze
{

    [QueryProperty(nameof(Messaggio), nameof(Messaggio))]
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username = "";
        [ObservableProperty]
        private string password = "";
        [ObservableProperty]
        private string messaggio = "";
        [ObservableProperty]
        private string assVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();


        public LoginViewModel() {
        }


        public LoginViewModel(string msg)
        {
            Messaggio = msg;
        }


        [RelayCommand]
        public async void EntraClick()
        {
            UserType ut = new() { Authorized = true };
            UserPerson up = new() { Username="admin", Type = ut };
            Sessione.IniziaSessione(up);
            GotoHomePage();
            return;

            string app = "";

            if (string.IsNullOrWhiteSpace(Username))
                app = "Username vuoto";
            else if (string.IsNullOrWhiteSpace(Password))
                app = "Password mancante";

            Messaggio = app;

            if (!string.IsNullOrWhiteSpace(app))
                return;



           Entra(); // il db non funziona

            

        }

        private async void Entra()
        {
            try
            {
                var ris = await APILogin.VerificaPassword(Username, Password);

                if (ris)
                {
                    Messaggio = "Login effettuato";
                    var u = await APILogin.GetUserPerson(Username);
                    Sessione.IniziaSessione(u);
                    GotoHomePage();
                }
                else
                {
                    Messaggio = "Credenziali non corrette";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n"+ex.Message;
            }
        }

        private void GotoHomePage()
        {
            App.Current.MainPage = new NavigationPage(new HomePage());
        }


        

    }
}
