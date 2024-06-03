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
        /// <param name="turma">Os dados da nova turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AddTurma")]
        public IActionResult AddTurma([FromBody] Turma turma)
        {
            if (turma == null || string.IsNullOrWhiteSpace(turma.Nome))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

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
        /// <param name="turma">Os novos dados da turma.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("EditTurma/{id}")]
        public IActionResult EditTurma(int id, [FromBody] Turma turma)
        {
            if (turma == null || string.IsNullOrWhiteSpace(turma.Nome))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

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
