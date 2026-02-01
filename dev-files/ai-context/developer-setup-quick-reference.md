# Developer setup – quick reference

Minimal checklist for a new developer to run the Eco dev server in Docker.

---

## 1. Prerequisites

- Docker Desktop installed and running.
- Eco server token: [play.eco/en/account](https://play.eco/en/account) → Server Authentication → Generate Token.

---

## 2. One-time setup (from Server root)

Run from the **Server** directory (repo root, where **docker-compose.yml** and **.env.example** live):

```bash
copy .env.example .env
# Edit .env and set ECO_TOKEN=your_token

mkdir Storage
# (if Storage doesn't exist)
```

---

## 3. Run the server

```bash
docker compose up -d
docker compose logs -f
```

Stop when you see world generation and plugin init complete.

---

## 4. Useful commands

| Command | Description |
|---------|-------------|
| `docker compose up -d` | Start server |
| `docker compose down` | Stop server |
| `docker compose logs -f` | Follow logs |
| `docker compose restart` | Restart server |
| `docker attach eco-dev` | Attach to console (Ctrl+P, Ctrl+Q to detach) |

---

## 5. Access

- **Game:** UDP port **3000**.
- **Web UI:** [http://localhost:3001](http://localhost:3001).

---

## 6. Required repo contents

- **docker-compose.yml** and **.env** at **Server root** (this world directory).
- **Configs/**, **Mods/UserCode/**, and **Storage/** (Storage can be empty).
- **dev-files/EcoPC_v0.12.0.6-beta/** with `Eco_Data/Server/Mods/__core__` (used by docker-compose for Mods/__core__ overlay).

The nidaren image can download the Linux Eco server into the mount if not present (`UPDATE_SERVER=true` in .env).

Full details and troubleshooting: [developer-setup-docker-and-server.md](developer-setup-docker-and-server.md).
