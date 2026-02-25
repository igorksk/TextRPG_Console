# TextRPG

Small console-based post-apocalyptic text RPG written in C# targeting .NET 8.

## Requirements

- .NET 8 SDK
- Any code editor or IDE that supports .NET (Visual Studio, Rider, VS Code)

## Run

From repository root:

- dotnet run --project TextRPG/TextRPG.csproj

Or:

- cd TextRPG
- dotnet run

## Gameplay

- Menu-driven console game. Options include exploring, resting, checking inventory, and exiting.
- Exploring consumes food and water and may produce random events: finding items, encountering enemies, or finding resources.

## Project structure

- `TextRPG/Program.cs` — entry point
- `TextRPG/Game.cs` — game loop and UI
- `TextRPG/World.cs` — locations, events, and item generation
- `TextRPG/Player.cs` — player state and actions
- `TextRPG/Item.cs` — item types (food, water, weapon, medicine)

## Notes

All in-game text is in English. Adjustments and translations can be made in the `.cs` files under the `TextRPG` folder.

License: Unspecified
