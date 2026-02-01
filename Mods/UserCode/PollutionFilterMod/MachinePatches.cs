// Copyright (c) Strange Loop Games. All rights reserved.
// Pollution Filter Mod - Example: Adding Filter Support to Existing Machines

using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;

namespace Eco.Mods.TechTree
{


	[AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class CombustionGeneratorItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class CombustionGeneratorObject { }

    [AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class IndustrialGeneratorItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class IndustrialGeneratorObject { }

    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class BlastFurnaceObject { }

    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class CementKilnObject { }

    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class OilRefineryObject { }

    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class StoveObject { }


    [AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class ExcavatorItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class ExcavatorObject { }


    [AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class IndustrialBargeItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class IndustrialBargeObject { }

	[AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class SkidSteerItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class SkidSteerObject { }


    [AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class TrailerTruckItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class TrailerTruckObject { }

    [AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class TruckItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class TruckObject { }


	[AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class MediumFishingTrawlerItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class MediumFishingTrawlerObject { }

	[AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class CraneItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class CraneObject { }

	[AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class SteamTruckItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class SteamTruckObject { }


    [AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class PoweredCartItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class PoweredCartObject { }


    [AllowPluginModules(ItemTypes = new[] {
        typeof(BasicPollutionFilterItem),
        typeof(AdvancedPollutionFilterItem),
        typeof(IndustrialPollutionFilterItem)
    })]
    public partial class SteamTractorItem { }

    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(PollutionFilterMonitorComponent))]
    public partial class SteamTractorObject { }

    // ============================================
    // SUMMARY
    // ============================================
    // Total machines/vehicles with pollution filter support: 17
    //
    // Machines (7):
    // - Combustion Generator, Industrial Generator (patched)
    // - Blast Furnace, Cement Kiln, Oil Refinery, Stove (already had modules)
    //
    // Vehicles (10):
    // - High pollution: Excavator, IndustrialBarge, SkidSteer, TrailerTruck, Truck
    // - Medium pollution: MediumFishingTrawler, Crane, SteamTruck
    // - Low pollution: PoweredCart, SteamTractor
}
