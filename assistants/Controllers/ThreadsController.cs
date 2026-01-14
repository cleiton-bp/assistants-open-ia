using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/threads")]
public class ThreadsController : ControllerBase
{
    private readonly HttpClient _http;

    public ThreadsController()
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
    public async Task<IActionResult> Create()
    {
        var response = await _http.PostAsync("threads", new StringContent("{}", System.Text.Encoding.UTF8, "application/json"));
        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _http.GetStringAsync($"threads/{id}"));
    }
}
