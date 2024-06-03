using Entities;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoFIAP.Interface;

namespace TesteTecnicoFiap_Oficial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoApiController : ControllerBase
    {
        private readonly IAlunoBLL _alunoBLL;

        public AlunoApiController(IAlunoBLL alunoBLL)
        {
            _alunoBLL = alunoBLL;
        }

        /// <summary>
        /// Retorna todos os alunos.
        /// </summary>
        /// <returns>Uma lista de alunos.</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var alunos = _alunoBLL.GetAllAlunos();
            return Ok(alunos);
        }

        /// <summary>
        /// Retorna um aluno específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do aluno.</param>
        /// <returns>Um aluno.</returns>
        [HttpGet("GetAluno/{id}")]
        public IActionResult GetAluno(int id)
        {
            var aluno = _alunoBLL.GetAlunoById(id);
            if (aluno == null)
            {
                return NotFound(new { success = false, message = "Aluno não encontrado." });
            }
            return Ok(aluno);
        }

        /// <summary>
        /// Adiciona um novo aluno.
        /// </summary>
        /// <param name="aluno">Os dados do novo aluno.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AddAluno")]
        public IActionResult AddAluno([FromBody] Aluno aluno)
        {
            if (aluno == null || string.IsNullOrWhiteSpace(aluno.Nome) || string.IsNullOrWhiteSpace(aluno.Email) || string.IsNullOrWhiteSpace(aluno.Senha))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var result = _alunoBLL.AddAluno(aluno);
            if (result.isSuccess)
            {
                return Ok(new { success = true, result.message });
            }
            return BadRequest(new { success = false, result.message });
        }

        /// <summary>
        /// Edita um aluno existente.
        /// </summary>
        /// <param name="id">O ID do aluno.</param>
        /// <param name="aluno">Os novos dados do aluno.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("EditAluno/{id}")]
        public IActionResult EditAluno(int id, [FromBody] Aluno aluno)
        {
            if (aluno == null || string.IsNullOrWhiteSpace(aluno.Nome) || string.IsNullOrWhiteSpace(aluno.Email))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var result = _alunoBLL.EditAluno(id, aluno);
            if (result)
            {
                return Ok(new { success = true, message = "Aluno atualizado com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao atualizar aluno." });
        }

        /// <summary>
        /// Exclui um aluno existente.
        /// </summary>
        /// <param name="id">O ID do aluno.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpDelete("DeleteAluno/{id}")]
        public IActionResult DeleteAluno(int id)
        {
            var result = _alunoBLL.DeleteAluno(id);
            if (result)
            {
                return Ok(new { success = true, message = "Aluno excluído com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao excluir aluno." });
        }

        /// <summary>
        /// Inativa um aluno existente.
        /// </summary>
        /// <param name="id">O ID do aluno.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("InativarAluno/{id}")]
        public IActionResult InativarAluno(int id)
        {
            var result = _alunoBLL.InativarAluno(id);
            if (result)
            {
                return Ok(new { success = true, message = "Aluno inativado com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao inativar aluno." });
        }

        /// <summary>
        /// Ativa um aluno existente.
        /// </summary>
        /// <param name="id">O ID do aluno.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AtivarAluno/{id}")]
        public IActionResult AtivarAluno(int id)
        {
            var result = _alunoBLL.AtivarAluno(id);
            if (result)
            {
                return Ok(new { success = true, message = "Aluno ativado com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao ativar aluno." });
        }
    }
}
