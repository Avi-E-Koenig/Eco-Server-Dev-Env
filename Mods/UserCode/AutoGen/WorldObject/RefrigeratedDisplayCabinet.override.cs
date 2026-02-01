// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// UserCode override: adds PublicStorageComponent so the cabinet can store items.
// Full override required because .override.cs replaces the core file (no partial method declaration from core).

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Shared.Networking;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Gameplay.Civics.Objects;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Items.Recipes;

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Stores", subPageName: "Refrigerated Display Cabinet Item")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]

    public partial class RefrigeratedDisplayCabinetObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(RefrigeratedDisplayCabinetItem);
        public override LocString DisplayName => Localizer.DoStr("Refrigerated Display Cabinet");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<PowerConsumptionComponent>().Initialize(100);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<CustomTextComponent>().Initialize(700);
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(36);
            storage.ShelfLifeMultiplier = 2.0f;
            storage.Storage.AddInvRestriction(new StackLimitRestriction(500));
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Refrigerated Display Cabinet")]
    [LocDescription("A temperature regulated display storage.")]
    [Ecopedia("Crafted Objects", "Stores", createAsSubPage: true)]
    [Weight(2000)]
    public partial class RefrigeratedDisplayCabinetItem : WorldObjectItem<RefrigeratedDisplayCabinetObject>
    {
        [NewTooltip(CacheAs.SubType, 50)] public static LocString UpdateTooltip() => Localizer.Do($"{Localizer.DoStr("Increases")} total shelf life by: {Text.InfoLight(Text.Percent(0.9f))}").Dash();
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext( 0  | DirectionAxisFlags.Down , WorldObject.GetOccupancyInfo(this.WorldObjectType));

        [NewTooltip(CacheAs.SubType, 7)] public static LocString PowerConsumptionTooltip() => Localizer.Do($"Consumes: {Text.Info(100)}w of {new ElectricPower().Name} power.");
    }

    [RequiresSkill(typeof(CarpentrySkill), 4)]
    [Ecopedia("Crafted Objects", "Stores", subPageName: "Refrigerated Display Cabinet Item")]
    public partial class RefrigeratedDisplayCabinetRecipe : RecipeFamily
    {
        public RefrigeratedDisplayCabinetRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "RefrigeratedDisplayCabinet",  //noloc
                displayName: Localizer.DoStr("Refrigerated Display Cabinet"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PaperItem), 50, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                    new IngredientElement("Lumber", 14, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), //noloc
                    new IngredientElement("WoodBoard", 16, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), //noloc
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<RefrigeratedDisplayCabinetItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 5;
            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(CarpentrySkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(RefrigeratedDisplayCabinetRecipe), start: 2, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Refrigerated Display Cabinet"), recipeType: typeof(RefrigeratedDisplayCabinetRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(SawmillObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}
