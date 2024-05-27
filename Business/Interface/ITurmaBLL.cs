using Entities;

namespace TesteTecnicoFIAP.Interface
{
    public interface ITurmaBLL
    {
        List<Turma> GetAllTurmas();
        Turma GetTurmaById(int id);
        bool AddTurma(Turma turma);
        bool EditTurma(int id, Turma turma);
        bool InativarTurma(int id);
        bool AtivarTurma(int id);
    }

}
