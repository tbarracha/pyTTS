namespace TtsApi.Entities;
public class Voice
{
    public int Id { get; set; }
    public string ShortName { get; set; } = null!;
    public string Locale { get; set; } = null!;
    public string Gender { get; set; } = null!;
}
