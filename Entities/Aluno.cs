using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Senha deve ter pelo menos 8 caracteres.")]
        public string Senha { get; set; }  
        
        public string TurmaNome { get; set; }

        public bool Ativo { get; set; }

        public ICollection<AlunoTurma> AlunoTurmas { get; set; }
    }
}
