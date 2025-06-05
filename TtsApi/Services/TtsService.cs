using Microsoft.CognitiveServices.Speech;
using Microsoft.EntityFrameworkCore;
using TtsApi.Data;
using TtsApi.Entities;
using TtsApi.Models;

namespace TtsApi.Services;

public class TtsService
{
    private readonly AppDbContext _db;
    private readonly SpeechConfig _config;

    public TtsService(AppDbContext db, SpeechConfig config)
    {
        _db = db;
        _config = config;
    }

    public async Task<string> SynthesizeAsync(TtsRequest req)
    {
        using var synthesizer = new SpeechSynthesizer(_config);
        var result = await synthesizer.SpeakTextAsync(req.Text);
        if (result.Reason != ResultReason.SynthesizingAudioCompleted)
            throw new Exception(result.ToString());

        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        var path = Path.Combine("Data", "output", $"tts_{timestamp}.wav");
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        await File.WriteAllBytesAsync(path, result.AudioData);
        return path;
    }

    public async Task<IEnumerable<object>> GetVoicesAsync(ListVoicesRequest req)
    {
        var voices = await _db.Voices.ToListAsync();
        if (!string.IsNullOrEmpty(req.Language))
            voices = voices.Where(v => v.Locale.StartsWith(req.Language, StringComparison.OrdinalIgnoreCase)).ToList();
        if (!string.IsNullOrEmpty(req.Gender))
            voices = voices.Where(v => v.Gender.Equals(req.Gender, StringComparison.OrdinalIgnoreCase)).ToList();
        voices = voices.OrderBy(v => v.ShortName).ToList();

        if (req.Detail == "low")
            return voices.Select(v => v.ShortName);
        return voices;
    }
}
