# ARPoster
An interactive Augmented Reality (AR) experience built in Unity using AR Foundation. This project allows users to scan a poster and view 3D fruit models that appear in real space, animate, and display information when tapped.
| Username | Name |
|--------- | ------- |
| https-haley | Haley |
| AfroHat | Andre |
| timothypham045 | Tim |
| kaijafrierson | Kaija |

# Requirements #
- Mobile device with AR support
  - Android (ARCore)
- Unity 2022.3 LTS

# How to Use #
1. Scan the poster with your device
2. Watch fruits appear in 3D
3. Tap a fruit to display its name, price, and health benefits
4. Observe animations (rotation, floating, pop-up effects)

# Key Scripts #
| Script | Description |
|--------|-------------|
| `FruitSpawner.cs` | Spawns all 6 fruit prefabs in a 2x3 grid when the poster is detected |
| `FruitInfo.cs` | Detects tap via Physics.Raycast, stores fruit data, and loads FruitDetailScene |
| `FruitDataHolder.cs` | Static class that passes fruit name, price, and health info between scenes |
| `FruitDetailDisplay.cs` | Reads from FruitDataHolder and populates the detail screen UI automatically |
| `RotateFruit.cs` | Rotates each fruit model continuously at 40°/sec |
| `PopUp.cs` | Plays a scale-up animation when a fruit first spawns |
| `PandaARPlacer.cs` | Places the Panda in AR space via tap-to-place |
| `PandaController.cs` | Manages mic input, animation states, and text-to-speech |
| `GroceryAIChat.cs` | Sends user questions to OpenAI GPT-4.1-mini and returns the response |
| `VoiceController.cs` | Android STT/TTS bridge via custom .jar plugins |

## Fruit Data

| Fruit | Price | Key Health Benefits |
|-------|-------|---------------------|
| 🍎 Apple | $20 | Rich in fiber and antioxidants, supports heart health |
| 🍌 Banana | $20 | High in potassium and B6, boosts energy |
| 🥭 Mango | $16 | Packed with Vitamins A and C, supports immune function |
| 🍊 Orange | $15 | Excellent source of Vitamin C, reduces inflammation |
| 🍑 Peach | $12 | Supports eye and skin health, anti-inflammatory |
| 🍓 Strawberry | $21 | High in antioxidants, supports heart and brain health |

## Scenes
- **SampleScene** — main AR scene with poster tracking and fruit interaction
- **FruitDetailScene** — 2D detail screen showing fruit name, price, and health benefits
- **PandaScene** — AR scene with the voice-activated AI Panda assistant

# AR Fruit Poster #
<img width="2304" height="2880" alt="University of Arkansas (1)" src="https://github.com/user-attachments/assets/1b0be79e-9af3-4c98-8db9-3655fe42501c" />

## Project Structure

```
ARPoster/
├── Assets/
│   ├── Editor/                       ← Editor utility scripts
│   ├── ExtensionsAssets/             ← Additional asset extensions
│   ├── Humanoid Panda/               ← 3D panda model + animations
│   ├── Models/                       ← 3D fruit models
│   ├── Plugins/                      ← Android .jar plugins (STT/TTS)
│   ├── Resources/                    ← Prefabs, scripts, image library
│   ├── Scenes/                       ← SampleScene, FruitDetailScene, PandaScene
│   ├── Settings/                     ← URP render pipeline settings
│   ├── TextMesh Pro/                 ← TMP font and shader assets
│   ├── TutorialInfo/                 ← Unity tutorial assets
│   ├── XR/                           ← XR plugin settings
│   ├── LookAtCamera.cs               ← Makes objects face the camera
│   ├── MyMixer.mixer                 ← Audio mixer
│   └── New Animator Controller       ← Animation controller
├── FruitImages/                      ← Fruit poster reference images
├── FruitModels/                      ← Source fruit model files
├── Packages/                         ← Unity package dependencies
├── ProjectSettings/                  ← Unity project configuration
├── ARPoster.png                      ← AR tracking poster image
└── README.md
```

# Fruit Info Display #

# AI Chatbot 3D Panda #
1. Navigate to the Panda scence by pressing the "Panda Assistant" button
2. Tap a flat surface to place the Panda in AR space
3. Press the mic button and ask a grocery-related question
4. Listen to the Panda's spoken response

## Third-Party Assets & Dependencies

- **Humanoid Panda** — 3D model and animations (idle, talking)
- **3 Android .jar plugins** — custom speech recognition and text-to-speech bridge
- **OpenAI GPT-4.1-mini API** — powers the Panda assistant responses
- **AR Foundation / ARCore** — Unity's cross-platform AR framework

# Demo #
