using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Role
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Specificare il nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specificare la descrizione")]
        public string Description { get; set; }


    }
}
