using System.ComponentModel.DataAnnotations;

namespace Repository
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Specificare il nome")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Il nome deve essere compreso tra 1 e 50 caratteri")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Specificare il cognome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Specificare il ruolo")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Specificare la data di assunzione")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Specificare l'email")]
        public string Email { get; set; }

        public string IdCognomeNome { get { return $"{Id} {LastName} {FirstName}"; } }

    }
}
