using Microsoft.AspNetCore.Mvc;
using Business;
using Entities;
using TesteTecnicoFIAP.Interface;
using Swashbuckle.AspNetCore.Filters;

namespace TesteTecnicoFIAP.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoTurmaApiController : ControllerBase
    {
        private readonly IAlunoTurmaBLL _alunoTurmaBLL;
        private readonly IAlunoBLL _alunoBLL;
        private readonly ITurmaBLL _turmaBLL;

        public AlunoTurmaApiController(IAlunoTurmaBLL alunoTurmaBLL, IAlunoBLL alunoBLL, ITurmaBLL turmaBLL)
        {
            _alunoTurmaBLL = alunoTurmaBLL;
            _alunoBLL = alunoBLL;
            _turmaBLL = turmaBLL;
        }

        /// <summary>
        /// Retorna todas as turmas.
        /// </summary>
        /// <returns>Uma lista de turmas.</returns>
        [HttpGet("GetAllTurmas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllTurmas()
        {
            var turmas = _turmaBLL.GetAllTurmas();
            return Ok(turmas);
        }

        /// <summary>
        /// Retorna todos os alunos.
        /// </summary>
        /// <returns>Uma lista de alunos.</returns>
        [HttpGet("GetAllAlunos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAlunos()
        {
            var alunos = _alunoBLL.GetAllAlunos();
            return Ok(alunos);
        }

        /// <summary>
        /// Retorna os alunos de uma turma específica.
        /// </summary>
        /// <param name="turmaId">O ID da turma.</param>
        /// <returns>Uma lista de alunos.</returns>
        [HttpGet("GetAlunosByTurma/{turmaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAlunosByTurma(int turmaId)
        {
            var alunos = _alunoTurmaBLL.GetAlunosByTurma(turmaId);
            if (alunos == null || !alunos.Any())
            {
                return NotFound(new { message = "Nenhum aluno encontrado para a turma especificada." });
            }
            return Ok(alunos);
        }

        /// <summary>
        /// Adiciona um aluno a uma turma.
        /// </summary>
        /// <param name="alunoId">O ID do aluno.</param>
        /// <param name="turmaId">O ID da turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AddAlunoTurma")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddAlunoTurma(int alunoId, int turmaId)
        {
            if (alunoId <= 0 || turmaId <= 0)
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var alunoTurma = new AlunoTurma
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            };

            var result = _alunoTurmaBLL.AddAlunoTurma(alunoTurma);
            if (result)
            {
                return Ok(new { success = true, message = "Associação adicionada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao adicionar associação." });
        }

        /// <summary>
        /// Inativa a associação de um aluno a uma turma.
        /// </summary>
        /// <param name="id">O ID da associação.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("InativarAlunoTurma/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult InativarAlunoTurma(int id)
        {
            var result = _alunoTurmaBLL.InativarAlunoTurma(id);
            if (result)
            {
                return Ok(new { success = true, message = "Associação inativada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao inativar associação." });
        }

        /// <summary>
        /// Ativa a associação de um aluno a uma turma.
        /// </summary>
        /// <param name="id">O ID da associação.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AtivarAlunoTurma/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AtivarAlunoTurma(int id)
        {
            var result = _alunoTurmaBLL.AtivarAlunoTurma(id);
            if (result)
            {
                return Ok(new { success = true, message = "Associação ativada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao ativar associação." });
        }

        /// <summary>
        /// Desvincula um aluno de uma turma.
        /// </summary>
        /// <param name="alunoId">O ID do aluno.</param>
        /// <param name="turmaId">O ID da turma.</param>
        /// <returns>Resultado da operação.</param>
        [HttpPost("DesvincularAlunoTurma")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DesvincularAlunoTurma(int alunoId, int turmaId)
        {
            if (alunoId <= 0 || turmaId <= 0)
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var result = _alunoTurmaBLL.DesvincularAlunoTurma(alunoId, turmaId);
            if (result)
            {
                return Ok(new { success = true, message = "Associação desvinculada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao desvincular associação." });
        }

        /// <summary>
        /// Vincula um aluno a uma turma.
        /// </summary>
        /// <param name="alunoId">O ID do aluno.</param>
        /// <param name="turmaId">O ID da turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("VincularAlunoTurma")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult VincularAlunoTurma(int alunoId, int turmaId)
        {
            if (alunoId <= 0 || turmaId <= 0)
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var alunoTurma = new AlunoTurma
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            };

            var result = _alunoTurmaBLL.AddAlunoTurma(alunoTurma);
            if (result)
            {
                return Ok(new { success = true, message = "Associação adicionada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao adicionar associação." });
        }
    }
}
