# Developer setup: Docker and Eco server

This guide gets a new developer’s machine ready to run the Eco dev server in Docker. Use it when onboarding or when you need to recreate the environment.

---

## Prerequisites

- **Docker Desktop** (Windows/Mac/Linux) installed and running.
- **Eco server token** (Strange Cloud auth). Get one at [play.eco/en/account](https://play.eco/en/account) → **Server Authentication** → **Generate Token**.

---

## Required files on this repo

The server runs from the **Server** directory (the repo root where this world’s Configs, Mods, Storage live). **docker-compose.yml** and **.env** are at **Server root** (not inside dev-files). The following must be present for Docker to work:

| Item | Purpose |
|------|--------|
| **docker-compose.yml** | At Server root. Mounts this world and **dev-files/EcoPC_v0.12.0.6-beta** for Mods/__core__. |
| **Configs/** | Your server config (Network.eco, Users.eco, etc.). |
| **Mods/UserCode/** | Your custom mods and overrides. |
| **Storage/** | World saves and backups (created if missing). |
| **dev-files/EcoPC_v0.12.0.6-beta/** | Extract of Eco 0.12.0.6-beta. Used to supply **Mods/__core__** so mod compilation matches the server version. |

The nidaren image can download the Linux Eco server into the mount if not present (set `UPDATE_SERVER=true` in .env). If you prefer to provide local Linux server files, add **EcoServer** (Linux binary) and **linux64/** (with steamclient.so) to the Server directory.

If any of these are missing (e.g. you cloned only part of the repo), see [Getting the server files](#getting-the-server-files) below.

---

## One-time setup

### 1. Create `.env` from the example

From the **Server** directory (repo root, where **docker-compose.yml** and **.env.example** live):

```bash
copy .env.example .env
```

(On Linux/Mac: `cp .env.example .env`.)

### 2. Set your Eco token in `.env`

Edit `.env` and set your real token:

```env
ECO_TOKEN=your_actual_token_from_play_eco
```

Get the token at [play.eco/en/account](https://play.eco/en/account) → Server Authentication → Generate Token.

### 3. Ensure `Storage` exists

The compose file bind-mounts `./Storage`. If it doesn’t exist:

```bash
mkdir Storage
```

---

## Running the server with Docker

All commands are run from the **Server** directory (repo root, where **docker-compose.yml** lives—not inside dev-files).

| Command | Description |
|---------|-------------|
| `docker compose up -d` | Start the server in the background. |
| `docker compose down` | Stop and remove the container. |
| `docker compose restart` | Restart the server. |
| `docker compose logs -f` | Stream logs (Ctrl+C to stop). |
| `docker attach eco-dev` | Attach to the server console (detach: **Ctrl+P**, then **Ctrl+Q**). |
| `docker compose exec eco-dev /bin/bash` | Open a shell inside the container. |

### First run

```bash
docker compose up -d
docker compose logs -f
```

Wait until you see world generation and “Initializing WorldGeneratorPlugin … Finished”. The server is ready when plugins have started and there are no repeated errors.

---

## Ports

| Port | Protocol | Use |
|------|----------|-----|
| 3000 | UDP | Game server (Network.eco: GameServerPort). |
| 3001 | TCP | Web UI (Network.eco: WebServerPort). |
| 3002 | TCP | RCON. |
| 3003 | UDP | Steam (Network.eco: SteamServerPort). |

- **Game:** connect to the host on port **3000** (UDP).  
- **Web UI:** open `http://localhost:3001` (or your machine’s IP) in a browser.

---

## How the Docker setup works

- **Image:** `nidaren/eco-server:environment` (includes Steam SDK; the official Eco image does not).
- **Auth:** `ECO_TOKEN` from `.env` is passed as `-userToken` for Strange Cloud; no manual `-offline` needed when the token is set.
- **Mounts:** Run from **Server root** (where Configs, Mods, Storage live).
  - The whole **Server** directory (`.`) is mounted at `/home/container/server`, so the container uses this world’s **Configs**, **Mods**, **Storage**, and **Logs**. The image can download the Linux Eco server into the mount if not present (`UPDATE_SERVER=true`).
  - **Mods/__core__** is overridden with **dev-files/EcoPC_v0.12.0.6-beta/Eco_Data/Server/Mods/__core__** so the compiler sees the correct 0.12.0.6 core and avoids type/namespace compile errors.

---

## Getting the server files

If you don’t have the Server directory fully populated (e.g. no EcoServer, no linux64, no EcoPC extract):

1. **EcoServer + linux64**  
   Install or copy the **Linux** Eco dedicated server (e.g. via SteamCMD or from another Linux install) so that this Server folder contains:
   - The **EcoServer** executable (Linux).
   - The **linux64/** folder with `steamclient.so` (and any other Steam libs the server expects).

2. **dev-files/EcoPC_v0.12.0.6-beta**  
   The Eco 0.12.0.6-beta extract should be at **dev-files/EcoPC_v0.12.0.6-beta** so that:
   - `dev-files/EcoPC_v0.12.0.6-beta/Eco_Data/Server/Mods/__core__` exists.
   - The compose at Server root uses `./dev-files/EcoPC_v0.12.0.6-beta/Eco_Data/Server/Mods/__core__` (adjust if your layout differs).

3. **Configs**  
   Copy from an existing server or use the templates in `Configs/` and configure **Network.eco**, **Users.eco**, etc. as needed.

---

## Troubleshooting

| Problem | What to try |
|--------|-------------|
| **EcoServer: No such file or directory** | Ensure the **EcoServer** Linux binary and **linux64/** (with steamclient.so) are in the Server directory. The container needs them via the full-directory mount. |
| **Authentication to Strange Cloud failed** | Set a valid **ECO_TOKEN** in `.env` (from play.eco → Server Authentication → Generate Token). |
| **Steam game server failed to initialize** / **steamclient.so** missing | Use the **nidaren** image (we do) and ensure **linux64/** with `steamclient.so` is present in the Server directory. |
| **Mod compile errors** (e.g. TailoringSkill, BlacksmithSkill, Eco.Mods.Organisms) | The **Mods/__core__** overlay must point at the 0.12.0.6 core. Check that **dev-files/EcoPC_v0.12.0.6-beta/Eco_Data/Server/Mods/__core__** exists and the second volume in **Server root** `docker-compose.yml` is correct. |
| **Port already in use** | Change host ports in `docker-compose.yml` (e.g. `"3010:3000/udp"` instead of `"3000:3000/udp"`) or stop the process using the port. |

---

## Reference: `.env` and compose

- **.env.example** (at Server root) lists `ECO_TOKEN` and optional vars (`VERSION_BRANCH`, `UPDATE_SERVER`, `STEAM_FEEDBACK`). Copy to `.env` in the same directory and set at least `ECO_TOKEN`.
- **docker-compose.yml** (at Server root) defines the `eco-dev` project, the nidaren image, env file, two volume mounts (world + dev-files __core__), and the four port mappings above. Customise project/container name via the top-level `name:` and `container_name:` if needed.

For modding and code conventions, see the other docs in this **ai-context** folder and `Mods/UserCode/README.md`.
