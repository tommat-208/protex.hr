using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AttendanceRecord
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Specificare il dipendente")]
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Specificare la data")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Specificare l'orario di check in")]
        public TimeSpan CheckInTime { get; set; }

        [Required(ErrorMessage = "Specificare l'orario di check out")]
        public TimeSpan CheckOutTime { get; set; }




    }
}
