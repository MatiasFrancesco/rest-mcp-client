using MCPClient.AbstractMcpServer.Abstracts;
using MCPClient.Chat.ReadModel.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ModelContextProtocol.Client;


namespace MCPClient.Chat.Facade.endpoints;

public static class ChatEndpoints
{
    public static IEndpointRouteBuilder MapChatEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/chat")
            .WithTags("Chat");

        group.MapGet("/test", HandleTestChatConnection)
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/tools", HandleGetTools)
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("/tools/response", HandleChatMessage)
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);


        return group;
    }

    private static async Task<IResult> HandleTestChatConnection(IChatService chatService)
    {
        var response = await chatService.TestChatConnection();
        return Results.Ok(response);
    }

    private static async Task<IResult> HandleGetTools(IAbstractMcpServer abstractMcpServer)
    {
        try
        {
            IList<McpClientTool> tools = await abstractMcpServer.GetToolsAsync();
            var message = "";
            foreach (McpClientTool tool in tools)
            {
                message = $"Tool Name: {tool.Name}, Description: {tool.Description}";
            }

            return Results.Ok(message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> HandleChatMessage(
        string prompt,
        IChatService chatService,
        IAbstractMcpServer abstractMcpServer)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return Results.BadRequest("Prompt cannot be empty.");
            }

            IList<McpClientTool> tools = await abstractMcpServer.GetToolsAsync();
            var response = await chatService.GetResponseWithToolsAsync(prompt, tools);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}