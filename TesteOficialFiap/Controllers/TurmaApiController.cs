using Microsoft.AspNetCore.Mvc;
using Business;
using Entities;
using TesteTecnicoFIAP.Interface;

namespace TesteTecnicoFIAP.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaApiController : ControllerBase
    {
        private readonly ITurmaBLL _turmaBLL;

        public TurmaApiController(ITurmaBLL turmaBLL)
        {
            _turmaBLL = turmaBLL;
        }

        /// <summary>
        /// Retorna todas as turmas.
        /// </summary>
        /// <returns>Uma lista de turmas.</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var turmas = _turmaBLL.GetAllTurmas();
            return Ok(turmas);
        }

        /// <summary>
        /// Retorna uma turma específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da turma.</param>
        /// <returns>Uma turma.</returns>
        [HttpGet("GetTurma/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTurma(int id)
        {
            var turma = _turmaBLL.GetTurmaById(id);
            if (turma == null)
            {
                return NotFound(new { success = false, message = "Turma não encontrada." });
            }
            return Ok(turma);
        }

        /// <summary>
        /// Adiciona uma nova turma.
        /// </summary>
        /// <param name="nome">Nome da nova turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AddTurma")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTurma([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var turma = new Turma
            {
                Nome = nome,
                Ativo = true
            };

            var result = _turmaBLL.AddTurma(turma);
            if (result)
            {
                return Ok(new { success = true, message = "Turma adicionada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Já existe uma turma com esse nome. Verifique se está correto e tente novamente." });
        }

        /// <summary>
        /// Edita uma turma existente.
        /// </summary>
        /// <param name="id">O ID da turma.</param>
        /// <param name="nome">Nome da turma.</param>
        /// <param name="ativo">Estado da turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("EditTurma/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditTurma(int id, [FromQuery] string nome, [FromQuery] bool ativo)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var turma = new Turma
            {
                Nome = nome,
                Ativo = ativo
            };

            var result = _turmaBLL.EditTurma(id, turma);
            if (result)
            {
                return Ok(new { success = true, message = "Turma atualizada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao atualizar turma." });
        }

        /// <summary>
        /// Inativa uma turma existente.
        /// </summary>
        /// <param name="id">O ID da turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("InativarTurma/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult InativarTurma(int id)
        {
            var result = _turmaBLL.InativarTurma(id);
            if (result)
            {
                return Ok(new { success = true, message = "Turma inativada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao inativar turma." });
        }

        /// <summary>
        /// Ativa uma turma existente.
        /// </summary>
        /// <param name="id">O ID da turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AtivarTurma/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AtivarTurma(int id)
        {
            var result = _turmaBLL.AtivarTurma(id);
            if (result)
            {
                return Ok(new { success = true, message = "Turma ativada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao ativar turma." });
        }
    }
}
