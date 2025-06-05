using Microsoft.CognitiveServices.Speech;
using TtsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var speechKey = builder.Configuration["SpeechKey"];
var speechRegion = builder.Configuration["SpeechRegion"];
var speechConfig = SpeechConfig.FromSubscription(speechKey ?? "", speechRegion ?? "");
builder.Services.AddSingleton(speechConfig);

builder.Services.AddScoped<TtsService>();

var app = builder.Build();

app.MapControllers();

app.Run();
