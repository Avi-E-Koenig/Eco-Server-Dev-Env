# KB: How to spawn items and give skills

Admin chat commands for spawning items into inventory, placing world objects, and granting skills. Requires **Admin** access (see [Configs/Users.eco](https://wiki.play.eco/en/Server_Configuration/Users.eco) Admins list).

---

## Prerequisites

- Your user must be in **Configs/Users.eco** → **UserPermission.Admins** (Steam64 ID or SLG ID).
- Commands are typed in **in-game chat** (press Enter, then `/command`).
- Reference: [Eco Wiki – Chat Commands](https://wiki.play.eco/en/Chat_Commands).

---

## 1. Spawn items (into your inventory)

Use the **item** type name (the class that represents the holdable item). Many items use the **`…Item`** suffix.

| Command | Description |
|--------|-------------|
| **/give** *itemName* *number* | Give yourself an item (Admin). |
| **/inventory give** *itemName* *number* | Same as `/give`. |

**Examples:**

```text
/give RefrigeratedDisplayCabinetItem 1
/give StoneItem 50
/give MechanicsSkillScroll 1
```

- **itemName** = exact type name (e.g. `RefrigeratedDisplayCabinetItem`, `StoneItem`). Case-sensitive.
- **number** = amount (default 1 if omitted in some versions).

**If the name is wrong:** Eco will report an error. Use **/help** or **/helpful** to list commands; check `__core__` or UserCode for the real class name (e.g. `*Item` for items, `*SkillScroll` for scrolls).

---

## 2. Place a world object (already in inventory)

1. Use **/give** *WorldObjectItem* to get the item (e.g. `RefrigeratedDisplayCabinetItem`).
2. Put it on your **hotbar**, select it.
3. **Click** in the world to place it like any other world object.

No separate “spawn placed object” step needed if you use `/give` + place.

---

## 3. Spawn a world object directly (without giving the item)

Some servers support spawning the **placed object** in the world:

| Command | Description |
|--------|-------------|
| **/objects add** *typeName* | Add one object of the given type at your position (Admin). |

**Examples:**

```text
/objects add RefrigeratedDisplayCabinet
/objects add RefrigeratedDisplayCabinetObject
```

- **typeName** is usually the **object** type (e.g. `RefrigeratedDisplayCabinet` or `RefrigeratedDisplayCabinetObject`), not the item name. If one fails, try the other.
- **/objects list** *typeName* — list objects of that type (or all types if omitted).

If **/objects add** doesn’t exist or fails, use **/give** *…Item* and place it manually.

---

## 4. Give skills (no scroll)

Grant a skill directly; no need to use a skill scroll.

| Command | Description |
|--------|-------------|
| **/skills give** *skillName* *targetUser* | Give a skill by name. Optional *targetUser*; if omitted, applies to you. |
| **/skills levelup** *skillName* *targetUser* | Level that skill to **max**. Optional *targetUser*. |
| **/skills all** *targetUser* | Unlock all skills for the target (or you). |

**Examples:**

```text
/skills give MechanicsSkill
/skills give LoggingSkill
/skills give CarpentrySkill
/skills levelup MechanicsSkill
/skills all
```

- **skillName** = exact type name (e.g. `MechanicsSkill`, `CarpentrySkill`, `LoggingSkill`). See `Mods/__core__/AutoGen/Tech/*.cs` for names.

---

## 5. Give skill scrolls (as items)

To give the **scroll item** (player uses it to learn the skill):

```text
/give MechanicsSkillScroll 1
/give CarpentrySkillScroll 1
/give LoggingSkillScroll 1
```

Same **/give** pattern as other items; scroll type names usually end with **`SkillScroll`**.

---

## 6. Command discovery

If a command or name doesn’t work:

| Command | Description |
|--------|-------------|
| **/help** | List all commands (optional filter text). |
| **/helpful** | Show all help including subcommands. |
| **/manage authlevel** | Show your authorization level (User / Admin / etc.). |
| **/objects list** | List object types; use to confirm exact *typeName* for **/objects add**. |

Example: **/help give** to see syntax for give-related commands.

---

## Quick reference

| Goal | Command |
|-----|--------|
| Item in inventory | `/give` *ItemTypeName* *amount* (e.g. `RefrigeratedDisplayCabinetItem 1`) |
| Place world object | `/give` *WorldObjectItem* then place from hotbar, **or** `/objects add` *ObjectTypeName* |
| Give skill | `/skills give` *SkillName* (e.g. `MechanicsSkill`) |
| Level skill to max | `/skills levelup` *SkillName* |
| Give skill scroll | `/give` *SkillScrollName* 1 (e.g. `MechanicsSkillScroll 1`) |
| List/find commands | `/help` or `/helpful` |

---

## See also

- [Chat Commands](https://wiki.play.eco/en/Chat_Commands) (Eco Wiki)
- [Server Configuration/Users.eco](https://wiki.play.eco/en/Server_Configuration/Users.eco) – Admins list
- [kb-reset-world.md](kb-reset-world.md) – World reset
