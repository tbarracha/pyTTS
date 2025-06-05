using Microsoft.AspNetCore.Mvc;
using TtsApi.Models;
using TtsApi.Services;

namespace TtsApi.Controllers;

[ApiController]
[Route("")]
public class TtsController : ControllerBase
{
    private readonly TtsService _service;

    public TtsController(TtsService service)
    {
        _service = service;
    }

    [HttpPost("tts")]
    public async Task<IActionResult> Tts([FromBody] TtsRequest req)
    {
        try
        {
            var path = await _service.SynthesizeAsync(req);
            if (req.Stream)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                return File(bytes, "audio/wav");
            }
            return PhysicalFile(path, "audio/wav", Path.GetFileName(path));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("voices")]
    public async Task<IActionResult> Voices([FromBody] ListVoicesRequest req)
    {
        var voices = await _service.GetVoicesAsync(req);
        return Ok(voices);
    }
}
