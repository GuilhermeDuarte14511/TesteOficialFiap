using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AlunoTurma
    {
        public int Id { get; set; }

        [Required]
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } // Pode ser null durante a associação inicial

        [Required]
        public int TurmaId { get; set; }
        public Turma Turma { get; set; } // Pode ser null durante a associação inicial

        public string TurmaNome { get; set; } // Não é necessário quando criando a associação
    }
}
