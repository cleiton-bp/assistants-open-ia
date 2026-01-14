using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using assistants.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/assistants")]
public class AssistantsController : ControllerBase
{
    private readonly HttpClient _http;

    public AssistantsController()
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
    public async Task<IActionResult> Create([FromBody] CreateAssistantDto dto)
    {
        var body = JsonSerializer.Serialize(new
        {
            name = dto.Name,
            instructions = dto.Instructions,
            model = dto.Model
        });

        var response = await _http.PostAsync(
            "assistants",
            new StringContent(body, Encoding.UTF8, "application/json")
        );

        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _http.GetStringAsync("assistants"));
    } 

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _http.GetStringAsync($"assistants/{id}"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateAssistantDto dto)
    {
        var body = JsonSerializer.Serialize(new
        {
            name = dto.Name,
            instructions = dto.Instructions
        });

        var response = await _http.PostAsync(
            $"assistants/{id}",
            new StringContent(body, Encoding.UTF8, "application/json")
        );

        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _http.DeleteAsync($"assistants/{id}");
        return NoContent();
    }
}
