using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AlunoTurma
    {
        public int Id { get; set; }

        [Required]
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        [Required]
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public string TurmaNome
        {
            get { return Turma?.Nome; }
            set
            {
                if (Turma != null)
                {
                    Turma.Nome = value;
                }
                else
                {
                    Turma = new Turma { Nome = value };
                }
            }
        }
    }
}
