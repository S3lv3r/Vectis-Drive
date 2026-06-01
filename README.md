# Vectis Drive | Assembly & Driving Simulator Prototype

Vectis Drive is a 3D simulation prototype developed in Unity. It combines a first-person mechanic workshop, a component-based truck assembly loop, and a functional driving scene utilizing Unity's physics engine. 

Rather than a finished commercial game, this repository serves as a technical vertical slice. It demonstrates the ability to integrate distinct gameplay systems first-person interaction, dynamic assembly validation, and vehicle locomotion into a complete, playable loop.

## ⚙️ Core Gameplay Loop

1. **Menu Navigation:** Start from the Main Menu.
2. **Workshop (First-Person):** Enter the garage to inspect the truck and interact with scattered parts.
3. **Assembly System:** Utilize raycast-based interactions to pick up, carry, and place missing components onto their designated sockets on the truck chassis.
4. **Validation:** Once the system detects the truck is fully assembled, the driving mode is unlocked.
5. **City Driving:** Transition to the urban environment to drive the assembled truck using a physics-based wheel setup and a functional cabin view.

## 🛠️ Technical Implementation

### 1. First-Person Interaction & Assembly (`TruckParts.cs` & `CamaraPlayer.cs`)
* **Raycast Interaction:** Detects interactable parts via center-screen raycasting, updating crosshair UI states contextually (Normal, Grab, Place).
* **State Management:** Manages two parallel truck hierarchies (`ON` for installed parts, `OFF` for placeholders/sockets). 
* **Kinematic Manipulation:** When grabbed, physics parts are temporarily set to kinematic to follow the player's view, and destroyed/replaced by the `ON` hierarchy upon correct placement.

### 2. Vehicle Locomotion (`CarController.cs`)
* **Physics Engine:** Built on Unity's `Rigidbody` and `WheelCollider` components for a three-axle configuration (Front, Middle, Rear).
* **Drivetrain Simulation:** Applies steering to the front axle and motor torque to the rear axles, smoothed via `Mathf.Lerp` for realistic heavy-vehicle acceleration.
* **Cabin Systems:** Features an active `DashboardController` mapping rigid velocity to the speedometer needle, and dynamic steering wheel rotation.

### 3. Real-time Rearview Mirrors
One of the key technical highlights of the cabin view. Instead of static textures, the mirrors use four independent `RenderTexture` assets (`LUP`, `LDOWN`, `RUP`, `RDOWN`) fed by auxiliary cameras, dynamically adjusting orientation based on the player's view relative to the mirror plane.

## 💻 Tech Stack & Assets

* **Engine:** Unity 2022.3.37f1
* **Language:** C#
* **UI:** Unity UI Canvas & TextMeshPro
* **Input:** Unity Legacy Input Manager
* **Assets:** Custom truck modeling/texturing mixed with third-party urban environment packs (e.g., POLYGON city pack).

## ⚠️ Known Limitations & Technical Debt

This project embraces radical transparency regarding its current architecture. The following limitations are acknowledged as areas for refactoring in a production environment:

* **String-Based Coupling:** The assembly logic heavily relies on naming conventions (the `OFF` suffix) rather than a robust, data-driven `ScriptableObject` architecture with unique IDs and sockets.
* **God Classes & Scene Transitions:** Major state changes rely on hard scene loads rather than a unified player state machine handling seamless vehicle entry/exit.
* **Input System:** Uses the Legacy Input Manager instead of the modern Unity Input System, limiting gamepad support and easy rebinding.
* **Hardcoded UI:** Menu coordinates are optimized for 1920x1080 and lack a fully abstracted responsive layout system.
* **Drivetrain Limitations:** While functional, the driving system does not yet simulate advanced heavy-truck mechanics like complex gear transmission, torque curves, or suspension elasticity.

## 🚀 How to Run

1. Clone the repository (Ensure LFS is enabled if required by the repo size).
2. Open the project in Unity Hub using version **2022.3.37f1**.
3. Open `Assets/Scenes/MainMenu.unity`.
4. Ensure `MainMenu`, `Taller`, and `City` are added to your Build Settings.
5. Press Play in the Editor or build for Windows Standalone.

> **Note on Repository Hygiene:** If cloning for the first time, ensure compilation artifacts (`Library/`, `Temp/`, `Builds/`) are ignored via your local `.gitignore`.