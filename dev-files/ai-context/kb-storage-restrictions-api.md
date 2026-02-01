# KB: Storage restrictions API (verified from __core__)

Verification of storage input restrictions in Eco, based on **Mods/__core__** and the snippet you shared.

---

## What the codebase actually uses

### 1. Method and property names

| Snippet / docs | __core__ usage |
|----------------|----------------|
| `inventory.AddRestriction(new InventoryRestriction(...))` | **`storage.Storage.AddInvRestriction(restriction)`** |
| `GetComponent<PublicStorageComponent>().Inventory` | **`.Storage`** for normal world-object storage (Icebox, Refrigerator, etc.) |

- **Property:** For standard storage (Icebox, Refrigerator, RefrigeratedDisplayCabinet, etc.), restrictions are added to **`PublicStorageComponent`’s `.Storage`**, not `.Inventory`.
- **Method:** The API in __core__ is **`AddInvRestriction`**, not `AddRestriction`.
- **Exception:** FishTrapObject and CrabPotObject use **`.Inventory.AddInvRestriction(...)`** (they use a separate “Inventory” on the component).

### 2. Food-only storage: use `FoodStorageRestriction`

**Mods/__core__/Objects/FoodStorage.cs** adds food-only storage to Icebox, Refrigerator, and IndustrialRefrigerator in **PostInitialize()**:

```csharp
this.GetComponent<PublicStorageComponent>().Storage.AddInvRestriction(new FoodStorageRestriction());
```

So the **canonical** way to get “food only” in this codebase is:

- **Restriction type:** **`FoodStorageRestriction`**
- **Where:** **`GetComponent<PublicStorageComponent>().Storage.AddInvRestriction(new FoodStorageRestriction())`**
- **When:** e.g. in **PostInitialize()** (as in FoodStorage.cs) or in **Initialize()** after storage init.

`FoodStorageRestriction` is not defined in Mods/__core__; it comes from the Eco.Gameplay assembly (precompiled). It is the built-in “food only” restriction.

### 3. Other restriction types seen in __core__

| Restriction | Usage |
|-------------|--------|
| **FoodStorageRestriction** | Food-only (Icebox, Refrigerator, IndustrialRefrigerator via FoodStorage.cs) |
| **SpecificItemTypesRestriction(Type[])** | Only specific item types (e.g. FishTrap: TroutItem, SalmonItem; CrabPot: CrabCarcassItem) |
| **StackLimitRestriction(n)** | Max stack size per slot |
| **NotCarriedRestriction()** | No blocks / large items |
| **TagRestriction("Fish")** | By tag (e.g. FishingComponent) |
| **SiloRestriction** | Silo-specific |
| **StockpileStackRestriction** | Stockpile-specific |
| **PaintItemRestriction**, **ClothItemRestriction** | Domain-specific |

No **`InventoryRestriction`** type with a lambda + message appears in __core__; the snippet you have may be from another Eco API layer or docs (e.g. a generic wrapper that isn’t used in these mods).

---

## If you want food-only on RefrigeratedDisplayCabinet (or similar)

**Option A – Use built-in (recommended):**

In your override, after initializing `PublicStorageComponent`, add:

```csharp
storage.Storage.AddInvRestriction(new FoodStorageRestriction());
```

Same pattern as **FoodStorage.cs** (and same property: **`.Storage`**, method: **`AddInvRestriction`**).

**Option B – Custom “food only” by type:**

If you ever need a custom predicate and your Eco build exposes something like `InventoryRestriction(Func<Item, bool>, LocString)`, then `item => item is FoodItem` is the right idea; but in the current __core__ we only see **`FoodStorageRestriction`** and **`SpecificItemTypesRestriction`** for this.

---

## TL;DR

| Question | Verified answer |
|----------|------------------|
| Property for main storage? | **`.Storage`** (not `.Inventory`) for normal world-object storage |
| Method to add restriction? | **`AddInvRestriction(restriction)`** (not `AddRestriction`) |
| Food-only restriction? | **`new FoodStorageRestriction()`** |
| Where is it applied? | **FoodStorage.cs** adds it in **PostInitialize()** to Icebox, Refrigerator, IndustrialRefrigerator via **`.Storage.AddInvRestriction(new FoodStorageRestriction())`** |

---

## See also

- **Mods/__core__/Objects/FoodStorage.cs** – food-only storage wiring
- **Mods/__core__/Objects/FishTrapObject.cs** – **SpecificItemTypesRestriction** and **.Inventory** (trap-specific)
- **Mods/__core__/AutoGen/WorldObject/Icebox.cs** – base storage init; food restriction comes from FoodStorage.cs
- [Eco ModKit / API docs](https://docs.play.eco/) – for types not defined in __core__ (e.g. `FoodStorageRestriction`, `InventoryRestriction` if it exists elsewhere)
