using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using protex.hr.Services;
using Repository;

namespace protex.hr.ViewModels.Dipendenti
{
    public partial class InserisciDipendenteViewModel : ObservableObject
    {
        [ObservableProperty]
        private Employee dipendente=new();
        [ObservableProperty]
        private ObservableCollection<Role> ruoli = new();
        [ObservableProperty]
        private string messaggio = "";


        public InserisciDipendenteViewModel()
        {
            CaricaElencoRuoli();
            AzzeraDipendente();
        }

        private void AzzeraDipendente()
        {
            Employee app = new() { HireDate = DateTime.Now };

            Dipendente = app;
        }

        [RelayCommand]
        public void InserisciDipendenteClick()
        {
            InserisciDipendente();
        }


        private async void InserisciDipendente()
        {
            try
            {
                var ris = await APIService.InsertEmployee(Dipendente);

                if (ris)
                {
                    Messaggio = "Dipendente aggiunto";
                    AzzeraDipendente();
                }
                else
                {
                    Messaggio = "Non e' stato possibile aggiungere il dipendente";
                }

            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
            
        }


        private async void CaricaElencoRuoli()
        {
            try
            {
                var l = await APIService.GetAllRoles();

                Ruoli = new(l);
            }
            catch (Exception ex)
            {
                Messaggio = "Errore durante il caricamento dei ruoli:\n" + ex.Message;
            }

        }

    }
}
