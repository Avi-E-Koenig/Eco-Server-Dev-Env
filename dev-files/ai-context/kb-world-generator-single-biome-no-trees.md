# KB: World had one biome and no trees

If a newly generated world has only one biome and no (or very few) trees, it’s usually due to **world size** and **seed** in `Configs/WorldGenerator.eco`.

---

## Likely causes

1. **Small map (72×72)** – With **MapSizePreset: "Small"** and **Dimensions: 72×72**, there’s limited space. The **Seed** can produce a layout where one landmass ends up as a single biome (e.g. all Grassland or all Desert). Trees are placed per biome; one small biome can mean few or no trees.
2. **Seed** – A fixed **Seed** (e.g. `1126004571`) always gives the same layout. Some seeds on a small map produce a single dominant biome.

---

## Fixes (edit `Configs/WorldGenerator.eco`)

### 1. Use a different seed

Change **Seed** so the next world layout is different:

- Set **Seed** to `0` (wiki default), or  
- Use any other integer (e.g. random). Each value gives a different world.

Location in the file: under the first **VoronoiModule** **Config** (near the top), e.g.:

```json
"Seed": 0,
```

### 2. Use a larger map (recommended)

Larger maps give more room for multiple biomes and forests:

- **MapSizePreset**: `"Medium"` (or `"Large"` if the server can handle it).
- **Dimensions**:
  - **WorldWidth**: `100` (Medium) or `160` (Large).
  - **WorldLength**: `100` (Medium) or `160` (Large).

Example for Medium (1 km²):

```json
"MapSizePreset": "Medium",
"Dimensions": {
  "WorldWidth": 100,
  "WorldLength": 100
},
```

Both dimensions must be equal and divisible by 4. See [Server Configuration/WorldGenerator.eco](https://wiki.play.eco/en/Server_Configuration/WorldGenerator.eco) for size limits.

### 3. Regenerate the world

Changes in **WorldGenerator.eco** only apply when a **new** world is generated:

1. Stop the server (`docker compose down`).
2. Wipe **Storage** (see [kb-reset-world.md](kb-reset-world.md)).
3. Start the server (`docker compose up -d`).

The server will create a new world using the updated seed and dimensions.

---

## Optional: preview in game

You can preview world generation without wiping:

- In-game: **/serverui** → **Sim** → **World Generator** to tweak seed, size, and biome weights and see a preview. Export or copy settings back into `Configs/WorldGenerator.eco` if needed.

---

## Summary

| Problem        | Try this |
|----------------|----------|
| One biome      | New **Seed** (e.g. `0`) and/or **larger map** (Medium 100×100). |
| No trees       | Usually the same: different seed + larger map so forest biomes (Warm Forest, Cool Forest, Rainforest, etc.) have room to generate. |
| Apply changes  | Edit `Configs/WorldGenerator.eco`, then **reset the world** (wipe Storage, restart server). |
