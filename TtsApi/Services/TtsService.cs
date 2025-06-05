using Microsoft.CognitiveServices.Speech;
using TtsApi.Models;

namespace TtsApi.Services;

public class TtsService
{
    private readonly SpeechConfig _config;

    public TtsService(SpeechConfig config)
    {
        _config = config;
    }

    public async Task<string> SynthesizeAsync(TtsRequest req)
    {
        _config.SpeechSynthesisVoiceName = req.Voice;
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
        using var synthesizer = new SpeechSynthesizer(_config);
        var result = await synthesizer.GetVoicesAsync(req.Language ?? string.Empty);
        if (result.Reason != ResultReason.VoicesListRetrieved)
            throw new Exception(result.ErrorDetails);

        var voices = result.Voices.AsEnumerable();
        if (!string.IsNullOrEmpty(req.Gender))
            voices = voices.Where(v => v.Gender.Equals(req.Gender, StringComparison.OrdinalIgnoreCase));
        voices = voices.OrderBy(v => v.ShortName);

        if (req.Detail == "low")
            return voices.Select(v => v.ShortName);
        return voices.Select(v => new { v.ShortName, v.Locale, v.Gender });
    }
}
