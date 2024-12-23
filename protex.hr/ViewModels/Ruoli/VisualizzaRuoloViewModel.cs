using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using protex.hr.Services;
using protex.hr.Views.Ruoli;
using Repository;

namespace protex.hr.ViewModels.Ruoli
{
    public partial class VisualizzaRuoloViewModel : ObservableObject
    {
        [ObservableProperty]
        private string filtroId = "";
        [ObservableProperty]
        private string messaggio = "";
        [ObservableProperty]
        private Role ruolo = new();

        private bool selezionato = false;



        public VisualizzaRuoloViewModel()
        {
            Azzera();
        }

        public VisualizzaRuoloViewModel(Role ruolo)
        {
            SelezionaRuolo(ruolo);
        }

        [RelayCommand]
        public void CercaRuoloClick()
        {
            var app = "";
            Console.WriteLine(FiltroId);

            // Controlli dell'input
            if (string.IsNullOrWhiteSpace(FiltroId))
                app = "Il filtro di ricerca e' vuoto";

            Messaggio = app;

            if (!string.IsNullOrEmpty(app))
                return;


            CercaRuolo();
        }


        private async void CercaRuolo()
        {
            try
            {
                var r = await APIService.GetRole(Convert.ToInt32(FiltroId));

                if (r != null)
                {
                    SelezionaRuolo(r);
                    Messaggio = "Ruolo trovato";
                }
                else
                {
                    selezionato = false;
                    Messaggio = "Ruolo non trovato";
                }


            }
            catch (Exception ex)
            {
                Messaggio = "Errore durante la ricerca:\n" + ex.Message;
            }

        }

        [RelayCommand]
        public void ModificaRuoloClick()
        {
            if (!selezionato)
            {
                Messaggio = "Nessun ruolo selezionato";
            }
            else
            {
                GotoModifica();
                Azzera();
            }
        }

        private async void GotoModifica()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ModificaRuoloPage(Ruolo));
        }


        [RelayCommand]
        public void EliminaRuoloClick()
        {
            if (!selezionato)
            {
                Messaggio = "Nessun ruolo selezionato";
            }
            else
            {
                EliminaRuolo();
            }
        }

        private async void EliminaRuolo()
        {
            try
            {
                var ris = await APIService.DeleteRole(Ruolo.Id);

                if (ris)
                {
                    Messaggio = "Ruolo eliminato";
                    Azzera();
                }
                else
                {
                    Messaggio = "Non e' stato possibile eliminare il ruolo";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore: "+ex.Message;
            }

        }

        private void SelezionaRuolo(Role r)
        {
            Ruolo = r;
            selezionato = true;
        }

        private void Azzera()
        {
            Ruolo = new();
            selezionato = false;
        }




    }
}
