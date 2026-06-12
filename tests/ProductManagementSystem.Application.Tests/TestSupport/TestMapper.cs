using AutoMapper;
using ProductManagementSystem.Application.Mapping;

namespace ProductManagementSystem.Application.Tests.TestSupport;

internal static class TestMapper
{
    public static IMapper Create()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<ApplicationMappingProfile>();
        });

        configuration.AssertConfigurationIsValid();

        return configuration.CreateMapper();
    }
}
