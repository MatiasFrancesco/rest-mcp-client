# MCP Client with Azure AI Agent

A C# MCP (Model Context Protocol) client that integrates with Azure AI Agent to provide intelligent assistant
capabilities across multiple MCP servers. The application dynamically discovers and uses tools from various MCP servers
configured through `appsettings.json`.

## Overview

This project implements a sophisticated MCP client that can connect to multiple MCP servers simultaneously, convert
their tools into Azure AI-compatible functions, and provide an intelligent chat interface powered by Azure AI Agent. The
application automatically discovers available tools from all configured servers and makes them available to the AI
assistant.

## Features

- **Multi-Server Support**: Connect to multiple MCP servers simultaneously
- **Azure AI Agent Integration**: Leverages Azure AI services for intelligent responses
- **Dynamic Tool Discovery**: Automatically discovers and registers tools from all connected MCP servers
- **Configuration-Driven**: Server connections managed through `appsettings.json`
- **Tool Conversion**: Seamlessly converts MCP tools to Azure AI-compatible functions
- **Robust Error Handling**: Comprehensive error handling for server connections and tool execution

## Architecture

```
+-------------------+         +-------------------+         +-------------------+
|   Azure AI Agent  | <-----> |     MCP Client    | <-----> |   MCP Server(s)   |
|  (Intelligent     |         |  (This Project)   |         |  (Tools & Data)   |
|   Assistant)      |         |                   |         |                   |
+-------------------+         +-------------------+         +-------------------+
```

- **Azure AI Agent**: Provides intelligent chat and tool orchestration.
- **MCP Client**: Connects to multiple MCP servers, discovers tools, converts them to Azure AI-compatible functions, and
  manages communication between the agent and servers.
- **MCP Servers**: Host various tools and data sources, each with their own endpoints and capabilities.

The MCP Client acts as a bridge, dynamically discovering and exposing tools from all configured MCP servers to the Azure
AI Agent, enabling seamless tool invocation and intelligent responses.

## Configuration

Configure your MCP servers in `appsettings.json`:

 ```json
 {
    "McpServers": [
      {
         "name": "server1",
         "endpoint": "mcp server endpoint"
      }
    ],
   "AzureOpenAi": {
     "ApiKey": "your-azure-api-key",
     "Endpoint": "endpoint-of-your-azure-openai-service",
     "DeploymentName": "your-agent-model-name"
   }
 }
 ```

## Key Components

### GetResponseWithToolsAsync

Main orchestration method that:

- Aggregates tools from all connected MCP servers
- Converts McpClientTool objects to AIFunction objects
- Configures Azure AI Agent with available tools
- Returns intelligent responses using discovered tools

### Multi-Server Tool Management

- Connects to multiple MCP servers based on configuration
- Handles tool name conflicts across servers
- Maintains server health and reconnection logic
- Provides unified tool interface to Azure AI Agent

## Usage

```c#
// The client automatically loads server configuration from appsettings.json
var mcpClient = new McpClient(configuration);
await mcpClient.InitializeAsync();

// Get response using tools from all connected servers
var response = await mcpClient.GetResponseWithToolsAsync(
"What tools are available and can you help me with beer information?"
);
```

## Tool Discovery Process

- **Server Connection**: Connect to all enabled MCP servers from configuration
- **Tool Enumeration**: Discover available tools from each server
- **Tool Conversion**: Convert MCP tools to Azure AI-compatible functions
- **Registration**: Register all tools with Azure AI Agent
- **Execution**: AI Agent can invoke any tool from any connected server

## Dependencies

- Microsoft.Extensions.AI: Core AI framework
- Azure.AI: Azure AI services integration
- Microsoft.Extensions.Configuration: Configuration management
- MCP Protocol Libraries: Model Context Protocol implementation

## Setup

1. Clone the repository:

    ```bash
    git clone https://github.com/MatiasFrancesco/mcp-client.git
    cd mcp-client
    ```

    2. Configure `appsettings.json` with your MCP servers and Azure AI credentials
       in the `MCPClient` project directory. Ensure you have the correct endpoints and API keys for your Azure AI
       services.


3. Install dependencies:

    ```bash
    dotnet restore
    ```

4. Run the application:

    ```bash
    dotnet run --project MCPClient/MCPClient.csproj
    ```

## Error Handling

The application provides robust error handling for:

- MCP server connection failures
- Tool conversion errors
- Azure AI Agent communication issues
- Individual tool execution failures
- Server disconnection and reconnection

## Branch Information

Currently on the `custom-mcp-client` branch with a custom MCP client implementation for Azure AI Agent integration.

## Contributing

When contributing:

- Ensure new MCP servers are properly configured in `appsettings.json`
- Maintain type safety in tool conversions
- Add appropriate error handling for new features
- Test with multiple MCP server configurations
- Follow the existing patterns for server management
