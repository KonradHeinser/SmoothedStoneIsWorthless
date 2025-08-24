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
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        private static readonly Type patchType = typeof(HarmonyPatches);
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("Rimworld.Alite.SSiW.main");

            harmony.Patch(AccessTools.Method(typeof(TerrainDefGenerator_Stone), nameof(TerrainDefGenerator_Stone.ImpliedTerrainDefs)),
                postfix: new HarmonyMethod(patchType, nameof(ImpliedTerrainDefsPostfix)));
        }

        public static void ImpliedTerrainDefsPostfix(ref IEnumerable<TerrainDef> __result)
        {
            var result = new List<TerrainDef>(__result);
            foreach (var terrainDef in result)
                if (terrainDef.defName.EndsWith("_Smooth"))
                    StatUtility.SetStatValueInList(ref terrainDef.statBases, StatDefOf.MarketValue, 0f);
            __result = result;
        }
    }
}
