using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class GeminiController : ControllerBase
{
    private readonly IGeminiService _geminiService;

    public GeminiController(IGeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskGemini([FromBody] GeminiRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
            return BadRequest("Prompt is required.");

        try
        {
            var reply = await _geminiService.AskGeminiAsync(request.Prompt);
            return Ok(new { reply });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}

public class GeminiRequest
{
    public string Prompt { get; set; }
}
