using Entities;

namespace TesteTecnicoFIAP.Interface
{
    public interface IAlunoTurmaBLL
    {
        bool AddAlunoTurma(AlunoTurma alunoTurma);
        List<Aluno> GetAlunosByTurma(int turmaId);
        bool InativarAlunoTurma(int id);
        bool AtivarAlunoTurma(int id);
        bool DesvincularAlunoTurma(int alunoId, int turmaId);
    }
}
