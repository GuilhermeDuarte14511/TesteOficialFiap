using Microsoft.AspNetCore.Mvc;
using Business;
using Entities;
using TesteTecnicoFIAP.Interface;
using System.Linq;

namespace TesteTecnicoFIAP.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoBLL _alunoBLL;

        public AlunoController(IAlunoBLL alunoBLL)
        {
            _alunoBLL = alunoBLL;
        }

        public IActionResult AlunoIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var alunos = _alunoBLL.GetAllAlunos();
            return Json(alunos);
        }

        [HttpGet]
        public IActionResult GetAluno(int id)
        {
            var aluno = _alunoBLL.GetAlunoById(id);
            return Json(aluno);
        }

        [HttpPost]
        public IActionResult AddAluno([FromBody] Aluno aluno)
        {
            if (aluno == null || string.IsNullOrWhiteSpace(aluno.Nome) || string.IsNullOrWhiteSpace(aluno.Email) || string.IsNullOrWhiteSpace(aluno.Senha))
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var result = _alunoBLL.AddAluno(aluno);
            if (result.isSuccess)
            {
                return Ok(new { success = true, message = result.message });
            }
            return BadRequest(new { success = false, message = result.message });
        }

        [HttpPost]
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

        [HttpPost]
        public IActionResult DeleteAluno(int id)
        {
            var result = _alunoBLL.DeleteAluno(id);
            if (result)
            {
                return Ok(new { success = true, message = "Aluno excluído com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao excluir aluno." });
        }

        [HttpPost]
        public IActionResult InativarAluno(int id)
        {
            var result = _alunoBLL.InativarAluno(id);
            if (result)
            {
                return Ok(new { success = true, message = "Aluno inativado com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao inativar aluno." });
        }

        [HttpPost]
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
