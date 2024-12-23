using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using protexhr.repository;

namespace protex.hr.Services
{
    public static class Sessione
    {
        public static UserPerson Utente { get; private set; }
        public static string Username { get => Utente.Username; }

        public static bool Autorizzato { 
            get {
                if(Utente != null && Utente.Type.Authorized)
                    return true;
                return false;
            } }

        // Messaggio errore
        private const string PermessiMancantiMsg = "Non disponi dei permessi necessari per accedere a quest'area.";




        public static void IniziaSessione(UserPerson u)
        {
            Utente = u;
        }

        public static void ChiudiSessione()
        {
            Utente = null;
        }


        public static void CheckPermessi(Page p, Page destinazione)
        {
            if (Autorizzato)
            {
                p.Navigation.PushAsync(destinazione);
            }
            else
            {
                p.DisplayAlert("Permessi mancanti", PermessiMancantiMsg, "Ok");
            }
        }

    }
}
