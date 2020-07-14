﻿using System;
using System.Linq;
using HarmonyLib;
using Pavlovs.DataTypes;
using Pavlovs.Tools;
using Verse;
using Verse.AI;

namespace Pavlovs.HPatches.HMentalState
{
    [HarmonyPatch(typeof(MentalStateHandler))]
    [HarmonyPatch(nameof(MentalStateHandler.TryStartMentalState))]
    public static class Patch_TryStartMentalState
    {
        public static void Postfix(ref bool __result, MentalStateWorker __instance, Pawn ___pawn,
            MentalStateDef stateDef, string reason = null, bool forceWake = false,
            bool causedByMood = false, Pawn otherPawn = null, bool transitionSilently = false)
        {
            if (!__result) { return; }

            if (Finder.GameMemories.IsTracked(___pawn, out MemoryUnit unit))
            {
                var t_0 = Find.TickManager.TicksGame;
                if (unit.nodes.Count > 0)
                {
                    if (unit.nodes.Last().t_0 == t_0) { return; }
                }
                unit.nodes.Add(new MemoryUnit.Memory
                {
                    t_0 = t_0,
                    mentalStateDef = stateDef
                });
            }
        }
    }
}