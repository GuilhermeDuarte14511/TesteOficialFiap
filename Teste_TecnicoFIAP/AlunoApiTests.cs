using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteTecnicoFIAP.Interface;
using TesteTecnicoFiap_Oficial.Controllers;

namespace TesteTecnicoFIAP.Tests
{
    public class AlunoApiTests
    {
        private readonly Mock<IAlunoBLL> _mockAlunoBLL;
        private readonly AlunoApiController _controller;

        public AlunoApiTests()
        {
            _mockAlunoBLL = new Mock<IAlunoBLL>();
            _controller = new AlunoApiController(_mockAlunoBLL.Object);
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "João Silva", Email = "joao.silva@example.com", Ativo = true },
                new Aluno { Id = 2, Nome = "Maria Oliveira", Email = "maria.oliveira@example.com", Ativo = true }
            };
            _mockAlunoBLL.Setup(bll => bll.GetAllAlunos()).Returns(alunos);

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Aluno>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetAluno_ReturnsNotFound()
        {
            _mockAlunoBLL.Setup(bll => bll.GetAlunoById(1)).Returns((Aluno)null);

            var result = _controller.GetAluno(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.False((bool)notFoundResult.Value.GetType().GetProperty("success").GetValue(notFoundResult.Value, null));
        }

        [Fact]
        public void GetAluno_ReturnsOk()
        {
            var aluno = new Aluno { Id = 1, Nome = "João Silva", Email = "joao.silva@example.com", Ativo = true };
            _mockAlunoBLL.Setup(bll => bll.GetAlunoById(1)).Returns(aluno);

            var result = _controller.GetAluno(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Aluno>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void AddAluno_ReturnsBadRequest()
        {
            var result = _controller.AddAluno("", "", "");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void AddAluno_ReturnsOk()
        {
            _mockAlunoBLL.Setup(bll => bll.AddAluno(It.IsAny<Aluno>())).Returns((true, "Aluno adicionado com sucesso."));

            var result = _controller.AddAluno("João Silva", "joao.silva@example.com", "SenhaSegura");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void EditAluno_ReturnsBadRequest()
        {
            var result = _controller.EditAluno(1, "", "");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void EditAluno_ReturnsOk()
        {
            var aluno = new Aluno { Nome = "João Silva", Email = "joao.silva@example.com" };
            _mockAlunoBLL.Setup(bll => bll.EditAluno(1, It.IsAny<Aluno>())).Returns(true);

            var result = _controller.EditAluno(1, "João Silva", "joao.silva@example.com");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void DeleteAluno_ReturnsOk()
        {
            _mockAlunoBLL.Setup(bll => bll.DeleteAluno(1)).Returns(true);

            var result = _controller.DeleteAluno(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void InativarAluno_ReturnsOk()
        {
            _mockAlunoBLL.Setup(bll => bll.InativarAluno(1)).Returns(true);

            var result = _controller.InativarAluno(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void AtivarAluno_ReturnsOk()
        {
            _mockAlunoBLL.Setup(bll => bll.AtivarAluno(1)).Returns(true);

            var result = _controller.AtivarAluno(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }
    }
}
