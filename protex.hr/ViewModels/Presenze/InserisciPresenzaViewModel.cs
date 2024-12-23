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
    public partial class InserisciPresenzaViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Employee> listaDipendenti=new();
        [ObservableProperty]
        private AttendanceRecord presenza=new();
        [ObservableProperty]
        private string messaggio = "";


        public InserisciPresenzaViewModel()
        {
            AzzeraPresenza();

            CaricaListaDipendenti();
        }

        private void AzzeraPresenza()
        {
            Presenza = new() { Date = DateTime.Today };
        }

        [RelayCommand]
        public void InserisciPresenzaClick()
        {
            InserisciPresenza();
        }

        private async void InserisciPresenza()
        {
            try
            {
                var ris = await APIService.InsertAttendanceRecord(Presenza);

                if (ris)
                {
                    AzzeraPresenza();
                    Messaggio = "Presenza inserita";
                }
                else
                {
                    Messaggio = "Non e' stato possibile inserire la presenza";
                }
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
