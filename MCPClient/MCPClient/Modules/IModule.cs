namespace MCPClient.Modules;

public interface IModule
{
    IServiceCollection RegisterModule(WebApplicationBuilder builder);
    WebApplication MapEndpoints(WebApplication endpoints);
}