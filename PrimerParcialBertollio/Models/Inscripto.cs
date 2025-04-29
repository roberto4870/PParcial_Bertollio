using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrimerParcialBertollio.Models
{
    public class Inscripto
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int IdDisciplina { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "El valor ingresado debe ser mayor o igual a 0")]
        public int Edad { get; set; }
        [Required]
        [DisplayName("Ciudad de residencia")]
        public string CiudadResidencia { get; set; }

        public Disciplina? Disciplina { get; set; }
    }
}
