# Eco modding: override classes best practices

How to extend or replace core classes safely.

---

## Prefer partial + hooks over full override

- Use a **partial class** in UserCode and implement only `ModsPreInitialize()` / `ModsPostInitialize()` to change recipes, housing, labor, etc.
- This is less fragile across game updates than replacing entire files.

---

## When to use full file override

- Only when you need to **replace the entire class** (e.g. behavior that cannot be done in the partial hooks).
- Create a file under UserCode with the **same path as in __core__** and add **`.override`** before `.cs`.
- Example: core `__core__\AutoGen\Blocks\AsphaltBlock.cs` → UserCode `UserCode\AutoGen\Blocks\AsphaltBlock.override.cs`.
- That file is compiled **instead of** the core file (see `Mods/UserCode/README.md`).

---

## Partial recipe example

1. Copy **namespace**, **usings**, and **partial class name** from the core file.
2. In `ModsPreInitialize()` you can:
   - **(a)** Clear and repopulate `this.Recipes[0].Ingredients`, or
   - **(b)** Replace `this.Recipes` with a new list, or
   - **(c)** Add a new recipe to the existing list.
3. Set `LaborInCalories`, `CraftMinutes` as needed.
4. Use the ComputerLab examples in `Mods/UserCode/README.md` as the canonical pattern.

---

## Override vs partial (C# keywords)

- **`override`** (C# keyword): use when a class **overrides** a base class method (e.g. `public override float Calories => 5000` in core). You usually do not add new `override` members in UserCode unless you are doing a full `.override.cs` file.
- **Partial class:** In UserCode you typically **add** partial method implementations (`ModsPreInitialize` / `ModsPostInitialize`), not new virtual/override members.
