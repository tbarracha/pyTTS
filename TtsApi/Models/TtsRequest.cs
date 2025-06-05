namespace TtsApi.Models;
public class TtsRequest
{
    public string Text { get; set; } = "Hello, world!";
    public string Voice { get; set; } = "pt-PT-RaquelNeural";
    public bool Stream { get; set; } = false;
}
