using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using protex.hr.Services;
using Repository;

namespace protex.hr.ViewModels.Ruoli
{
    public partial class ModificaRuoloViewModel : ObservableObject
    {
        [ObservableProperty]
        private Role ruolo;
        [ObservableProperty]
        private string messaggio="";


        public ModificaRuoloViewModel(Role ruolo)
        {
            this.Ruolo = ruolo;
        }


        [RelayCommand]
        public void ModificaRuoloClick()
        {
            ModificaRuolo();
        }

        private async void ModificaRuolo()
        {
            try
            {
                var ris = await APIService.UpdateRole(Ruolo);

                if (ris)
                    Messaggio = "Ruolo modificato";
                else
                    Messaggio = "Ruolo non modificato";
            }
            catch (Exception ex)
            {
                Messaggio = "Errore:\n"+ex.Message;
            }

        }
    }
}
