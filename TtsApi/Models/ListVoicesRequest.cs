namespace TtsApi.Models;
public class ListVoicesRequest
{
    public string? Language { get; set; } = "en";
    public string? Gender { get; set; } = "male";
    public string Detail { get; set; } = "high"; // "low" or "high"
}
