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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <param name="nome">Nome do aluno.</param>
        /// <param name="email">Email do aluno.</param>
        /// <param name="senha">Senha do aluno.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost("AddAluno")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddAluno([FromQuery] string nome, [FromQuery] string email, [FromQuery] string senha)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var aluno = new Aluno
            {
                Nome = nome,
                Email = email,
                Senha = senha,
                Ativo = true
            };

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
        /// <param name="nome">Nome do aluno.</param>
        /// <param name="email">Email do aluno.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("EditAluno/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditAluno(int id, [FromQuery] string nome, [FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var aluno = new Aluno
            {
                Nome = nome,
                Email = email,
                Id = id
            };

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
