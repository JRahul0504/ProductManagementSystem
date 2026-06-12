using ProductManagementSystem.Application.DTOs.Auth;
using ProductManagementSystem.Application.DTOs.Items;
using ProductManagementSystem.Application.DTOs.Products;

namespace ProductManagementSystem.Application.Mapping;

/// <summary>
/// Defines AutoMapper mappings for application DTOs and domain entities.
/// </summary>
public sealed class ApplicationMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationMappingProfile"/> class.
    /// </summary>
    public ApplicationMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedOn, options => options.Ignore())
            .ForMember(destination => destination.ModifiedBy, options => options.Ignore())
            .ForMember(destination => destination.ModifiedOn, options => options.Ignore())
            .ForMember(destination => destination.IsDeleted, options => options.Ignore())
            .ForMember(destination => destination.DeletedBy, options => options.Ignore())
            .ForMember(destination => destination.DeletedOn, options => options.Ignore())
            .ForMember(destination => destination.Items, options => options.Ignore());
        CreateMap<UpdateProductDto, Product>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedBy, options => options.Ignore())
            .ForMember(destination => destination.CreatedOn, options => options.Ignore())
            .ForMember(destination => destination.ModifiedOn, options => options.Ignore())
            .ForMember(destination => destination.IsDeleted, options => options.Ignore())
            .ForMember(destination => destination.DeletedBy, options => options.Ignore())
            .ForMember(destination => destination.DeletedOn, options => options.Ignore())
            .ForMember(destination => destination.Items, options => options.Ignore());

        CreateMap<Item, ItemDto>();
        CreateMap<CreateItemDto, Item>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.Product, options => options.Ignore());
        CreateMap<UpdateItemDto, Item>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.ProductId, options => options.Ignore())
            .ForMember(destination => destination.Product, options => options.Ignore());

        CreateMap<ApplicationUser, UserDto>();
    }
}
