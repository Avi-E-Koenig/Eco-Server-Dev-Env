# Eco modding: environment, tools, editors

Concise reference for setting up and using the Eco modding environment.

---

## Server-only modding (UserCode)

- **No ModKit required.** Put C# in `Mods/UserCode`; the server compiles it together with core on start.
- **Apply changes:** Restart the server after editing UserCode.
- **Editors:** Any text editor works. **Visual Studio 2022** (Community) is recommended by Strange Loop for C# and debugging.

---

## Full mods (new items/blocks, Unity assets)

- **ModKit:** Download from [play.eco/account](https://play.eco/account) (ModKit section), unzip locally.
- **Unity:** Version must match the ModKit’s `ProjectVersion.txt` (see [Installing the ModKit](https://wiki.play.eco/en/Installing_the_ModKit)) — create a 3D project, import `EcoModKit.unitypackage`.
- **API docs:** [docs.play.eco](https://docs.play.eco) — Server API, Client API, Remote API; generated for the current Eco version.

---

## Server config

- **ModKit config:** `Configs/ModKit.eco` (create from `Configs/ModKit.eco.template` if needed). Options include: whitelist, `LiveUpdateUnityFiles`, `SubscribedMods`, `PreserveGeneratedModsAssembly`.
- Config changes typically require a server restart unless you use `/serverui` and save.

---

## References

- [Eco Mod Development (wiki)](https://wiki.play.eco/en/Mod_Development)
- [EcoModKit GitHub](https://github.com/StrangeLoopGames/EcoModKit)
- Eco Discord: #mod-dev, #mod-help
