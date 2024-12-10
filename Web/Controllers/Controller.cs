namespace FinalExam.Controllers;

using System.Text;
using System.Text.Json;
using Contracts;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("tickets")]
public class Controller : ControllerBase
{
    private static string Key =
        "sk-svcacct-df9hvCianVt-JF2wdW5Vx_tc2G1yEU12UbRVVpE6c43Cs-N2Fl_MgSsvwGATeT3BlbkFJZMkElNSlRFRYTYKwqZZpBNzFowIAbqc-fL0gsjIfnq7yvVOPg2CewNZWy-3AA";

    /// <summary>
    /// Open new ticket with a question to IT Support
    /// </summary>
    [HttpPost("open")]
    public async Task<ActionResult<TicketContract>> OpenTicket([FromBody] OpenTicketRequest request)
    {
        return Ok(new TicketContract()
        {
            TicketId = "ticket1",
            Dialog = [
                new TicketMessage() { FromUser = true, Content = request.Question },
                new TicketMessage() { FromUser = false, Content = await AskChatGPTAsync(request.Question) },
            ]
        });
    }

    /// <summary>
    ///  Reply to Specifc ticket
    /// </summary>
    [HttpPost("{id}/reply")]
    public async Task<ActionResult<TicketContract>> OpenTicket([FromBody] ReplyToTicketRequest request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///  Get all my tickets
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TicketContract>>> AllTickets()
    {
        throw new NotImplementedException();
    }

    private async Task<string> AskChatGPTAsync(string question)
    {
        var apiUrl = "https://api.openai.com/v1/chat/completions";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Key}");

        // if you want to add a chatGPT message to context, use 'assistant' role
        var payload = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new
                {
                    role = "system",
                    content =
                        "You are sarcastic IT-support guy that don't really want to work a lot. Keep answers short"
                },
                new { role = "user", content = question }
            },
            temperature = 0.7
        };

        var jsonPayload = JsonSerializer.Serialize(payload);

        HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var parsedResponse = JsonSerializer.Deserialize<JsonDocument>(responseContent);

        var assistantMessage = parsedResponse
            ?.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return assistantMessage ?? "No response from ChatGPT.";
    }
}