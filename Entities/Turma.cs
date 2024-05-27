using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int NumeroAlunos { get; set; }
        public ICollection<AlunoTurma> AlunoTurmas { get; set; }
    }
}
