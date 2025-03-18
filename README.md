# Sprint Style Christmas Game

## Description
Our game is a sprint-style game with Christmas elements. Players must navigate the environment while avoiding obstacles, destroying moving snowmen, collecting Santas, and fighting robot enemies that try to prevent them from reaching the final checkpoint.

## Contributors
This project was developed by **Jonathan Lopez** and **Christopher Poche**, with both contributing equally to all aspects of design, development, and implementation, including:
- **Game Mechanics & Programming** – Implementing player controls, enemy behaviors, and game logic.
- **UI & UX Design** – Designing and integrating user interface elements like HUDs, settings, and game status tracking.
- **Audio Integration** – Implementing background music, sound effects, and custom audio triggers.
- **3D Assets & Environment Setup** – Integrating third-party assets and creating interactive environments.
- **Testing & Debugging** – Identifying and fixing gameplay issues, including UI inconsistencies and game logic errors.

## Features
- **Win and Loss Conditions** – The player must reach the final checkpoint while avoiding enemies.
- **UI Tracking System**:
  - Displays the number of targets destroyed.
  - Tracks the number of Santas collected.
- **Custom Audio System**:
  - Unique sound effect when collecting all Santas.
  - Cannonball fire sound effects.
- **Settings Manager** – Adjust in-game settings via UI.

## How to Build and Run the Game
### Unity Version
This project was built using **Unity 2022.3.15f1 (DX11)**. Ensure you have this version or a compatible one installed for the best experience.

### Building in Unity
1. Open **Unity Hub** and select the project.
2. In **Unity Editor**, navigate to **File → Build Settings**.
3. Ensure the correct platform is selected (e.g., Windows, macOS, WebGL, etc.).
4. Set the target resolution and graphics settings as needed.
5. Click **Build**, then choose a folder to save the final game files.

### Running the Game
1. Once the build is complete, navigate to the output folder.
2. Locate the executable file (`.exe` for Windows, `.app` for macOS, or a WebGL folder for web builds).
3. Double-click the executable to launch the game.
4. If on Windows, ensure all game files stay in the same directory as the `.exe` file to avoid missing assets.

## Controls
| Action         | Key/Button |
|---------------|------------|
| Move          | WASD |
| Interact      | E |
| Aim           | Mouse Cursor |
| Jump          | Space Bar |
| Fire Cannon   | Left Click |

## Key Scripts
### GameManager.cs
Handles the overall game logic, including win/loss conditions, tracking UI elements, and game resets.

### PlayerCharacter.cs
Controls player movement, interactions, damage mechanics, and respawning.

### VictoryManager.cs
Displays the Victory Screen when the player reaches the final checkpoint.

### GameOverManager.cs
Triggers the Game Over Screen when the player runs out of lives.

### SettingsManager.cs
Manages in-game settings, including audio and game preferences.

### BackgroundMusicManager.cs
Handles looping background music and allows volume adjustments.

## Attributions
### Audio
- **Background Music:** [Pixabay - Game Music Player Console 8bit](https://pixabay.com/music/upbeat-game-music-player-console-8bit-background-intro-theme-297305/)
- **Cannon Sounds:** [OpenGameArt - Old Cannon](https://opengameart.org/content/old-canon)
  - Licensed under **CC BY 3.0** [License](http://creativecommons.org/licenses/by/3.0/)
  - Hosted on OpenGameArt
- **Custom Audio:** Christopher Poche

### 3D Models & Environment
- **Santa and Snowmen:** [Unity Asset Store - Low Poly Christmas Pack](https://assetstore.unity.com/packages/3d/characters/low-poly-christmas-pack-santa-claus-181035)
- **Environment:** [Unity Asset Store - RPG FPS Industrial Set](https://assetstore.unity.com/packages/3d/environments/industrial/rpg-fps-game-assets-for-pc-mobile-industrial-set-v2-0-86679?srsltid=AfmBOor8O69wEvxU-SK-aqqcaID0s44bLt5XxJL7LCzevZ2oGvTgRlQH)

## Known Issues
- **Wall Jump Exploit:** Players can wall jump outside the map, leading to infinite falling.

🚨 No planned future updates.

