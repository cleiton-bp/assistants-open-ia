using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using assistants.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/threads/{threadId}/runs")]
public class RunsController : ControllerBase
{
    private readonly HttpClient _http;

    public RunsController()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://api.openai.com/v1/")
        };

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                Environment.GetEnvironmentVariable("OPENAI_API_KEY")
            );

        _http.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
    }

    [HttpPost]
    public async Task<IActionResult> CreateRun(
        string threadId,
        [FromBody] CreateRunDto dto)
    {
        var body = new
        {
            assistant_id = dto.AssistantId
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _http.PostAsync(
            $"threads/{threadId}/runs",
            content
        );

        var result = await response.Content.ReadAsStringAsync();

        var json = JsonDocument.Parse(result);

        return Ok(new
        {
            runId = json.RootElement.GetProperty("id").GetString(),
            status = json.RootElement.GetProperty("status").GetString()
        });
    }

    [HttpGet("{runId}")]
    public async Task<IActionResult> GetRunStatus(
        string threadId,
        string runId)
    {
        var response = await _http.GetAsync(
            $"threads/{threadId}/runs/{runId}"
        );

        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }
}
