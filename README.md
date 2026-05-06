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


# AR Fruit Poster #
<img width="1580" height="950" alt="ARPoster_Figure1" src="https://github.com/user-attachments/assets/99b9194e-f3ce-4dd2-9b9e-4bb8e7c4a5a9" />

# Fruit Info Display #

# AI Chatbot 3D Panda #
1. Navigate to the Panda scence by pressing the "Panda Assistant" button
2. Tap a flat surface to place the Panda in AR space
3. Press the mic button and ask a grocery-related question
4. Listen to the Panda's spoken response

# Demo #
