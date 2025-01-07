using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using protex.hr.Services;
using protex.hr.Views.Dipendenti;
using Repository;

namespace protex.hr.ViewModels.Dipendenti
{
    public partial class ListaDipendentiViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Employee> listaDipendenti=new();
        [ObservableProperty]
        private Employee dipendenteSelezionato = null;
        [ObservableProperty]
        private string messaggio = "";


        public ListaDipendentiViewModel()
        {
            CaricaListaDipendenti();

        }

        [RelayCommand]
        public void VisualizzaDipendente()
        {
            if(DipendenteSelezionato!=null)
            {
                GotoVisualizzaDipendente();
            }
            else
            {
                Messaggio = "Nessun dipendente selezionato";
            }
        }

        private async void GotoVisualizzaDipendente()
        {
            await App.Current.MainPage.Navigation.PushAsync(new VisualizzaDipendentePage(DipendenteSelezionato));
        }

        [RelayCommand]
        public void AggiornaListaDipendenti()
        {
            CaricaListaDipendenti();
        }


        private async void CaricaListaDipendenti()
        {
            try
            {
                var l = await APIService.GetAllEmployees();

                if (l != null) {
                    ListaDipendenti = new(l);
                    Messaggio = $"Lista dipendenti caricata";
                }
                else
                {
                    Messaggio = "Non e' stato possibile caricare la lista";
                }
                
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n"+ex.Message;
            }

        }

    }
}
