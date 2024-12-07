using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceFiap.Model;
using WebServiceFiap.Repository;
using WebServiceFiap.Services;
using Xunit;

namespace WebServiceFiap.Tests
{
    public class MonitoramentoAguaServiceTests
    {
        [Fact]
        public async Task GetAllPagedAsync_ShouldReturnExpectedItemsAndCount()
        {
            // Arrange
            var mockRepo = new Mock<IMonitoramentoAguaRepository>();

            var fakeData = new List<MonitoramentoAgua>
            {
                new MonitoramentoAgua { ID_MONITORAMENTO_AGUA = 1, LC_LOCALIZACAO = "Local 1" },
                new MonitoramentoAgua { ID_MONITORAMENTO_AGUA = 2, LC_LOCALIZACAO = "Local 2" }
            };

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(fakeData);

            mockRepo.Setup(r => r.GetTotalCountAsync())
                    .ReturnsAsync(fakeData.Count);

            var service = new MonitoramentoAguaService(mockRepo.Object);

            // Act
            var (items, total) = await service.GetAllPagedAsync(1, 10);

            // Assert
            Assert.NotNull(items);
            Assert.Equal(2, total);
            Assert.Collection(items,
                item => Assert.Equal("Local 1", item.LC_LOCALIZACAO),
                item => Assert.Equal("Local 2", item.LC_LOCALIZACAO));
        }
    }
}
