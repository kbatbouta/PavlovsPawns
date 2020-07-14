using System;
using System.Linq;
using HarmonyLib;
using Pavlovs.DataTypes;
using Pavlovs.Tools;
using Verse;
using Verse.AI;

namespace Pavlovs.HPatches.HMentalBreak
{
    [HarmonyPatch(typeof(Verse.AI.MentalBreakWorker))]
    [HarmonyPatch(nameof(MentalBreakWorker.TryStart))]
    public static class Patch_TryStart
    {
        public static void Postfix(Pawn pawn, string reason, bool causedByMood, bool __result, MentalBreakWorker __instance)
        {
            if (!__result) { return; }

            if (Finder.GameMemories.IsTracked(pawn, out MemoryUnit unit))
            {
                var t_0 = Find.TickManager.TicksGame;
                if (unit.nodes.Count > 0)
                {
                    if (unit.nodes.Last().t_0 == t_0) { return; }
                }
                unit.nodes.Add(new MemoryUnit.Memory
                {
                    t_0 = t_0,
                    mentalStateDef = __instance.def.mentalState
                });
            }
        }
    }
}
