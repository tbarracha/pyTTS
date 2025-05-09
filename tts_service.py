#tts_service.py

import edge_tts
from datetime import datetime
from pathlib import Path
from typing import AsyncGenerator, Union, List, Dict, Optional
from edge_tts import list_voices

AUDIO_DIR = Path("data/audio/output")
AUDIO_DIR.mkdir(parents=True, exist_ok=True)

async def synthesize_speech(
    text: str,
    voice: str = "pt-PT-RaquelNeural",
    stream: bool = False
) -> Union[Path, AsyncGenerator[bytes, None]]:
    communicate = edge_tts.Communicate(text=text, voice=voice)

    if not stream:
        # Non-streaming mode: Save and return path
        timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
        output_file = AUDIO_DIR / f"tts_{timestamp}.mp3"
        await communicate.save(str(output_file))
        return output_file

    # Streaming mode: yield raw audio chunks
    async def generator():
        async for chunk in communicate.stream():
            if chunk["type"] == "audio":
                yield chunk["data"]

    return generator()

async def get_available_voices(
    proxy: Optional[str] = None,
    language: Optional[str] = None,
    gender: Optional[str] = None,
    detail: str = "high"
):
    voices = await list_voices(proxy=proxy)

    if language:
        voices = [v for v in voices if v["Locale"].lower().startswith(language.lower())]

    if gender:
        voices = [v for v in voices if v["Gender"].lower() == gender.lower()]

    voices = sorted(voices, key=lambda v: v["ShortName"])

    if detail == "low":
        return [v["ShortName"] for v in voices]

    return voices
