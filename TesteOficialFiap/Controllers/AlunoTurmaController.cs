using Microsoft.AspNetCore.Mvc;
using Business;
using Entities;
using TesteTecnicoFIAP.Interface;

namespace TesteTecnicoFIAP.Web.Controllers
{
    public class AlunoTurmaController : Controller
    {
        private readonly IAlunoTurmaBLL _alunoTurmaBLL;
        private readonly IAlunoBLL _alunoBLL;
        private readonly ITurmaBLL _turmaBLL;

        public AlunoTurmaController(IAlunoTurmaBLL alunoTurmaBLL, IAlunoBLL alunoBLL, ITurmaBLL turmaBLL)
        {
            _alunoTurmaBLL = alunoTurmaBLL;
            _alunoBLL = alunoBLL;
            _turmaBLL = turmaBLL;
        }

        public IActionResult AlunoTurmaIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllTurmas()
        {
            var turmas = _turmaBLL.GetAllTurmas();
            return Json(turmas);
        }

        [HttpGet]
        public IActionResult GetAllAlunos()
        {
            var alunos = _alunoBLL.GetAllAlunos();
            return Json(alunos);
        }

        [HttpGet]
        public IActionResult GetAlunosByTurma(int turmaId)
        {
            var alunos = _alunoTurmaBLL.GetAlunosByTurma(turmaId);
            return Json(alunos);
        }

        [HttpPost]
        public IActionResult AddAlunoTurma([FromBody] AlunoTurma alunoTurma)
        {
            if (alunoTurma == null || alunoTurma.AlunoId <= 0 || alunoTurma.TurmaId <= 0)
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var result = _alunoTurmaBLL.AddAlunoTurma(alunoTurma);
            if (result)
            {
                return Ok(new { success = true, message = "Associação adicionada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao adicionar associação." });
        }

        [HttpPost]
        public IActionResult InativarAlunoTurma(int id)
        {
            var result = _alunoTurmaBLL.InativarAlunoTurma(id);
            if (result)
            {
                return Ok(new { success = true, message = "Associação inativada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao inativar associação." });
        }

        [HttpPost]
        public IActionResult AtivarAlunoTurma(int id)
        {
            var result = _alunoTurmaBLL.AtivarAlunoTurma(id);
            if (result)
            {
                return Ok(new { success = true, message = "Associação ativada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao ativar associação." });
        }

        [HttpPost]
        public IActionResult DesvincularAlunoTurma(int alunoId, int turmaId)
        {
            var result = _alunoTurmaBLL.DesvincularAlunoTurma(alunoId, turmaId);
            if (result)
            {
                return Ok(new { success = true, message = "Associação desvinculada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao desvincular associação." });
        }

        [HttpPost]
        public IActionResult VincularAlunoTurma([FromBody] AlunoTurma alunoTurma)
        {
            if (alunoTurma == null || alunoTurma.AlunoId <= 0 || alunoTurma.TurmaId <= 0)
            {
                return BadRequest(new { success = false, message = "Dados inválidos. Verifique se todos os campos obrigatórios estão preenchidos." });
            }

            var result = _alunoTurmaBLL.AddAlunoTurma(alunoTurma);
            if (result)
            {
                return Ok(new { success = true, message = "Associação adicionada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao adicionar associação." });
        }
    }
}
