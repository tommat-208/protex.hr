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

namespace protex.hr.ViewModels.Presenze
{
    public partial class StoricoPresenzeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Employee> listaDipendenti = new();
        [ObservableProperty]
        private Employee dipendenteSelezionato = new();
        [ObservableProperty]
        private ObservableCollection<AttendanceRecord> storicoPresenze = new();
        [ObservableProperty]
        private string messaggio = "";


        public StoricoPresenzeViewModel()
        {
            CaricaListaDipendenti();
        }

        [RelayCommand]
        public void CaricaStoricoClick()
        {
            string app = "";

            if (DipendenteSelezionato == null)
                app = "Nessun dipendente selezionato";

            Messaggio = app;

            if (!string.IsNullOrEmpty(app))
                return;

            CaricaStorico();
        }

        private async void CaricaStorico()
        {
            try
            {
                var l = await APIService.GetAttendaceByEmployeeId(DipendenteSelezionato.Id);

                if(l != null)
                {
                    StoricoPresenze = new(l);
                    Messaggio = $"{l.Count} risultati";
                }
                else
                {
                    Messaggio = "Non e' stato possible caricare lo storico";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Erorre:\n" + ex.Message;
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
