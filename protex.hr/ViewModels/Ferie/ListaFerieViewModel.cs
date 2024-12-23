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
    public partial class ListaFerieViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<LeaveRequest> listaRichieste=new();
        [ObservableProperty]
        private LeaveRequest richiestaSelezionata;
        [ObservableProperty]
        private string messaggio = "";


        public ListaFerieViewModel()
        {
            CaricaListaRichieste();


        }

        [RelayCommand]
        public void AggiornaListaClick()
        {
            CaricaListaRichieste();
        }


        [RelayCommand]
        public void RifiutaRichiestaClick()
        {
            if (RichiestaSelezionata == null)
            {
                Messaggio = "Nessuna richiesta selezionata";
                return;
            }

            RifiutaRichiesta();
        }

        private async void RifiutaRichiesta()
        {
            try
            {
                var ris = await APIService.RejectLeaveRequest(RichiestaSelezionata);

                if (ris)
                {
                    Messaggio = "Richiesta rifiutata";
                    CaricaListaRichieste();
                }
                else
                {
                    Messaggio = "Non e' stato possibile rifiutare la richiesta";
                }
            }
            catch (Exception ex) 
            {
                Messaggio = "Errore:\n" + ex.Message;
            }

        }

        [RelayCommand]
        public void AccettaRichiestaClick()
        {
            if (RichiestaSelezionata == null)
            {
                Messaggio = "Nessuna richiesta selezionata";
                return;
            }

            AccettaRichiesta();
        }

        private async void AccettaRichiesta()
        {
            try
            {
                var ris = await APIService.ApproveLeaveRequest(RichiestaSelezionata);

                if (ris)
                {
                    Messaggio = "Richiesta accettata";
                    CaricaListaRichieste();
                }
                else
                {
                    Messaggio = "Non e' stato possibile rifiutare la richiesta";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }

        }


        private async void CaricaListaRichieste()
        {
            try
            {
                var l = await APIService.GetPendingLeaveRequests();

                if (l != null)
                {
                    ListaRichieste = new(l);
                }
                else
                {
                    Messaggio = "Non e' stato possibile caricare le richieste";
                    ListaRichieste = new();
                }
            }
            catch(Exception ex)
            {
                Messaggio = "Errore:\n"+ex.Message;
                ListaRichieste = new();
            }
        }



    }
}
