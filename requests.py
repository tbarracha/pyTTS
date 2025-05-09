# requests.py
from pydantic import BaseModel
from enum import Enum
from typing import Optional

class TtsRequest(BaseModel):
    text: str
    voice: str = "pt-PT-RaquelNeural"
    stream: bool = False

class DetailLevel(str, Enum):
    low = "low"
    high = "high"

class ListVoicesRequest(BaseModel):
    language: Optional[str] = None
    gender: Optional[str] = None
    detail: DetailLevel = DetailLevel.high