using Microsoft.CognitiveServices.Speech;
using Microsoft.EntityFrameworkCore;
using TtsApi.Data;
using TtsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("tts"));

var speechKey = builder.Configuration["SpeechKey"];
var speechRegion = builder.Configuration["SpeechRegion"];
var speechConfig = SpeechConfig.FromSubscription(speechKey ?? "", speechRegion ?? "");
builder.Services.AddSingleton(speechConfig);

builder.Services.AddScoped<TtsService>();

var app = builder.Build();

app.MapControllers();

app.Run();
