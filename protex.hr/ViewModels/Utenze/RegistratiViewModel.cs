using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using protex.hr.Services;
using protex.hr.Views;
using protex.hr.Views.Utenze;
using protexhr.repository;

namespace protex.hr.ViewModels.Utenze
{
    public partial class RegistratiViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<UserType> listaTipi = new();
        [ObservableProperty]
        private UserPerson utente=new();
        [ObservableProperty]
        private string confermaPassword="";
        [ObservableProperty]
        private string messaggio = "";


        public RegistratiViewModel()
        {
            CaricaListaTipi();
            AzzeraUtente();
        }
        
        private void AzzeraUtente()
        {
            Utente = new() { PasswordHash = "" };
            ConfermaPassword = "";
        }

        [RelayCommand]
        public void RegistratiClick()
        {
            string app = "";

            if (!Utente.PasswordHash.Equals(ConfermaPassword))
                app = "Le password non combaciano";

            Messaggio = app;

            if (!string.IsNullOrWhiteSpace(app))
                return;

            Registrati();
        }


        private async void Registrati()
        {
            try
            {
                var ris = await APILogin.InsertUserPerson(Utente);

                if (ris)
                {
                    Messaggio = "Utente registrato. Esegui il login per entrare nel programma.";
                    AzzeraUtente();
                    GotoLogin("Utente registrato. Esegui il login per entrare nel programma");
                }
                else
                {
                    Messaggio = "Utente non inserito.";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
        }


        private async void CaricaListaTipi()
        {
            try
            {
                var l = await APILogin.GetAllUserType();

                if (l != null)
                {
                    ListaTipi = new(l);
                }
                else
                {
                    Messaggio = "Non e' stato possibile caricare la lista dei tipi";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore durante il caricamento dei tipi:\n" + ex.Message;
            }
        }

        private void GotoLogin(string s)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage(s));
        }



    }
}
