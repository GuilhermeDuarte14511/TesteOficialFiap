using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoFIAP.Web.Controllers.Api;
using Business;
using Entities;
using System.Collections.Generic;
using TesteTecnicoFIAP.Interface;

namespace TesteTecnicoFIAP.Tests
{
    public class AlunoTurmaApiTests
    {
        private readonly Mock<IAlunoTurmaBLL> _mockAlunoTurmaBLL;
        private readonly Mock<IAlunoBLL> _mockAlunoBLL;
        private readonly Mock<ITurmaBLL> _mockTurmaBLL;
        private readonly AlunoTurmaApiController _controller;

        public AlunoTurmaApiTests()
        {
            _mockAlunoTurmaBLL = new Mock<IAlunoTurmaBLL>();
            _mockAlunoBLL = new Mock<IAlunoBLL>();
            _mockTurmaBLL = new Mock<ITurmaBLL>();
            _controller = new AlunoTurmaApiController(_mockAlunoTurmaBLL.Object, _mockAlunoBLL.Object, _mockTurmaBLL.Object);
        }

        [Fact]
        public void GetAllTurmas_ReturnsOk()
        {
            var turmas = new List<Turma>
            {
                new Turma { Id = 1, Nome = "Turma A", Ativo = true },
                new Turma { Id = 2, Nome = "Turma B", Ativo = true }
            };
            _mockTurmaBLL.Setup(bll => bll.GetAllTurmas()).Returns(turmas);

            var result = _controller.GetAllTurmas();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Turma>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetAllAlunos_ReturnsOk()
        {
            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "João Silva", Email = "joao.silva@example.com", Ativo = true },
                new Aluno { Id = 2, Nome = "Maria Oliveira", Email = "maria.oliveira@example.com", Ativo = true }
            };
            _mockAlunoBLL.Setup(bll => bll.GetAllAlunos()).Returns(alunos);

            var result = _controller.GetAllAlunos();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Aluno>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetAlunosByTurma_ReturnsOk()
        {
            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "João Silva", Email = "joao.silva@example.com", Ativo = true }
            };
            _mockAlunoTurmaBLL.Setup(bll => bll.GetAlunosByTurma(1)).Returns(alunos);

            var result = _controller.GetAlunosByTurma(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Aluno>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void AddAlunoTurma_ReturnsBadRequest()
        {
            var result = _controller.AddAlunoTurma(0, 0);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void AddAlunoTurma_ReturnsOk()
        {
            var alunoTurma = new AlunoTurma { AlunoId = 1, TurmaId = 1 };
            _mockAlunoTurmaBLL.Setup(bll => bll.AddAlunoTurma(It.IsAny<AlunoTurma>())).Returns(true);

            var result = _controller.AddAlunoTurma(1, 1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void InativarAlunoTurma_ReturnsOk()
        {
            _mockAlunoTurmaBLL.Setup(bll => bll.InativarAlunoTurma(1)).Returns(true);

            var result = _controller.InativarAlunoTurma(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void AtivarAlunoTurma_ReturnsOk()
        {
            _mockAlunoTurmaBLL.Setup(bll => bll.AtivarAlunoTurma(1)).Returns(true);

            var result = _controller.AtivarAlunoTurma(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void DesvincularAlunoTurma_ReturnsBadRequest()
        {
            var result = _controller.DesvincularAlunoTurma(0, 0);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void DesvincularAlunoTurma_ReturnsOk()
        {
            _mockAlunoTurmaBLL.Setup(bll => bll.DesvincularAlunoTurma(1, 1)).Returns(true);

            var result = _controller.DesvincularAlunoTurma(1, 1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void VincularAlunoTurma_ReturnsBadRequest()
        {
            var result = _controller.VincularAlunoTurma(0, 0);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void VincularAlunoTurma_ReturnsOk()
        {
            _mockAlunoTurmaBLL.Setup(bll => bll.AddAlunoTurma(It.IsAny<AlunoTurma>())).Returns(true);

            var result = _controller.VincularAlunoTurma(1, 1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }
    }
}
