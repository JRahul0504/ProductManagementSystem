using FluentAssertions;
using Moq;
using ProductManagementSystem.Application.DTOs.Common;
using ProductManagementSystem.Application.DTOs.Products;
using ProductManagementSystem.Application.Interfaces.Persistence;
using ProductManagementSystem.Application.Services;
using ProductManagementSystem.Application.Tests.TestSupport;
using ProductManagementSystem.Domain.Entities;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Tests.Services;

public sealed class ProductServiceTests
{
    [Fact]
    public async Task GetProductsAsync_ReturnsPagedSearchAndSortedProducts()
    {
        var products = new List<Product>
        {
            new() { Id = 1, ProductName = "Keyboard", CreatedBy = "admin", CreatedOn = DateTime.UtcNow },
            new() { Id = 2, ProductName = "Mouse", CreatedBy = "admin", CreatedOn = DateTime.UtcNow },
            new() { Id = 3, ProductName = "Monitor", CreatedBy = "admin", CreatedOn = DateTime.UtcNow }
        };
        var productRepository = new Mock<IGenericRepository<Product>>();
        productRepository
            .Setup(repository => repository.FindAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<Product, bool>> predicate, CancellationToken _) => products.Where(predicate.Compile()).ToList());

        var unitOfWork = CreateUnitOfWork(productRepository.Object);
        var service = new ProductService(unitOfWork.Object, TestMapper.Create());

        var response = await service.GetProductsAsync(new PagedRequestDto
        {
            PageNumber = 1,
            PageSize = 2,
            SearchTerm = "Mo",
            SortBy = "ProductName",
            SortDirection = "desc"
        });

        response.Succeeded.Should().BeTrue();
        response.Data!.TotalRecords.Should().Be(2);
        response.Data.Data.Select(product => product.ProductName).Should().Equal("Mouse", "Monitor");
    }

    [Fact]
    public async Task CreateProductAsync_AddsProductAndCommits()
    {
        var productRepository = new Mock<IGenericRepository<Product>>();
        var unitOfWork = CreateUnitOfWork(productRepository.Object);
        var service = new ProductService(unitOfWork.Object, TestMapper.Create());

        var response = await service.CreateProductAsync(new CreateProductDto
        {
            ProductName = "Laptop",
            CreatedBy = "admin"
        });

        response.Succeeded.Should().BeTrue();
        response.Data!.ProductName.Should().Be("Laptop");
        productRepository.Verify(repository => repository.AddAsync(It.Is<Product>(product => product.ProductName == "Laptop"), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteProductAsync_SoftDeletesProduct()
    {
        var product = new Product
        {
            Id = 10,
            ProductName = "Tablet",
            CreatedBy = "admin",
            CreatedOn = DateTime.UtcNow
        };
        var productRepository = new Mock<IGenericRepository<Product>>();
        productRepository
            .Setup(repository => repository.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var unitOfWork = CreateUnitOfWork(productRepository.Object);
        var service = new ProductService(unitOfWork.Object, TestMapper.Create());

        var response = await service.DeleteProductAsync(product.Id);

        response.Succeeded.Should().BeTrue();
        product.IsDeleted.Should().BeTrue();
        product.DeletedOn.Should().NotBeNull();
        productRepository.Verify(repository => repository.Update(product), Times.Once);
        productRepository.Verify(repository => repository.Delete(It.IsAny<Product>()), Times.Never);
    }

    private static Mock<IUnitOfWork> CreateUnitOfWork(IGenericRepository<Product> productRepository)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.SetupGet(work => work.Products).Returns(productRepository);
        unitOfWork.Setup(work => work.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        return unitOfWork;
    }
}
