from fastapi import FastAPI
from fastapi.responses import StreamingResponse, FileResponse, JSONResponse
from fastapi import Request
from requests import TtsRequest, ListVoicesRequest
from tts_service import synthesize_speech, get_available_voices

app = FastAPI()

@app.post("/tts")
async def tts(req: TtsRequest):
    try:
        result = await synthesize_speech(text=req.text, voice=req.voice, stream=req.stream)
        if req.stream:
            return StreamingResponse(result, media_type="audio/mpeg")
        else:
            return FileResponse(result, media_type="audio/mpeg", filename=result.name)
    except Exception as e:
        return JSONResponse(status_code=500, content={"error": str(e)})

@app.post("/voices")
async def list_voices(req: ListVoicesRequest):
    try:
        voices = await get_available_voices(
            language=req.language,
            gender=req.gender,
            detail=req.detail
        )
        return voices
    except Exception as e:
        return JSONResponse(status_code=500, content={"error": str(e)})
