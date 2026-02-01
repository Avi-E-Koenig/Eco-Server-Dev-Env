# Fridge mod: tools and environment setup

Step-by-step setup for developing an Eco mod that adds a **placeable fridge** (WorldObject with 3D mesh). A fridge requires the full ModKit + Unity workflow; see [eco-modding-assets-and-world-objects.md](eco-modding-assets-and-world-objects.md).

---

## 1. Prerequisites

- **Eco account** — needed to download the ModKit from [play.eco/account](https://play.eco/account).
- **Eco installed** (e.g. via Steam) — you already have a server at `Eco_Data\Server`; the same game build is the target for the mod.

---

## 2. Downloads (in order)

| Step | What | Where | Notes |
|------|------|--------|--------|
| 2.1 | **Visual Studio 2022 Community** | [visualstudio.microsoft.com/downloads](https://visualstudio.microsoft.com/downloads/) | Free. Install workload **".NET desktop development"** (or "Game development with Unity" if you want VS to open Unity C# scripts). |
| 2.2 | **Eco ModKit** | [play.eco/account](https://play.eco/account) → ModKit in downloads | Download and **unzip** to a folder (e.g. `EcoModKit`). Do **not** install Unity before checking the ModKit version. |
| 2.3 | **Unity** | [unity3d.com/get-unity/download/archive](https://unity3d.com/get-unity/download/archive) | Open **ProjectVersion.txt** inside the unzipped ModKit folder. Install the Unity version that matches: **year and first number exactly**, third number same or higher (e.g. `2020.3.x`). |
| 2.4 | **Collections package** | Installed later inside Unity (Package Manager) | Required by the ModKit; see step 4. |

---

## 3. Create a Unity project for the fridge mod

1. Open **Unity Hub** (installed with Unity).
2. **New project** → choose the Unity version you installed in 2.3.
3. Template: **3D**.
4. Project name and path (e.g. `EcoFridgeMod`, any folder). Create.

---

## 4. Install the ModKit in the Unity project

1. In Unity: **Assets** → **Import Package** → **Custom Package...**.
2. Browse to the **unzipped ModKit folder** and select **EcoModKit.unitypackage** → Open.
3. In the import dialog, leave defaults (or select All) → **Import**.
4. **Window** → **Package Manager**.
5. Top-left: switch from "In Project" to **Unity Registry**.
6. Find **Collections** → **Install**.
   - **Unity 2019.x:** If Collections is missing, enable "Show preview packages" (Advanced) in Package Manager, then install Collections.
   - **Unity 2020.x:** If Collections is hidden, use **+** → **Add package from git URL** → `com.unity.collections` → Add.

Reference: [Installing the ModKit](https://wiki.play.eco/en/Installing_the_ModKit).

---

## 5. Optional: Visual Studio as Unity's C# editor

1. In Unity: **Edit** → **Preferences** (or **Settings** on Mac) → **External Tools**.
2. **External Script Editor:** set to **Visual Studio 2022** (or **Visual Studio Code** if you prefer).
3. Double-clicking a C# script in Unity will then open VS; you can build and debug from there.

---

## 6. Verify the environment

- **Unity:** Project opens without errors; ModKit assets/menus (e.g. VoxelEngine, Eco-related items) appear if the package added them.
- **Visual Studio:** Open a `.cs` script from the Unity project; it loads and IntelliSense works (Unity may need to regenerate project files once: focus Unity and wait).
- **Server:** Your existing Eco server (`Eco_Data\Server`) will later receive the mod (exported package or copied build output); no change needed for this setup step.

---

## 7. What you have after setup

- **Visual Studio 2022** — edit C# for server-side fridge logic (and any ModKit C# in the Unity project).
- **Unity** — one 3D project with ModKit and Collections installed, ready for:
  - Fridge **prefab** (3D model, materials, colliders) and scene placement.
  - ModKit **export** (e.g. ModExporter) to produce the mod package for server and clients.

The **fridge implementation** itself (server: `FridgeObject`, `FridgeItem`, Recipe + components; client: prefab with matching name; export and deploy) is the next phase after this setup. Use [eco-modding-assets-and-world-objects.md](eco-modding-assets-and-world-objects.md) (Placeable WorldObjects, Pipeline, References) and the [EcoModKit examples](https://github.com/StrangeLoopGames/EcoModKit/tree/main/Examples) (e.g. Flag, CornOnTheCob) when you implement the fridge.

---

## Summary checklist

| Phase | Action |
|-------|--------|
| Download | VS 2022 Community, ModKit (play.eco/account), Unity (version from ModKit ProjectVersion.txt) |
| Unity project | New 3D project → Import EcoModKit.unitypackage → Package Manager: install Collections |
| Optional | Set VS 2022 as Unity External Script Editor |
| Next | Implement fridge (Object + Item + Recipe + prefab), then export and test on server |
