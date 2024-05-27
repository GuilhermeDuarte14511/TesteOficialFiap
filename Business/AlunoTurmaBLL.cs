using Dapper;
using Entities;
using System.Data.SqlClient;
using TesteTecnicoFIAP.Interface;

namespace Business
{
    public class AlunoTurmaBLL : IAlunoTurmaBLL
    {
        private readonly string _connectionString;

        public AlunoTurmaBLL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddAlunoTurma(AlunoTurma alunoTurma)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var queryCheck = "SELECT COUNT(1) FROM AlunoTurmas WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
                    var alreadyExists = connection.ExecuteScalar<bool>(queryCheck, new { alunoTurma.AlunoId, alunoTurma.TurmaId });

                    if (alreadyExists)
                    {
                        return false;
                    }

                    var queryInsert = "INSERT INTO AlunoTurmas (AlunoId, TurmaId) VALUES (@AlunoId, @TurmaId)";
                    connection.Execute(queryInsert, new { alunoTurma.AlunoId, alunoTurma.TurmaId });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<Aluno> GetAlunosByTurma(int turmaId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    SELECT a.*
                    FROM AlunoTurmas at
                    INNER JOIN Alunos a ON at.AlunoId = a.Id
                    WHERE at.TurmaId = @TurmaId";
                return connection.Query<Aluno>(query, new { TurmaId = turmaId }).ToList();
            }
        }

        public bool InativarAlunoTurma(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Turmas SET Ativo = 0 WHERE Id = @Id";
                    connection.Execute(query, new { Id = id });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool AtivarAlunoTurma(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Turmas SET Ativo = 1 WHERE Id = @Id";
                    connection.Execute(query, new { Id = id });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DesvincularAlunoTurma(int alunoId, int turmaId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "DELETE FROM AlunoTurmas WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
                    connection.Execute(query, new { AlunoId = alunoId, TurmaId = turmaId });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VincularAlunoTurma(AlunoTurma alunoTurma)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "INSERT INTO AlunoTurmas (AlunoId, TurmaId) VALUES (@AlunoId, @TurmaId)";
                    connection.Execute(query, new { alunoTurma.AlunoId, alunoTurma.TurmaId });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
