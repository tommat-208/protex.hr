using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using protex.hr.Services;
using protex.hr.Views;
using protex.hr.Views.Utenze;

namespace protex.hr.ViewModels.Utenze
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string vecchiaPw = "";
        [ObservableProperty]
        private string nuovaPw = "";
        [ObservableProperty]
        private string confermaPw = "";
        [ObservableProperty]
        private string messaggio = "";
        [ObservableProperty]
        private string assVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();


        public SettingsViewModel()
        {

        }


        [RelayCommand]
        public void EliminaClick()
        {
            Elimina();
        }

        private async void Elimina()
        {
            try
            {
                var ris = await APILogin.DeleteUserPerson(Sessione.Username);

                if (ris)
                {
                    Messaggio = "Utente eliminato";
                    Logout("Utente eliminato. Esegui il login");
                }
                else
                    Messaggio = "Utente non eliminato";

            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
        }



        [RelayCommand]
        public void CambiaPwClick()
        {
            var app = "";

            if (string.IsNullOrWhiteSpace(VecchiaPw) ||
                string.IsNullOrWhiteSpace(NuovaPw) ||
                string.IsNullOrWhiteSpace(ConfermaPw))
                app = "Compila tutti i campi";
            else if (!NuovaPw.Equals(ConfermaPw))
                app = "Le password non combaciano";

            Messaggio = app;

            if (!string.IsNullOrEmpty(app))
                return;


            CambiaPw();
        }

        private async void CambiaPw()
        {
            try
            {
                Console.WriteLine($"ViewModel.CambiaPw : Sessione.Utente.Username={Sessione.Utente.Username}, vecchiapw={VecchiaPw}, nuovapw={NuovaPw}");

                var ris = await APILogin.CambiaPassword(Sessione.Utente.Username, VecchiaPw, NuovaPw);

                if (ris)
                {
                    Messaggio = "Password cambiata";
                    Logout("Password cambiata. Esegui di nuovo il login");
                }
                else
                {
                    Messaggio = "Password non cambiata";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n"+ex.Message;
            }
        }



        [RelayCommand]
        public void LogoutClick()
        {
            Logout();
        }

        private void Logout()
        {
            Logout("");
        }

        private void Logout(string s)
        {
            Sessione.ChiudiSessione();
            App.Current.MainPage = new NavigationPage(new LoginPage(s));
        }







    }
}
