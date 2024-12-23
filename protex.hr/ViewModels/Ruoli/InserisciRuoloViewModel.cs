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
    public partial class InserisciRuoloViewModel : ObservableObject
    {
        [ObservableProperty]
        private Role role=new();
        [ObservableProperty]
        private string messaggio="";


        [RelayCommand]
        public void InserisciRuoloClick()
        {
            InserisciRuolo();
        }


        public async void InserisciRuolo()
        {
            string msg = "";

            try
            {
                var ris = await APIService.InsertRole(this.Role);

                if (ris)
                {
                    msg = "Ruolo inserito";
                    this.Role = new();
                }
                else
                {
                    msg = "Non e' stato possibile inserire il ruolo";
                }
            } catch (Exception ex) {
                msg = $"Errore:\n{ex.Message}";
            }

            Messaggio = msg;
        }




    }
}
