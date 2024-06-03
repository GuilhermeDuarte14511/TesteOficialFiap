using Dapper;
using Entities;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using TesteTecnicoFIAP.Interface;

namespace Business
{
    public class AlunoBLL : IAlunoBLL
    {
        private readonly string _connectionString;

        public AlunoBLL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public (bool isSuccess, string message) AddAluno(Aluno aluno)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var existingAluno = connection.QuerySingleOrDefault<Aluno>(
                                "SELECT Id FROM Alunos WHERE Email = @Email",
                                new { aluno.Email },
                                transaction: transaction
                            );

                            if (existingAluno != null)
                            {
                                return (false, "Erro ao cadastrar aluno. Já existe um aluno cadastrado com este e-mail.");
                            }

                            if (!IsPasswordStrong(aluno.Senha))
                            {
                                return (false, "Erro ao cadastrar aluno. A senha é muito fraca.");
                            }

                            var senhaHash = HashPassword(aluno.Senha);

                            // Inserir o aluno e recuperar o ID gerado
                            var query = @"
                                INSERT INTO Alunos (Nome, Email, SenhaHash, Ativo)
                                OUTPUT INSERTED.Id
                                VALUES (@Nome, @Email, @SenhaHash, @Ativo)";
                            var alunoId = connection.QuerySingle<int>(
                                query,
                                new { aluno.Nome, aluno.Email, SenhaHash = senhaHash, aluno.Ativo },
                                transaction: transaction
                            );

                            var turmaIds = new HashSet<int>();
                            foreach (var turma in aluno.AlunoTurmas)
                            {
                                if (turmaIds.Contains(turma.TurmaId))
                                {
                                    return (false, "Erro ao cadastrar aluno. Turma duplicada encontrada.");
                                }
                                turmaIds.Add(turma.TurmaId);

                                var insertQuery = "INSERT INTO AlunoTurmas (AlunoId, TurmaId) VALUES (@AlunoId, @TurmaId)";
                                connection.Execute(
                                    insertQuery,
                                    new { AlunoId = alunoId, TurmaId = turma.TurmaId },
                                    transaction: transaction
                                );
                            }

                            transaction.Commit();
                            return (true, "Aluno cadastrado com sucesso.");
                        }
                        catch (Exception ex)
                        {
                            // Rollback da transação se qualquer operação falhar
                            transaction.Rollback();
                            return (false, $"Erro ao cadastrar aluno: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao cadastrar aluno: {ex.Message}");
            }
        }

        public List<Aluno> GetAllAlunos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                            SELECT a.Id, a.Nome, a.Email, a.SenhaHash, a.Ativo, t.Nome AS TurmaNome
                            FROM Alunos a
                            LEFT JOIN AlunoTurmas at ON a.Id = at.AlunoId
                            LEFT JOIN Turmas t ON at.TurmaId = t.Id";

                var alunoDictionary = new Dictionary<int, Aluno>();

                var alunos = connection.Query<Aluno, string, Aluno>(query, (aluno, turmaNome) =>
                {
                    if (!alunoDictionary.TryGetValue(aluno.Id, out var alunoEntry))
                    {
                        alunoEntry = aluno;
                        alunoEntry.TurmaNome = turmaNome;
                        alunoDictionary.Add(aluno.Id, alunoEntry);
                    }
                    else
                    {
                        alunoEntry.TurmaNome += ", " + turmaNome;
                    }

                    return alunoEntry;
                }, splitOn: "TurmaNome").Distinct().ToList();

                return alunos;
            }
        }





        public Aluno GetAlunoById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                            SELECT a.*, t.Id AS TurmaId, t.Nome AS TurmaNome
                            FROM Alunos a
                            LEFT JOIN AlunoTurmas at ON a.Id = at.AlunoId
                            LEFT JOIN Turmas t ON at.TurmaId = t.Id
                            WHERE a.Id = @Id";

                var alunoDictionary = new Dictionary<int, Aluno>();

                var alunos = connection.Query<Aluno, AlunoTurma, Aluno>(query, (aluno, alunoTurma) =>
                {
                    if (!alunoDictionary.TryGetValue(aluno.Id, out var alunoEntry))
                    {
                        alunoEntry = aluno;
                        alunoEntry.AlunoTurmas = new List<AlunoTurma>();
                        alunoDictionary.Add(aluno.Id, alunoEntry);
                    }

                    if (alunoTurma != null)
                    {
                        var turma = new Turma
                        {
                            Id = alunoTurma.TurmaId,
                            Nome = alunoTurma.TurmaNome
                        };

                        alunoTurma.Turma = turma;
                        alunoEntry.AlunoTurmas.Add(alunoTurma);
                    }

                    return alunoEntry;
                }, new { Id = id }, splitOn: "TurmaId").Distinct().ToList();

                return alunos.FirstOrDefault();
            }
        }


        public bool EditAluno(int id, Aluno aluno)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var query = "UPDATE Alunos SET Nome = @Nome, Email = @Email, Ativo = @Ativo WHERE Id = @Id";
                            connection.Execute(query, new { aluno.Nome, aluno.Email, aluno.Ativo, Id = aluno.Id }, transaction: transaction);

                            var turmaQuery = "SELECT TurmaId FROM AlunoTurmas WHERE AlunoId = @AlunoId";
                            var parameters = new { AlunoId = aluno.Id };

                            var currentTurmas = connection.Query<int>(turmaQuery, parameters, transaction: transaction).ToList();

                            var newTurmas = aluno.AlunoTurmas.Select(at => at.TurmaId).ToList();

                            var turmasToRemove = currentTurmas.Except(newTurmas).ToList();

                            var turmasToAdd = newTurmas.Except(currentTurmas).ToList();

                            if (turmasToRemove.Any())
                            {
                                var deleteQuery = "DELETE FROM AlunoTurmas WHERE AlunoId = @AlunoId AND TurmaId IN @TurmaIds";
                                connection.Execute(deleteQuery, new { AlunoId = aluno.Id, TurmaIds = turmasToRemove }, transaction: transaction);
                            }

                            foreach (var turmaId in turmasToAdd)
                            {
                                var insertQuery = "INSERT INTO AlunoTurmas (AlunoId, TurmaId) VALUES (@AlunoId, @TurmaId)";
                                connection.Execute(insertQuery, new { AlunoId = aluno.Id, TurmaId = turmaId }, transaction: transaction);
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }


        public bool DeleteAluno(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "DELETE FROM Alunos WHERE Id = @Id";
                    connection.Execute(query, new { Id = id });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InativarAluno(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Alunos SET Ativo = 0 WHERE Id = @Id";
                    connection.Execute(query, new { Id = id });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AtivarAluno(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Alunos SET Ativo = 1 WHERE Id = @Id";
                    connection.Execute(query, new { Id = id });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private byte[] HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool IsPasswordStrong(string password)
        {
            bool lengthValid = password.Length >= 8;
            bool uppercaseValid = password.Any(char.IsUpper);
            bool specialCharValid = password.Any(ch => "!@#$%^&*(),.?\":{}|<>".Contains(ch));
            return lengthValid && uppercaseValid && specialCharValid;
        }
    }
}
