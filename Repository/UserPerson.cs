using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace protexhr.repository
{
    public class UserPerson
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Specificare username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Specificare email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Specificare la password")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Specificare il tipo")]
        public UserType Type { get; set; }




    }
}
