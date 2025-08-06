using MCPClient.AbstractMcpServer.Abstracts;
using MCPClient.SharedKernel.Configuration;
using ModelContextProtocol.Client;

namespace MCPClient.AbstractMcpServer.Concretes;

public class AbstractMcpServer : IAbstractMcpServer
{
    private readonly IList<McpServerConnectionString> _configuration;
    private IList<IMcpClient>? _mcpClients;
    private readonly SemaphoreSlim _initializationSemaphore = new(1, 1);

    public AbstractMcpServer(IList<McpServerConnectionString> configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<IList<McpClientTool>> GetToolsAsync()
    {
        await EnsureInitializedAsync();
        
        var tools = new List<McpClientTool>();
        
        foreach (var client in _mcpClients!)
        {
            IList<McpClientTool> clientTools = await client.ListToolsAsync();
            tools.AddRange(clientTools);
        }
        
        return tools;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_mcpClients != null) return;

        await _initializationSemaphore.WaitAsync();
        try
        {
            if (_mcpClients != null) return;

            if (_configuration.Count == 0)
            {
                throw new InvalidOperationException("No MCP servers configured.");
            }

            var mcpClients = new List<IMcpClient>();

            foreach (var server in _configuration)
            {
                IMcpClient mcpClient = await McpClientFactory.CreateAsync(
                    new SseClientTransport(new()
                    {
                        Name = server.ServerName,
                        Endpoint = new Uri(server.ServerUrl),
                    }));

                mcpClients.Add(mcpClient);
            }

            _mcpClients = mcpClients;
        }
        finally
        {
            _initializationSemaphore.Release();
        }
    }
}