
# pyTTS – FastAPI Text-to-Speech (Edge-TTS)

**pyTTS** is a simple FastAPI-based Text-to-Speech service using Microsoft's Edge TTS engine. It supports:
- Voice synthesis with streaming or saved file modes
- Voice listing with filtering by language, gender, and detail level

---

## 🔊 Features

- 🎙️ Generate speech from text via `/tts`
- 🌍 Get available voices with filters using `/voices`
- ⚡ Stream audio or download it
- 🧩 Simple, local, and Pythonic

---

## 🧪 API Usage

### 🎤 `POST /tts`

Convert text to speech.

#### Request JSON
```json
{
  "text": "Olá, tudo bem?",
  "voice": "pt-PT-RaquelNeural",
  "stream": false
}
````

* `text`: (required) The text to synthesize
* `voice`: (optional) Voice short name (default: `"pt-PT-RaquelNeural"`)
* `stream`: (optional) If `true`, streams audio instead of saving to file

#### Response

* `stream = true`: `audio/mpeg` streaming response
* `stream = false`: MP3 file download

---

### 🗣️ `POST /voices`

List available voices with optional filters.

#### Request JSON

```json
{
  "language": "pt",
  "gender": "Female",
  "detail": "low"
}
```

* `language`: Optional ISO locale prefix like `"pt"`, `"en"`, `"fr"`
* `gender`: Optional filter (`"Male"`, `"Female"`)
* `detail`: `"low"` returns just `ShortName`, `"high"` returns full voice objects

#### Response

```json
[
  "pt-PT-RaquelNeural",
  "pt-BR-FranciscaNeural"
]
```

---

## ⚙️ Installation & Running Locally

### ✅ Requirements

* Python 3.10 or higher

### 🛠️ Setup

1. **Clone the repo**

```bash
git clone https://github.com/YOUR_USERNAME/pyTTS.git
cd pyTTS
```

2. **Create a virtual environment**

```bash
python -m venv .venv
```

3. **Activate the environment**

* PowerShell:

```bash
. .venv\Scripts\Activate.ps1
```

* Bash/macOS:

```bash
source .venv/bin/activate
```

4. **Install dependencies**

```bash
pip install -r requirements.txt
```

5. **Run the app**

```bash
python main.py
```

Visit [http://localhost:8000/docs](http://localhost:8000/docs) to test the API.

---

## 🧠 Credits

Built using:

* [FastAPI](https://fastapi.tiangolo.com/)
* [Edge-TTS](https://github.com/rany2/edge-tts)

---

## 📄 License

MIT License. Use freely and fork responsibly.