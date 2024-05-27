using Microsoft.AspNetCore.Mvc;
using Business;
using Entities;
using TesteTecnicoFIAP.Interface;

namespace TesteTecnicoFIAP.Web.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaBLL _turmaBLL;

        public TurmaController(ITurmaBLL turmaBLL)
        {
            _turmaBLL = turmaBLL;
        }

        public IActionResult TurmaIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var turmas = _turmaBLL.GetAllTurmas();
            return Json(turmas);
        }

        [HttpGet]
        public IActionResult GetTurma(int id)
        {
            var turma = _turmaBLL.GetTurmaById(id);
            return Json(turma);
        }

        [HttpPost]
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

        [HttpPost]
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
            return BadRequest(new { success = false, message = "Já existe uma turma com esse nome. Verifique se está correto e tente novamente." });
        }

        [HttpPost]
        public IActionResult InativarTurma(int id)
        {
            var result = _turmaBLL.InativarTurma(id);
            if (result)
            {
                return Ok(new { success = true, message = "Turma inativada com sucesso." });
            }
            return BadRequest(new { success = false, message = "Erro ao inativar turma." });
        }

        [HttpPost]
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
