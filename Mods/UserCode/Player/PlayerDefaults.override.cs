// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// UserCode override: starter campsite inventory replaced with modern tools + Meaty Stew.

using System;
using System.Collections.Generic;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;

// default starting player items / skills
public static class PlayerDefaults
{
    public static Dictionary<Type, int> GetDefaultToolbar()
    {
        return new Dictionary<Type, int>
        {
        };
    }

    public static Dictionary<Type, int> GetDefaultInventory()
    {
        return new Dictionary<Type, int>
        {
            { typeof(StarterCampItem), 1 },
        };
    }

    /// <summary>Default items placed in the campsite when a starting player places it for the first time.
    /// Override: full set of modern tools + 50 Meaty Stew (no stone tools).</summary>
    public static Dictionary<Type, int> GetDefaultCampsiteInventory()
    {
        return new Dictionary<Type, int>
        {
            { typeof(ClaimToolItem), 1 },
            { typeof(ModernPickaxeItem), 1 },
            { typeof(ModernAxeItem), 1 },
            { typeof(ModernShovelItem), 1 },
            { typeof(ModernHammerItem), 1 },
            { typeof(TorchItem), 1 },
            { typeof(MeatyStewItem), 50 },
        };
    }

    public static IEnumerable<Type> GetSkillsForcedToLevelUp()
    {
        return new Type[]
        {
            typeof(SurvivalistSkill),
            typeof(SelfImprovementSkill),
        };
    }

    public static IEnumerable<Type> GetDefaultSkills()
    {
        return new Type[]
        {
            typeof(CarpenterSkill),
            typeof(LoggingSkill),
            typeof(MasonSkill),
            typeof(MiningSkill),
            typeof(ChefSkill),
            typeof(FarmerSkill),
            typeof(GatheringSkill),
            typeof(CampfireCookingSkill),
            typeof(HunterSkill),
            typeof(HuntingSkill),
            typeof(SurvivalistSkill),
            typeof(SelfImprovementSkill),
        };
    }

    static Dictionary<UserStatType, IDynamicValue> dynamicValuesDictionary = new Dictionary<UserStatType, IDynamicValue>()
    {
        {
            UserStatType.MaxCalories, new MultiDynamicValue(MultiDynamicOps.Sum,
                new MultiDynamicValue(MultiDynamicOps.Multiply,
                    CreateSmv(0f,  new BonusUnitsDecoratorStrategy(SelfImprovementSkill.AdditiveStrategy, "cal", (float val) => val/2f), typeof(SelfImprovementSkill), Localizer.DoStr("stomach capacity"), DynamicValueType.Misc),
                    new ConstantValue(0.5f)),
                    new TalentModifiedValue(typeof(UserStatType), typeof(SelfImprovementGluttonTalent), 0),
                new ConstantValue(3000))
        },
        {
            UserStatType.MaxCarryWeight, new MultiDynamicValue(MultiDynamicOps.Sum,
                CreateSmv(0f, new BonusUnitsDecoratorStrategy(SelfImprovementSkill.AdditiveStrategy, "kg", (float val) => val/1000f), typeof(SelfImprovementSkill), Localizer.DoStr("carry weight"), DynamicValueType.Misc),
                new TalentModifiedValue(typeof(UserStatType), typeof(SelfImprovementDeeperPocketsTalent), 0),
                new ConstantValue(ToolbarBackpackInventory.DefaultWeightLimit))
        },
        {
            UserStatType.CalorieRate, new MultiDynamicValue(MultiDynamicOps.Sum,
                new ConstantValue(1))
        },
        {
            UserStatType.DetectionRange, new MultiDynamicValue(MultiDynamicOps.Sum,
                CreateSmv(0f, HuntingSkill.AdditiveStrategy, typeof(HuntingSkill), Localizer.DoStr("how close you can approach animals"), DynamicValueType.Misc),
                new ConstantValue(0))
        },
        {
            UserStatType.MovementSpeed, new MultiDynamicValue(MultiDynamicOps.Sum,
                new TalentModifiedValue(typeof(UserStatType), typeof(SelfImprovementNatureAdventurerSpeedTalent), 0),
                new TalentModifiedValue(typeof(UserStatType), typeof(SelfImprovementUrbanTravellerSpeedTalent), 0))
        }
    };

    private static SkillModifiedValue CreateSmv(float startValue, ModificationStrategy strategy, Type skillType, LocString benefitsDescription, DynamicValueType valueType)
    {
        var smv = new SkillModifiedValue(startValue, strategy, skillType, typeof(Player), benefitsDescription, valueType);
        return smv;
    }

    public static Dictionary<UserStatType, IDynamicValue> GetDefaultDynamicValues()
    {
        return dynamicValuesDictionary;
    }
}
