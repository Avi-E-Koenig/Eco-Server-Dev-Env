# KB: Mod compile errors in Docker (TreeEntity, CarpentrySkill, etc. not found)

If the Eco server in Docker fails to start with many **CS0246** errors (e.g. `TreeEntity`, `CarpentrySkill`, `GetComponent` not found), the **Mods/__core__** the container sees does not match the **EcoServer** binary version.

---

## Cause

- The **world’s** `Server/Mods/__core__` (from the Windows host / game UI) is from one Eco version.
- The **downloaded** Linux EcoServer in the container is from another (e.g. public branch).
- Overlaying **EcoPC 0.12.0.6-beta** `__core__` can still mismatch if the download is not exactly that version.

So the compiler sees __core__ source that doesn’t match the server’s assemblies → “type or namespace could not be found”.

---

## Fix: Let the download provide Mods/__core__

1. **Stop the container**
   ```powershell
   cd C:\workspace\Server\dev-files
   docker compose down
   ```

2. **Back up and remove the world’s __core__** (so the container doesn’t use it)
   ```powershell
   cd C:\workspace\Server
   Rename-Item -Path "Mods\__core__" -NewName "__core__.backup"
   ```

3. **Start the container** (with `UPDATE_SERVER=true` in `.env` so it can download)
   ```powershell
   cd C:\workspace\Server\dev-files
   docker compose up -d
   docker compose logs -f
   ```

4. **Let the first run finish**  
   The image should download the server and, if the depot includes it, populate `Mods/__core__` under the mount. After that, the server should start without those compile errors.

5. **If Mods/__core__ is still missing** after the download (e.g. empty or no such folder), the image may keep __core__ only inside `steamapps`. Then you can:
   - Restore the backup: `Rename-Item Mods\__core__.backup Mods\__core__` and try a different `VERSION_BRANCH` in `.env` (e.g. `playtest`), or
   - Ask in [nidaren Discord](https://discord.nidaren.net) how to get a matching __core__ for the downloaded server.

---

## See also

- [developer-setup-docker-and-server.md](developer-setup-docker-and-server.md) – Docker setup, ports, troubleshooting.
