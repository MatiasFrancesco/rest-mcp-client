using ModelContextProtocol.Client;

namespace MCPClient.AbstractMcpServer.Abstracts;

public interface IAbstractMcpServer
{
    public Task<IList<McpClientTool>> GetToolsAsync();
}