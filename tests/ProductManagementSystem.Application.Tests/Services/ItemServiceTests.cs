using FluentAssertions;
using Moq;
using ProductManagementSystem.Application.DTOs.Items;
using ProductManagementSystem.Application.Interfaces.Persistence;
using ProductManagementSystem.Application.Services;
using ProductManagementSystem.Application.Tests.TestSupport;
using ProductManagementSystem.Domain.Entities;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Tests.Services;

public sealed class ItemServiceTests
{
    [Fact]
    public async Task CreateItemAsync_ReturnsFailure_WhenProductDoesNotExist()
    {
        var productRepository = new Mock<IGenericRepository<Product>>();
        productRepository
            .Setup(repository => repository.AnyAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var itemRepository = new Mock<IGenericRepository<Item>>();
        var unitOfWork = CreateUnitOfWork(productRepository.Object, itemRepository.Object);
        var service = new ItemService(unitOfWork.Object, TestMapper.Create());

        var response = await service.CreateItemAsync(new CreateItemDto
        {
            ProductId = 99,
            Quantity = 2
        });

        response.Succeeded.Should().BeFalse();
        itemRepository.Verify(repository => repository.AddAsync(It.IsAny<Item>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateItemAsync_AddsItem_WhenProductExists()
    {
        var productRepository = new Mock<IGenericRepository<Product>>();
        productRepository
            .Setup(repository => repository.AnyAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var itemRepository = new Mock<IGenericRepository<Item>>();
        var unitOfWork = CreateUnitOfWork(productRepository.Object, itemRepository.Object);
        var service = new ItemService(unitOfWork.Object, TestMapper.Create());

        var response = await service.CreateItemAsync(new CreateItemDto
        {
            ProductId = 1,
            Quantity = 5
        });

        response.Succeeded.Should().BeTrue();
        response.Data!.Quantity.Should().Be(5);
        itemRepository.Verify(repository => repository.AddAsync(It.Is<Item>(item => item.ProductId == 1 && item.Quantity == 5), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private static Mock<IUnitOfWork> CreateUnitOfWork(
        IGenericRepository<Product> productRepository,
        IGenericRepository<Item> itemRepository)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.SetupGet(work => work.Products).Returns(productRepository);
        unitOfWork.SetupGet(work => work.Items).Returns(itemRepository);
        unitOfWork.Setup(work => work.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        return unitOfWork;
    }
}
