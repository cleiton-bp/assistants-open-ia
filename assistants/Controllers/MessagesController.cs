using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using assistants.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/threads/{threadId}/messages")]
public class MessagesController : ControllerBase
{
    private readonly HttpClient _http;

    public MessagesController()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://api.openai.com/v1/")
        };

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        _http.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
    }

    [HttpPost]
    public async Task<IActionResult> Send(
        string threadId,
        [FromBody] CreateMessageDto dto)
    {
        var body = JsonSerializer.Serialize(new
        {
            role = "user",
            content = dto.Content
        });

        var response = await _http.PostAsync(
            $"threads/{threadId}/messages",
            new StringContent(body, Encoding.UTF8, "application/json")
        );

        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpGet]
    public async Task<IActionResult> List(string threadId)
    {
        return Ok(await _http.GetStringAsync($"threads/{threadId}/messages"));
    }
}
