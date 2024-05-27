using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoFIAP.Web.Controllers.Api;
using Business;
using Entities;
using TesteTecnicoFIAP.Interface;
using System.Collections.Generic;

namespace TesteTecnicoFIAP.Tests
{
    public class TurmaApiTests
    {
        private readonly Mock<ITurmaBLL> _mockTurmaBLL;
        private readonly TurmaApiController _controller;

        public TurmaApiTests()
        {
            _mockTurmaBLL = new Mock<ITurmaBLL>();
            _controller = new TurmaApiController(_mockTurmaBLL.Object);
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            // Arrange
            var turmas = new List<Turma>
            {
                new Turma { Id = 1, Nome = "Turma A", Ativo = true },
                new Turma { Id = 2, Nome = "Turma B", Ativo = true }
            };
            _mockTurmaBLL.Setup(bll => bll.GetAllTurmas()).Returns(turmas);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Turma>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetTurma_ReturnsNotFound()
        {
            // Arrange
            _mockTurmaBLL.Setup(bll => bll.GetTurmaById(1)).Returns((Turma)null);

            // Act
            var result = _controller.GetTurma(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.False((bool)notFoundResult.Value.GetType().GetProperty("success").GetValue(notFoundResult.Value, null));
        }

        [Fact]
        public void GetTurma_ReturnsOk()
        {
            // Arrange
            var turma = new Turma { Id = 1, Nome = "Turma A", Ativo = true };
            _mockTurmaBLL.Setup(bll => bll.GetTurmaById(1)).Returns(turma);

            // Act
            var result = _controller.GetTurma(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Turma>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void AddTurma_ReturnsBadRequest()
        {
            // Arrange
            var turma = new Turma { Nome = "" };

            // Act
            var result = _controller.AddTurma(turma);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void AddTurma_ReturnsOk()
        {
            // Arrange
            var turma = new Turma { Nome = "Turma A", Ativo = true };
            _mockTurmaBLL.Setup(bll => bll.AddTurma(turma)).Returns(true);

            // Act
            var result = _controller.AddTurma(turma);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void EditTurma_ReturnsBadRequest()
        {
            // Arrange
            var turma = new Turma { Nome = "" };

            // Act
            var result = _controller.EditTurma(1, turma);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False((bool)badRequestResult.Value.GetType().GetProperty("success").GetValue(badRequestResult.Value, null));
        }

        [Fact]
        public void EditTurma_ReturnsOk()
        {
            // Arrange
            var turma = new Turma { Nome = "Turma A", Ativo = true };
            _mockTurmaBLL.Setup(bll => bll.EditTurma(1, turma)).Returns(true);

            // Act
            var result = _controller.EditTurma(1, turma);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void InativarTurma_ReturnsOk()
        {
            // Arrange
            _mockTurmaBLL.Setup(bll => bll.InativarTurma(1)).Returns(true);

            // Act
            var result = _controller.InativarTurma(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }

        [Fact]
        public void AtivarTurma_ReturnsOk()
        {
            // Arrange
            _mockTurmaBLL.Setup(bll => bll.AtivarTurma(1)).Returns(true);

            // Act
            var result = _controller.AtivarTurma(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
        }
    }
}
