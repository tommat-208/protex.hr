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

namespace protex.hr.ViewModels.Ferie
{
    public partial class InserisciRichiestaViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Employee> listaDipendenti=new();
        [ObservableProperty]
        private LeaveRequest richiesta=new();
        [ObservableProperty]
        private string messaggio = "";

        
        public InserisciRichiestaViewModel()
        {
            CaricaListaDipendenti();
            AzzeraRichiesta();
        }

        private void AzzeraRichiesta()
        {
            Richiesta = new LeaveRequest
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

        }

        [RelayCommand]
        public void InserisciRichiestaClick()
        {
            InserisciRichiesta();
        }

        private async void InserisciRichiesta()
        {
            try
            {
                var ris = await APIService.InsertLeaveRequest(Richiesta);

                if (ris)
                {
                    Messaggio = "Richiesta di ferie inserita";
                    AzzeraRichiesta();
                }
                else
                    Messaggio = "Non e' stato possibile inserire la richiesta";

            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
        }


        private async void CaricaListaDipendenti()
        {
            try
            {
                var l = await APIService.GetAllEmployees();

                if (l != null)
                {
                    ListaDipendenti = new(l);
                }
                else
                {
                    Messaggio = "Non e' stato possibile caricare la lista dei dipendenti";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
        }

    }
}
