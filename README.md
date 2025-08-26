# MusicQuiz

MusicQuiz is a Unity-based **quiz management** app built for party games and small gatherings. Rather than shipping copyrighted audio, it lets you create your own quizzes by referencing short music clips from URLs (MP3 files, YouTube links, etc.) and share them with friends. Each category collects several 15‑second snippets, and players must identify the correct title or source. The project aims to stay lightweight and data driven so anyone can extend it without touching the core code.

## Features
- Create and share quizzes by supplying external audio links rather than bundling media
- A single categories JSON file lists all categories, while each question has its own JSON file so users can extend the quiz by adding new category entries and question files
- Custom `ResourceManager` loads assets via key-based handles, providing caching and letting the game swap out resource providers without touching gameplay code
- Modular architecture that encourages contributions to questions, categories or entire gameplay modes

## Getting Started
1. Install [Unity](https://unity.com/) (2021 LTS or newer is recommended).
2. Clone this repository and open the `MusicQuiz/` folder with the Unity editor.
3. Play the sample scene and add your own categories or question files under `Assets/StreamingAssets`.

See the [CodexRules](CodexRules/README.md) folder for development rules and conventions.

## Project Structure
- `MusicQuiz/` – Unity project root
  - `Assets/`
    - `Musicmania/` – core quiz logic and assets
      - `Questions/` – JSON question definitions referencing audio URLs
      - `ResourceManagement/` – asset loading abstractions
      - Additional folders such as `Data/`, `UI/`, `SaveManagement/` and utilities
    - `StreamingAssets/` – `categories.json` and individual question files
  - `Packages/` and `ProjectSettings/` – Unity configuration files
- `MusicQuiz-Architecture.drawio` – diagram outlining the high‑level architecture
- `GamesMusicDifficulty.ods` – spreadsheet with question metadata

## Code Example
**Loading assets via the `ResourceManager`:**

```csharp
var imageHandle = context.ResourceManager.GetResource<Sprite>(questionData.ImageResourceKey);
var audioHandle = context.ResourceManager.GetResource<AudioClip>(questionData.AudioResourceKey);

var sprite = await imageHandle.LoadAsync(cancellationToken);
var clip = await audioHandle.LoadAsync(cancellationToken);

image.sprite = sprite;
audioSource.clip = clip;

imageHandle.Unload();
audioHandle.Unload();
```

---
This README provides an overview of the project. Feel free to expand it with additional details as the game evolves.
