using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using protex.hr.Services;

namespace protex.hr.ViewModels.Utenze
{
    public partial class ChangelogViewModel : ObservableObject
    {
        [ObservableProperty]
        private string changes = "";


        public ChangelogViewModel()
        {
            GetChangelog();
        }
        
        private async void GetChangelog()
        {
            try
            {
                var app = await APIGithub.GetChangelog();

                if (string.IsNullOrEmpty(app))
                    Changes = "Nessun changelog disponibile.";
                else
                    Changes = app;
            }
            catch (Exception ex)
            {
                Changes = "Errore:\n" + ex.Message;
            }
        }


    }
}
