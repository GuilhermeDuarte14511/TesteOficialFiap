using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AlunoTurmaDto
    {
        [Required]
        public int AlunoId { get; set; }

        [Required]
        public int TurmaId { get; set; }
    }
}
