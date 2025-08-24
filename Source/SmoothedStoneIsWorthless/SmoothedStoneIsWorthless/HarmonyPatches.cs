using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace SmoothedStoneIsWorthless
{
    public class SSiWMod : Mod
    {
        public SSiWMod(ModContentPack content) : base(content)
        {
            new Harmony("Rimworld.Alite.SSiW.main").PatchAll();
        }
    }

    [HarmonyPatch(typeof(TerrainDefGenerator_Stone))]
    [HarmonyPatch(nameof(TerrainDefGenerator_Stone.ImpliedTerrainDefs))]
    public static class SSiW_ImpliedTerrainDefs_Postfix
    {
        public static IEnumerable<TerrainDef> Postfix(IEnumerable<TerrainDef> values)
        {
            var result = values.ToList();
            foreach (var terrainDef in result)
                if (terrainDef.defName.EndsWith("_Smooth"))
                    StatUtility.SetStatValueInList(ref terrainDef.statBases, StatDefOf.MarketValue, 0f);
            return result;
        }
    }
}
