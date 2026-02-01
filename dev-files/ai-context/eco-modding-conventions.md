# Eco modding: conventions

Conventions for Eco server mods (UserCode and overrides).

---

## Namespaces

- **Match the core file you extend.** Copy the `namespace` and relevant `using` from the original file in `Mods/__core__`.
- Common namespaces:
  - `Eco.Mods.TechTree` — most AutoGen content, Tools, Items, Objects
  - `Eco.Mods.Organisms` — organisms (e.g. trees)
  - `Eco.Mods.WorldLayers` — world layer settings

---

## Do not edit `Mods/__core__`

- Game updates overwrite `__core__`. All custom code belongs in:
  - `Mods/UserCode` (partial classes or `.override.cs` files), or
  - Precompiled `.dll` mods in `Mods/` (or subfolders).

---

## Partial classes

- Use `partial class` with the **same name** as the core class.
- Implement `partial void ModsPreInitialize()` and/or `partial void ModsPostInitialize()` only where the core declares them (e.g. `Mods/__core__/AutoGen/WorldObject/StorageChest.cs`).
- One partial = one extension of that type; keep changes in these hooks where possible.

---

## Auto-generated notice in core

- Core files often state: *"Use Mods* partial methods instead for customization"* and *"override the entire file"* via UserCode (e.g. `Mods/__core__/AutoGen/Food/Tomato.cs` remarks). Follow that: prefer partials; use full override only when necessary.

---

## File naming

- **Partial:** File name = class name (e.g. `ComputerLabRecipe.cs`).
- **Full override:** Same path as in `__core__`, and add `.override` before `.cs` (e.g. `AsphaltBlock.override.cs`). See [eco-modding-folder-structure.md](eco-modding-folder-structure.md).
