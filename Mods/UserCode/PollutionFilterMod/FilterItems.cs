// Copyright (c) Strange Loop Games. All rights reserved.
// Pollution Filter Mod - Filter Item Definitions

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Core.Items;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Utils;

    // ============================================
    // BASIC POLLUTION FILTER - Tier 1
    // ============================================
    [Serialized]
    [LocDisplayName("Basic Pollution Filter")]
    [LocDescription("A basic filtration module that reduces air pollution from machines by 20%. Can be installed in any machine with a module slot.")]
    [Weight(2000)]
    [Tag("AdvancedUpgrade")]
    [Tag("ModernUpgrade")]
    [Ecopedia("Items", "Upgrade", createAsSubPage: true)]
    public partial class BasicPollutionFilterItem : PollutionFilterModule
    {
        public override float PollutionReduction => 0.20f; // 20% reduction
        public override string FilterTier => "Basic";
    }

    [RequiresSkill(typeof(MechanicsSkill), 2)]
    public partial class BasicPollutionFilterRecipe : RecipeFamily
    {
        public BasicPollutionFilterRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BasicPollutionFilter",
                displayName: Localizer.DoStr("Basic Pollution Filter"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronBarItem), 10, typeof(MechanicsSkill)),
                    new IngredientElement(typeof(SteelBarItem), 5, typeof(MechanicsSkill)),
                    new IngredientElement("Fabric", 10, typeof(MechanicsSkill))
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BasicPollutionFilterItem>()
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 5;
            this.LaborInCalories = CreateLaborInCaloriesValue(250);
            this.CraftMinutes = CreateCraftTimeValue(10f);
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Basic Pollution Filter"), recipeType: typeof(BasicPollutionFilterRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    // ============================================
    // ADVANCED POLLUTION FILTER - Tier 2
    // ============================================
    [Serialized]
    [LocDisplayName("Advanced Pollution Filter")]
    [LocDescription("An advanced multi-stage filtration system that reduces air pollution from machines by 40%.")]
    [Weight(3000)]
    [Tag("AdvancedUpgrade")]
    [Tag("ModernUpgrade")]
    [Ecopedia("Items", "Upgrade", createAsSubPage: true)]
    public partial class AdvancedPollutionFilterItem : PollutionFilterModule
    {
        public override float PollutionReduction => 0.40f; // 40% reduction
        public override string FilterTier => "Advanced";
    }

    [RequiresSkill(typeof(MechanicsSkill), 4)]
    public partial class AdvancedPollutionFilterRecipe : RecipeFamily
    {
        public AdvancedPollutionFilterRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AdvancedPollutionFilter",
                displayName: Localizer.DoStr("Advanced Pollution Filter"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SteelBarItem), 20, typeof(MechanicsSkill)),
                    new IngredientElement(typeof(CopperBarItem), 10, typeof(MechanicsSkill)),
                    new IngredientElement("Fabric", 20, typeof(MechanicsSkill)),
                    new IngredientElement(typeof(BasicPollutionFilterItem), 1, true)
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<AdvancedPollutionFilterItem>()
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 10;
            this.LaborInCalories = CreateLaborInCaloriesValue(500);
            this.CraftMinutes = CreateCraftTimeValue(20f);
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Advanced Pollution Filter"), recipeType: typeof(AdvancedPollutionFilterRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(RoboticAssemblyLineObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    // ============================================
    // INDUSTRIAL POLLUTION FILTER - Tier 3
    // ============================================
    [Serialized]
    [LocDisplayName("Industrial Pollution Filter")]
    [LocDescription("A state-of-the-art industrial filtration system that reduces air pollution from machines by 60%.")]
    [Weight(5000)]
    [Tag("AdvancedUpgrade")]
    [Tag("ModernUpgrade")]
    [Ecopedia("Items", "Upgrade", createAsSubPage: true)]
    public partial class IndustrialPollutionFilterItem : PollutionFilterModule
    {
        public override float PollutionReduction => 0.60f; // 60% reduction
        public override string FilterTier => "Industrial";
    }

    [RequiresSkill(typeof(IndustrySkill), 6)]
    public partial class IndustrialPollutionFilterRecipe : RecipeFamily
    {
        public IndustrialPollutionFilterRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "IndustrialPollutionFilter",
                displayName: Localizer.DoStr("Industrial Pollution Filter"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SteelBarItem), 40, typeof(IndustrySkill)),
                    new IngredientElement(typeof(PlasticItem), 20, typeof(IndustrySkill)),
                    new IngredientElement(typeof(GoldBarItem), 5, typeof(IndustrySkill)),
                    new IngredientElement("Fabric", 40, typeof(IndustrySkill)),
                    new IngredientElement(typeof(AdvancedPollutionFilterItem), 1, true)
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<IndustrialPollutionFilterItem>()
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 20;
            this.LaborInCalories = CreateLaborInCaloriesValue(1000);
            this.CraftMinutes = CreateCraftTimeValue(30f);
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Industrial Pollution Filter"), recipeType: typeof(IndustrialPollutionFilterRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ElectronicsAssemblyObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}
