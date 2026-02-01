# ai-context

Documentation and knowledge boards for this Eco server. For AI context and human reference.

---

## Developer setup (Docker and server)

For new developers: how to run the Eco dev server on your machine.

| Doc | Description |
|-----|-------------|
| [developer-setup-docker-and-server.md](developer-setup-docker-and-server.md) | Full guide: prerequisites, required files, .env, Docker commands, ports, troubleshooting. |
| [developer-setup-quick-reference.md](developer-setup-quick-reference.md) | Short checklist and commands. |

---

## Knowledge base (server operations)

| Doc | Description |
|-----|-------------|
| [setting-up-server-wiki.md](setting-up-server-wiki.md) | **Fresh server:** wiki-based setup (Steam, SteamCMD, Without Steam), folder breakdown, SteamCMD script. |
| [kb-reset-world.md](kb-reset-world.md) | How to reset the world: stop server, wipe Storage, start fresh; optional backup. |
| [kb-world-generator-single-biome-no-trees.md](kb-world-generator-single-biome-no-trees.md) | World had one biome / no trees: fix via Seed and map size in WorldGenerator.eco, then regenerate. |

---

## Eco modding docs

| Doc | Description |
|-----|-------------|
| [eco-modding-environment.md](eco-modding-environment.md) | Environment, tools, editors (UserCode vs full ModKit, Visual Studio, Unity, API docs). |
| [eco-modding-conventions.md](eco-modding-conventions.md) | Namespaces, partial classes, file naming, and not editing __core__. |
| [eco-modding-overrides.md](eco-modding-overrides.md) | Override best practices: partial hooks vs full .override.cs, recipe examples. |
| [eco-modding-folder-structure.md](eco-modding-folder-structure.md) | Mods/__core__, Mods/UserCode, path rules, and namespace-to-folder mapping. |
| [eco-modding-assets-and-world-objects.md](eco-modding-assets-and-world-objects.md) | Mods with 3D assets: custom blocks (BlockSet, BlockBuilder, meshes) and placeable WorldObjects (e.g. fridge, CO2 scrubber); Unity/ModKit pipeline and naming. |
| [fridge-mod-setup-guide.md](fridge-mod-setup-guide.md) | Step-by-step tools and environment setup for developing a fridge mod (VS 2022, Unity, ModKit, Collections). |

For full detail, see [docs.play.eco](https://docs.play.eco), [wiki Mod Development](https://wiki.play.eco/en/Mod_Development), and `Mods/UserCode/README.md`.
