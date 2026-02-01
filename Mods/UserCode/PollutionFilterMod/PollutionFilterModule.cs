// Copyright (c) Strange Loop Games. All rights reserved.
// Pollution Filter Mod - V1
// Adds pollution-reducing modules for machines

namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    /// <summary>
    /// Base class for pollution filter modules that reduce air pollution from machines.
    /// Unlike standard modules, these reduce pollution output rather than affecting efficiency or speed.
    /// </summary>
    [Serialized]
    public abstract class PollutionFilterModule : PluginModule
    {
        /// <summary>
        /// Pollution reduction as a multiplier (0.0 to 1.0).
        /// 0.2 = 20% reduction, 0.5 = 50% reduction, etc.
        /// </summary>
        public abstract float PollutionReduction { get; }

        /// <summary>
        /// Filter tier for display purposes (e.g., "Basic", "Advanced", "Industrial")
        /// </summary>
        public abstract string FilterTier { get; }

        protected PollutionFilterModule() : base(ModuleTypes.None)
        {
            // We use ModuleTypes.None because pollution reduction isn't a standard module type
            // The actual pollution reduction is handled by a custom component
        }

        public override IEnumerable<LocString> Benefits
        {
            get
            {
                var percent = Text.StyledPercent(this.PollutionReduction);
                yield return Localizer.Do($"Reduces air pollution output by {percent}.");
                yield return Localizer.Do($"Filter Tier: {this.FilterTier}");
            }
        }

        public override float Modify(ModuleModifiedValue value)
        {
            // This module doesn't modify standard dynamic values (efficiency/speed)
            // Pollution reduction is handled separately by PollutionFilterComponent
            return value.GetBaseValue;
        }

        /// <summary>
        /// Gets the pollution reduction multiplier to apply to pollution output.
        /// Returns value between 0 and 1, where lower = more pollution reduction.
        /// Example: 0.8 means machine produces 80% of original pollution (20% reduction)
        /// </summary>
        public float GetPollutionMultiplier()
        {
            return 1f - this.PollutionReduction;
        }
    }
}
