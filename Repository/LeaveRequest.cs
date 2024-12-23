using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Specificare il dipendente")]
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Specificare la data di inizio ferie")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Specificare la data di fine ferie")]
        public DateTime EndDate { get; set; }

        [Required]
        public LeaveRequestStatus Status { get; set; }

    }
}
