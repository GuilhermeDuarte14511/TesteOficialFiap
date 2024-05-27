using Entities;

namespace TesteTecnicoFIAP.Interface
{
    public interface IAlunoBLL
    {
        (bool isSuccess, string message) AddAluno(Aluno aluno);
        List<Aluno> GetAllAlunos();
        Aluno GetAlunoById(int id);
        bool EditAluno(int id, Aluno aluno);
        bool DeleteAluno(int id);
        bool InativarAluno(int id);
        bool AtivarAluno(int id);
    }

}
