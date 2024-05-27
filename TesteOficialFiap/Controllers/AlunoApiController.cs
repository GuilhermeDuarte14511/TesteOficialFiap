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

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var alunos = _alunoBLL.GetAllAlunos();
            return Ok(alunos);
        }

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
