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
            // Arrange
            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "João Silva", Email = "joao.silva@example.com", Ativo = true },
                new Aluno { Id = 2, Nome = "Maria Oliveira", Email = "maria.oliveira@example.com", Ativo = true }
            };
            _mockAlunoBLL.Setup(bll => bll.GetAllAlunos()).Returns(alunos);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Aluno>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetAluno_ReturnsNotFound()
        {
            // Arrange
            _mockAlunoBLL.Setup(bll => bll.GetAlunoById(1)).Returns((Aluno)null);

            // Act
            var result = _controller.GetAluno(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.False((bool)notFoundResult.Value.GetType().GetProperty("success").GetValue(notFoundResult.Value, null));
        }

        [Fact]
        public void GetAluno_ReturnsOk()
        {
            // Arrange
            var aluno = new Aluno { Id = 1, Nome = "João Silva", Email = "joao.silva@example.com", Ativo = true };
            _mockAlunoBLL.Setup(bll => bll.GetAlunoById(1)).Returns(aluno);

            // Act
            var result = _controller.GetAluno(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Aluno>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void AddAluno_ReturnsBadRequest()
        {
            // Arrange
            var aluno = new Aluno { Nome = "", Email = "", Senha = "" };

            // Act
            var result = _controller.AddAluno(aluno);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void AddAluno_ReturnsOk()
        {
            // Arrange
            var aluno = new Aluno { Nome = "João Silva", Email = "joao.silva@example.com", Senha = "SenhaSegura" };
            _mockAlunoBLL.Setup(bll => bll.AddAluno(aluno)).Returns((true, "Aluno adicionado com sucesso."));

            // Act
            var result = _controller.AddAluno(aluno);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void EditAluno_ReturnsBadRequest()
        {
            // Arrange
            var aluno = new Aluno { Nome = "", Email = "" };

            // Act
            var result = _controller.EditAluno(1, aluno);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void EditAluno_ReturnsOk()
        {
            // Arrange
            var aluno = new Aluno { Nome = "João Silva", Email = "joao.silva@example.com" };
            _mockAlunoBLL.Setup(bll => bll.EditAluno(1, aluno)).Returns(true);

            // Act
            var result = _controller.EditAluno(1, aluno);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void DeleteAluno_ReturnsOk()
        {
            // Arrange
            _mockAlunoBLL.Setup(bll => bll.DeleteAluno(1)).Returns(true);

            // Act
            var result = _controller.DeleteAluno(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void InativarAluno_ReturnsOk()
        {
            // Arrange
            _mockAlunoBLL.Setup(bll => bll.InativarAluno(1)).Returns(true);

            // Act
            var result = _controller.InativarAluno(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void AtivarAluno_ReturnsOk()
        {
            // Arrange
            _mockAlunoBLL.Setup(bll => bll.AtivarAluno(1)).Returns(true);

            // Act
            var result = _controller.AtivarAluno(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }
    }
}
