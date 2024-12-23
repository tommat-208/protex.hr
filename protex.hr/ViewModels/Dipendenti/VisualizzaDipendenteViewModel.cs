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
    public partial class VisualizzaDipendenteViewModel : ObservableObject
    {
        [ObservableProperty]
        private Employee dipendente = new();

        [ObservableProperty]
        private ObservableCollection<Role> ruoli = new();

        [ObservableProperty]
        private string messaggio = "";


        private bool eliminato = false;

        public VisualizzaDipendenteViewModel(Employee e)
        {
            CaricaRuoli();

            this.Dipendente = new()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                HireDate = e.HireDate,
                Role = e.Role,
                Email = e.Email,
            };

            eliminato = false;
        }


        [RelayCommand]
        public void ModificaDipendenteClick()
        {
            if (!eliminato)
                ModificaDipendente();
            else
                Messaggio = "Il dipendente e' stato eliminato";
        }

        private async void ModificaDipendente()
        {
            try
            {
                var ris = await APIService.UpdateEmployee(Dipendente);

                if (ris)
                {
                    Messaggio = "Modifiche salvate";
                }
                else
                {
                    Messaggio = "Modifiche non salvate";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
        }



        [RelayCommand]
        public void EliminaDipendenteClick()
        {
            if (!eliminato)
                EliminaDipendente();
            else
                Messaggio = "Il dipendente e' gia' stato eliminato";
        }

        private async void EliminaDipendente()
        {
            try
            {
                var ris = await APIService.DeleteEmployee(Dipendente.Id);

                if (ris) 
                {
                    Messaggio = "Dipendente eliminato";
                    Dipendente = new();
                    eliminato = true;
                }
                else
                {
                    Messaggio = "Non e' stato possibile eliminare il dipendente";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
        }

        private async void CaricaRuoli()
        {
            try
            {
                var l = await APIService.GetAllRoles();

                if (l != null)
                {
                    Ruoli = new(l);
                }
                else
                {
                    Ruoli = new();
                    Messaggio = "Non e' stato possibile caricare i ruoli";
                }

            }catch(Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }

        }
    }
}
