# KB: How to reset the world

How to wipe the Eco server world and start fresh (e.g. to test starter campsite changes or a new world seed).

---

## What gets reset

- **Storage/** holds the saved world and game state (Game.db, Game.eco, Backup/, UserTextures/, etc.).
- Wiping **Storage** removes the world, all players, and all progress. The server will generate a **new world** on next start.

---

## Steps

### 1. Stop the server

From the **Server** directory (where `docker-compose.yml` is):

```bash
docker compose down
```

### 2. (Optional) Back up the current world

If you want to keep a copy before wiping:

```bash
# Example: rename Storage so you can restore later
mv Storage Storage_backup_2026-01-31
mkdir Storage
```

Or copy the folder instead of renaming:

```bash
cp -r Storage Storage_backup_2026-01-31
```

Then wipe the contents of **Storage** (step 3). On Windows PowerShell you can rename: `Rename-Item Storage Storage_backup; New-Item -ItemType Directory Storage`.

### 3. Wipe Storage

**Option A – Delete contents, keep folder**

The Docker bind mount expects the **Storage** folder to exist. Delete everything *inside* it:

**PowerShell (Windows):**
```powershell
Remove-Item -Path ".\Storage\*" -Recurse -Force
```

**Bash (Linux/Mac):**
```bash
rm -rf Storage/*
```

**Option B – Remove and recreate Storage**

```bash
rm -rf Storage
mkdir Storage
```

On Windows PowerShell: `Remove-Item -Recurse -Force Storage; New-Item -ItemType Directory Storage`

### 4. Start the server

```bash
docker compose up -d
```

On first boot after a wipe, the server will generate a new world. New characters will get the current starter config (e.g. overridden campsite inventory) when they place their first campsite.

---

## Quick reference

| Goal              | Commands |
|-------------------|----------|
| Full reset        | `docker compose down` → wipe `Storage` contents → `docker compose up -d` |
| Restart only      | `docker compose restart` (world is **not** reset) |
| Backup then reset | Rename or copy `Storage` to `Storage_backup_*`, then wipe `Storage` and `docker compose up -d` |

---

## See also

- [developer-setup-docker-and-server.md](developer-setup-docker-and-server.md) – Docker and server setup, ports, troubleshooting.
