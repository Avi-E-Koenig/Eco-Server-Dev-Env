// Copyright (c) Strange Loop Games. All rights reserved.
// Pollution Filter Mod - Component to Apply Filter Effects
// This uses a simpler component-based approach instead of Harmony patches

namespace Eco.Mods.TechTree
{
    using System;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Serialization;
    using Eco.Core.Utils;
    using Eco.Shared.Utils;
    using Eco.Shared.Localization;
    using Eco.Gameplay.Players;

    /// <summary>
    /// Component that monitors for pollution filter modules and applies pollution reduction.
    /// Add this component to any world object that has both AirPollutionComponent and PluginModulesComponent.
    /// This ensures that adding/removing filters updates pollution values in real-time.
    /// </summary>
    [Serialized]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    public class PollutionFilterMonitorComponent : WorldObjectComponent
    {
        private PluginModulesComponent modulesComponent;
        private AirPollutionComponent pollutionComponent;
        private float baselinePollution;
        private bool isInitialized = false;

        public override void Initialize()
        {
            base.Initialize();

            this.modulesComponent = this.Parent.GetComponent<PluginModulesComponent>();
            this.pollutionComponent = this.Parent.GetComponent<AirPollutionComponent>();

            if (this.modulesComponent != null && this.pollutionComponent != null)
            {
                // Store the baseline pollution (without any filter)
                // We need to do this carefully because the pollution might already have a filter applied
                this.baselinePollution = this.pollutionComponent.PollutionTonsPerHour;

                // Check if there's already a filter installed and adjust baseline
                var existingFilter = this.GetInstalledFilter();
                if (existingFilter != null)
                {
                    // Reverse the filter effect to get the true baseline
                    var multiplier = existingFilter.GetPollutionMultiplier();
                    if (multiplier > 0)
                    {
                        this.baselinePollution = this.pollutionComponent.PollutionTonsPerHour / multiplier;
                    }
                }

                // Subscribe to module changes
                this.modulesComponent.OnChanged.Add(this.OnModulesChanged);
                this.isInitialized = true;

                // Apply the filter effect immediately if one exists
                this.ApplyFilterEffect();
            }
        }

        private PollutionFilterModule GetInstalledFilter()
        {
            return this.modulesComponent?.Inventory?.Stacks
                .Select(stack => stack.Item)
                .OfType<PollutionFilterModule>()
                .FirstOrDefault();
        }

        private void OnModulesChanged()
        {
            if (!this.isInitialized) return;
            this.ApplyFilterEffect();
        }

        private void ApplyFilterEffect()
        {
            // Find the current pollution filter module
            var filterModule = this.GetInstalledFilter();

            // Calculate new pollution value
            float newPollution;
            if (filterModule != null)
            {
                var multiplier = filterModule.GetPollutionMultiplier();
                newPollution = this.baselinePollution * multiplier;
            }
            else
            {
                // No filter, use baseline
                newPollution = this.baselinePollution;
            }

            // Update the pollution value using reflection (PollutionTonsPerHour is public so we can set it directly)
            try
            {
                // Access the property - it should be public
                var pollutionProperty = typeof(AirPollutionComponent).GetProperty("PollutionTonsPerHour");
                if (pollutionProperty != null && pollutionProperty.CanWrite)
                {
                    pollutionProperty.SetValue(this.pollutionComponent, newPollution);
                }
                else
                {
                    // Try field access as fallback
                    var pollutionField = typeof(AirPollutionComponent).GetField("PollutionTonsPerHour",
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                    if (pollutionField != null)
                    {
                        pollutionField.SetValue(this.pollutionComponent, newPollution);
                    }
                }

                // Force tooltip update by calling SetStatus() using reflection
                var setStatusMethod = typeof(AirPollutionComponent).GetMethod("SetStatus",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (setStatusMethod != null)
                {
                    setStatusMethod.Invoke(this.pollutionComponent, null);
                }
            }
            catch (Exception ex)
            {
                // Silently fail - pollution reduction will not apply but machine continues working
            }
        }

        public override void Destroy()
        {
            // Unsubscribe from events
            if (this.modulesComponent != null)
            {
                this.modulesComponent.OnChanged.Remove(this.OnModulesChanged);
            }
            base.Destroy();
        }
    }
}
