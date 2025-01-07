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

namespace protex.hr.ViewModels.Ruoli
{
    
    public partial class ListaRuoliViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Role> listaRuoli=new();
        [ObservableProperty]
        private string messaggio = "";

        public ListaRuoliViewModel()
        {
            GetAllRuoli();
        }

        [RelayCommand]
        public void RicaricaLista()
        {
            GetAllRuoli();
        }


        private async void GetAllRuoli()
        {
            try
            {
                var l = await APIService.GetAllRoles();

                if (l != null)
                {
                    ListaRuoli = new ObservableCollection<Role>(l);
                    Messaggio = $"Elenco ruoli caricato";
                }
                else
                {
                    ListaRuoli = new();
                    Messaggio = "Errore";
                }
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n" + ex.Message;
            }
        }



    }
}
