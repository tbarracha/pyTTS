# TtsApi (.NET)

This project replicates the simple text-to-speech API implemented in the Python `pyTTS` project. It uses ASP.NET Core 6 and the Microsoft Cognitive Services Speech SDK.

## Endpoints

- `POST /tts` – synthesize text to speech. Accepts JSON body:

```json
{
  "text": "Olá, tudo bem?",
  "voice": "pt-PT-RaquelNeural",
  "stream": false
}
```

Returns an audio file. When `stream` is `true` the file bytes are streamed in the response.

- `POST /voices` – list available voices from the Speech service. Accepts filters for language, gender and detail (`"low"` only returns the voice short names).

## Running

1. Ensure [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) is installed.
2. Restore dependencies:

```bash
dotnet restore
```

3. Run the API:

```bash
dotnet run
```

Use `SpeechKey` and `SpeechRegion` environment variables (or appsettings) to configure the Microsoft Speech service.
