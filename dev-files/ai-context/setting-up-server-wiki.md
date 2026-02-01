# Setting up a server (wiki-based)

Summary of the [official Eco wiki: Setting Up a Server](https://wiki.play.eco/en/Setting_Up_a_Server). Use this to build a **fresh** server the intended way.

---

## Three ways to get server files

| Method | Source | Notes |
|--------|--------|--------|
| **Steam (Library → Tools)** | Install "Eco Server" from Steam | Easiest if you own Eco on Steam. Installs to `Steam\steamapps\common\Eco Server`. Run **EcoServer.exe** (Windows). |
| **SteamCMD** | `app_update 739590 validate` | Cross‑platform. Download to a folder, then **copy that folder** to where you host (do not run from the SteamCMD folder). |
| **Without Steam** | [play.eco](https://play.eco/) → Account → Server .zip | Download .zip, unzip into an **empty folder**. Run **EcoServer.exe**. Manual updates. |

---

## Fresh server via SteamCMD (recommended for Linux/Docker)

The wiki says:

1. Set install directory: `force_install_dir ./Eco_Server` (or your path).
2. Login: `login anonymous`.
3. Download: `app_update 739590 validate`.
4. **Do not run the server from inside the SteamCMD folder.** Copy the downloaded folder to the location where you want to host, then run from there.

For our setup (Docker on Windows host, Linux container):

- We need the **Linux** server (EcoServer binary + linux64, etc.). SteamCMD in a Linux container downloads the Linux build.
- Use **dev-files/scripts/download-eco-server-steamcmd.ps1** to run SteamCMD in Docker and download directly into the **Server** folder (or into a staging folder, then copy to Server).
- After the download, run the game server (e.g. nidaren image) with the same Server folder mounted; it will find EcoServer and start.

---

## Server folder breakdown (wiki)

| Folder | Purpose |
|--------|---------|
| **Configs** | Server settings (.eco files). Edit with a text editor; restart server for changes. |
| **Mods** | Game data + mods. Put custom mods here (e.g. Mods/UserCode). |
| **Storage** | Saves: Game.db, Game.eco, Backup/. Wipe this folder to reset the world. |
| **Web Client** | Web server files for the server UI. |
| **EcoServer** (binary) | Linux: `EcoServer` + **linux64/** (steamclient.so). Windows: EcoServer.exe + .dlls. |

---

## Ports (wiki)

- **Game:** 3000 UDP (or UDP/TCP)
- **Web UI:** 3001 TCP (or UDP/TCP)

If the client runs on the same machine, use different ports (e.g. 4000, 4001) and forward those.

---

## Configuration notes (wiki)

1. **WorldGenerator.eco** – Changes require a server wipe to take effect.
2. **Map size** – Keep values divisible by 4.
3. **Whitelist/admins** – Use SLG id or Steam64 id, not username. [steam64.com](http://www.steam64.com)
4. In Server UI: **File → Save** after changes or they are lost on reload.

---

## Building a fresh server in this repo

1. **Get server files** (pick one):
   - **SteamCMD (Linux files for Docker):** Run `dev-files/scripts/download-eco-server-steamcmd.ps1` so the Server folder (or a staging folder) gets the Linux Eco server (app 739590).
   - **play.eco .zip:** Download Server .zip from [play.eco/account](https://play.eco/), unzip into an empty folder. (Windows build; for Docker you need Linux files from SteamCMD.)
2. **Preserve your data:** If Server already has Configs/Storage you want to keep, back them up before overwriting. The script can download to a staging folder; you then copy only EcoServer, linux64, Mods/__core__, etc., into Server and keep your Configs/Storage.
3. **Run the server:** Use docker-compose (nidaren image) with Server mounted, or run EcoServer.exe / EcoServer directly if not using Docker.

See [developer-setup-docker-and-server.md](developer-setup-docker-and-server.md) for Docker commands and troubleshooting.
