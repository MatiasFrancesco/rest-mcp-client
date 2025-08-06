using MCPClient.AbstractMcpServer.Abstracts;
using MCPClient.SharedKernel.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MCPClient.AbstractMcpServer;

public static class AbstractMcpServerHelper
{
    public static IServiceCollection AddAbstractMcpServer(this IServiceCollection services)
    {
        // Return scopoed abstract MCP server services that have a getTools()  
        services.AddSingleton<IList<McpServerConnectionString>>(provider =>
        {
            var config = provider.GetService<IConfiguration>()!;
            var mcpServersSection = config.GetSection("OrdineDeiSintetizzatori:McpServers");

            IList<McpServerConnectionString> mcpServers = new List<McpServerConnectionString>();
            foreach (var serverSection in mcpServersSection.GetChildren())
            {
                var serverUrl = serverSection["Url"]!;
                var serverName = serverSection["Name"]!;
                var serverToken = serverSection["ApiKey"]!;

                mcpServers.Add(new McpServerConnectionString(serverUrl, serverName, serverToken));
            }

            return mcpServers;
        });

        services.AddScoped<IAbstractMcpServer, Concretes.AbstractMcpServer>();

        return services;
    }
}