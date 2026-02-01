// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// UserCode override: Meaty Stew gives 1000 calories and 1000 to all nutrition categories.

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Time;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;

    [Serialized]
    [LocDisplayName("Meaty Stew")]
    [Weight(500)]
    [Ecopedia("Food", "Campfire", createAsSubPage: true)]
    [LocDescription("A thick meaty stew. A great source of protein.")]
    public partial class MeatyStewItem : FoodItem
    {
        public override float Calories => 1000;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 1000, Fat = 1000, Protein = 1000, Vitamins = 1000 };
        public override float BaseShelfLife => (float)TimeUtil.HoursToSeconds(72);
    }

    [RequiresSkill(typeof(CampfireCookingSkill), 3)]
    [Ecopedia("Food", "Campfire", subPageName: "Meaty Stew Item")]
    public partial class MeatyStewRecipe : RecipeFamily
    {
        public MeatyStewRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "MeatyStew",  //noloc
                displayName: Localizer.DoStr("Meaty Stew"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CharredMeatItem), 2, typeof(CampfireCookingSkill), typeof(CampfireCookingLavishResourcesTalent)),
                    new IngredientElement(typeof(ScrapMeatItem), 1, typeof(CampfireCookingSkill), typeof(CampfireCookingLavishResourcesTalent)),
                    new IngredientElement(typeof(FlourItem), 1, typeof(CampfireCookingSkill), typeof(CampfireCookingLavishResourcesTalent)),
                    new IngredientElement("Fat", 1, typeof(CampfireCookingSkill), typeof(CampfireCookingLavishResourcesTalent)), //noloc
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<MeatyStewItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(15, typeof(CampfireCookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(MeatyStewRecipe), start: 1, skillType: typeof(CampfireCookingSkill), typeof(CampfireCookingFocusedSpeedTalent), typeof(CampfireCookingParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Meaty Stew"), recipeType: typeof(MeatyStewRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(CampfireObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}
