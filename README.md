# Eco Server – Mod Development Environment

This project is a **development environment for building and testing mods** for the game [Eco](https://play.eco/). It runs the Eco dedicated server in Docker so you can develop custom mods (e.g. in `Mods/UserCode`), tweak configs, and test world generation and gameplay without affecting a production server.

---

## What’s in this repo

| Path | Purpose |
|------|--------|
| **Configs/** | Server settings (`.eco` / `.template`). Edit world size, difficulty, etc. |
| **Mods/** | Game data + your mods. Put custom code in **Mods/UserCode**. |
| **dev-files/** | Scripts, Docker setup, and docs. **docker-compose** and **.env** live here. |
| **Storage/** | Runtime only (ignored by Git). Saves, world DB. Wipe to reset the world. |

The Linux server binary (`EcoServer`), `linux64/`, `WebClient/`, and `Mods/__core__` are **not** in the repo; you obtain them via the setup below (SteamCMD + copy script).

---

## Prerequisites

- **Docker Desktop** installed and running.
- **Eco server token** (optional but recommended): [play.eco/en/account](https://play.eco/en/account) → Server Authentication → Generate Token.

---

## Setup: install and run from scratch

Follow these steps to get a working server and create a world from scratch.

### 1. Clone or open the repo

Ensure you have this repo (with at least `Configs/`, `Mods/`, `dev-files/`). The Server root is the folder that contains `Configs`, `Mods`, and `dev-files`.

### 2. Download the Eco server (Linux) via SteamCMD

Docker runs Linux, so the server must be the Linux build. Use the provided script (requires Docker):

```powershell
cd dev-files\scripts
.\download-eco-server-steamcmd.ps1
```

This downloads the Eco dedicated server (Steam app 739590) into **dev-files/eco-server-download** (staging). It may take several minutes.

### 3. Copy server files into the Server root

Copy the binary, Steam libs, `Mods/__core__`, and `WebClient` from staging into the Server root. Your existing **Configs** and **Storage** are left unchanged.

```powershell
.\copy-eco-staging-to-server.ps1
```

Run this from **dev-files\scripts**. After it finishes, the Server root will contain `EcoServer`, `linux64/`, `Mods/__core__`, and `WebClient/`.

### 4. Configure environment

```powershell
cd ..\dev-files
copy .env.example .env
```

Edit **.env** and set your Eco server token (optional):

```env
ECO_TOKEN=your_token_here
```

### 5. Ensure Storage exists

The server expects a **Storage** folder at the Server root (it holds the world save). If it doesn’t exist:

```powershell
cd ..   # back to Server root
mkdir Storage
```

### 6. (Optional) Adjust world generation

Before generating a world, you can edit **Configs/WorldGenerator.eco**:

- **Seed**: `0` is a safe default; change it for a different world layout.
- **MapSizePreset** / **Dimensions**: e.g. `"Small"` with 72×72, or `"Medium"` with 100×100. Dimensions must be equal and divisible by 4.
- Changes to **WorldGenerator.eco** only apply when a **new** world is created (see “Create a world from scratch” below).

### 7. Start the server

From **dev-files** (where `docker-compose.yml` lives):

```powershell
docker compose up -d
docker compose logs -f
```

Follow the logs until world generation and plugin init complete. Then you can connect:

- **Game:** UDP port **3000**
- **Web UI:** [http://localhost:3001](http://localhost:3001)

To stop:

```powershell
docker compose down
```

---

## Create a world from scratch

If you want a **new** world (e.g. after changing **WorldGenerator.eco** or to wipe all progress):

1. **Stop the server** (from **dev-files**):
   ```powershell
   docker compose down
   ```

2. **Wipe Storage** (keeps the folder, deletes all world data):
   ```powershell
   cd ..   # Server root
   Remove-Item -Path ".\Storage\*" -Recurse -Force
   ```

3. **Start the server** again:
   ```powershell
   cd dev-files
   docker compose up -d
   ```

The server will generate a new world on first boot. If you changed **WorldGenerator.eco**, the new world will use those settings (seed, map size, etc.).

**Tip:** If a new world has only one biome or almost no trees, try a different **Seed** (e.g. `0`) or a larger map (e.g. Medium 100×100) in **Configs/WorldGenerator.eco**, then reset the world again with the steps above.

---

## Developing mods

- Put your mod code in **Mods/UserCode** (e.g. new C# assemblies, overrides).
- The server uses **Mods/__core__** from the SteamCMD download; don’t commit that folder if you want a smaller repo (see **.gitignore**).
- Rebuild/restart the server after code changes as needed. See **dev-files/ai-context/** for modding guides (e.g. overrides, assets, conventions).

---

## Useful commands

| Command | Description |
|--------|-------------|
| `docker compose up -d` | Start server (from **dev-files**) |
| `docker compose down` | Stop server |
| `docker compose logs -f` | Follow logs |
| `docker compose restart` | Restart server (world not reset) |

---

## More detail

- **dev-files/ai-context/developer-setup-quick-reference.md** – Short setup checklist.
- **dev-files/ai-context/developer-setup-docker-and-server.md** – Docker, ports, troubleshooting.
- **dev-files/ai-context/kb-reset-world.md** – Full world-reset steps and backup options.
- **dev-files/ai-context/kb-world-generator-single-biome-no-trees.md** – Fixing single-biome / no-trees worlds.
- **dev-files/ai-context/setting-up-server-wiki.md** – How this aligns with the official Eco server setup.
