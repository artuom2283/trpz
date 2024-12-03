using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusServ.DTOs;
using BusServ.Mappings;
using BusServ.Services.Implementation;
using lab7.Entities;
using lab7.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Tests
{
    public class BusServiceTests
    {
        private readonly Mock<IBusRepository> _mockBusRepository;
        private readonly IMapper _mapper;
        private readonly BusService _busService;

        public BusServiceTests()
        {
            _mockBusRepository = new Mock<IBusRepository>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new BusProfile()));
            _mapper = mapperConfig.CreateMapper();

            _busService = new BusService(_mockBusRepository.Object, _mapper);
        }

        [Fact]
        public void GetAllBuses_ShouldReturnAllBuses()
        {
            // Arrange
            var buses = new List<Bus>
            {
                new Bus { Id = 1, BusNumber = "B123", Capacity = 50 },
                new Bus { Id = 2, BusNumber = "B456", Capacity = 40 }
            };
            _mockBusRepository.Setup(repo => repo.GetAll()).Returns(buses);

            // Act
            var result = _busService.GetAllBuses();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("B123", result.First().BusNumber);
        }

        [Fact]
        public void GetBusById_ShouldReturnBus_WhenBusExists()
        {
            // Arrange
            var bus = new Bus { Id = 1, BusNumber = "B123", Capacity = 50 };
            _mockBusRepository.Setup(repo => repo.Get(1)).Returns(bus);

            // Act
            var result = _busService.GetBusById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("B123", result.BusNumber);
            Assert.Equal(50, result.Capacity);
        }

        [Fact]
        public void CreateBus_ShouldCallRepositoryCreate()
        {
            // Arrange
            var busDto = new BusDto { Id = 1, BusNumber = "B123", Capacity = 50 };

            // Act
            _busService.CreateBus(busDto);

            // Assert
            _mockBusRepository.Verify(repo => repo.Create(It.IsAny<Bus>()), Times.Once);
        }

        [Fact]
        public void UpdateBus_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var busDto = new BusDto { Id = 1, BusNumber = "B123", Capacity = 50 };

            // Act
            _busService.UpdateBus(busDto);

            // Assert
            _mockBusRepository.Verify(repo => repo.Update(It.IsAny<Bus>()), Times.Once);
        }

        [Fact]
        public void DeleteBus_ShouldCallRepositoryDelete()
        {
            // Arrange
            var busId = 1;

            // Act
            _busService.DeleteBus(busId);

            // Assert
            _mockBusRepository.Verify(repo => repo.Delete(busId), Times.Once);
        }
    }
}