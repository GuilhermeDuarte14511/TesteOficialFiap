using Dapper;
using Entities;
using System.Data.SqlClient;
using TesteTecnicoFIAP.Interface;

namespace Business
{
    public class TurmaBLL : ITurmaBLL
    {
        private readonly string _connectionString;

        public TurmaBLL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddTurma(Turma turma)
        {
            if (TurmaExists(turma.Nome)) return false;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "INSERT INTO Turmas (Nome, Ativo) VALUES (@Nome, @Ativo)";
                    connection.Execute(query, new { turma.Nome, turma.Ativo });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditTurma(int id, Turma turma)
        {
            if (TurmaExists(turma.Nome, id)) return false;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Turmas SET Nome = @Nome, Ativo = @Ativo WHERE Id = @Id";
                    connection.Execute(query, new { turma.Nome, turma.Ativo, Id = id });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Turma> GetAllTurmas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    SELECT t.Id, t.Nome, t.Ativo, COUNT(at.AlunoId) AS NumeroAlunos
                    FROM Turmas t
                    LEFT JOIN AlunoTurmas at ON t.Id = at.TurmaId
                    GROUP BY t.Id, t.Nome, t.Ativo";
                return connection.Query<Turma>(query).ToList();
            }
        }

        public Turma GetTurmaById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                        var query = @"
                    SELECT t.Id, t.Nome, t.Ativo, COUNT(at.AlunoId) AS NumeroAlunos
                    FROM Turmas t
                    LEFT JOIN AlunoTurmas at ON t.Id = at.TurmaId
                    WHERE t.Id = @Id
                    GROUP BY t.Id, t.Nome, t.Ativo";

                return connection.QuerySingleOrDefault<Turma>(query, new { Id = id });
            }
        }


        public bool InativarTurma(int id)
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

        private bool TurmaExists(string nome, int? id = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = id.HasValue
                    ? "SELECT COUNT(1) FROM Turmas WHERE LOWER(Nome) = LOWER(@Nome) AND Id != @Id"
                    : "SELECT COUNT(1) FROM Turmas WHERE LOWER(Nome) = LOWER(@Nome)";
                var count = connection.ExecuteScalar<int>(query, new { Nome = nome, Id = id });
                return count > 0;
            }
        }


        public bool AtivarTurma(int id)
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


    }
}
