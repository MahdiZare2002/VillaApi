using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineShop.Utility
{
    public class SwaggerVillaDocument : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public SwaggerVillaDocument(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Villa Api",
                        Version = item.ApiVersion.ToString(),
                        Description = $"API version {item.ApiVersion}"
                    });
                var path = Path.Combine(AppContext.BaseDirectory, "SwaggerComment.xml");
                options.IncludeXmlComments(path);
            }
        }
    }
}
