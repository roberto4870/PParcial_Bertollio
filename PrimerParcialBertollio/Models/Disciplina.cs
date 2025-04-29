using System.ComponentModel.DataAnnotations;

namespace PrimerParcialBertollio.Models
{
    public class Disciplina
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreDisciplina { get; set; }
    }
}
