using System;
using HarmonyLib;
using Pavlovs.Data;
using Pavlovs.Tools;
using Verse;
using Verse.AI;

namespace Pavlovs.HPatches.HMentalState
{
    [HarmonyPatch(typeof(MentalStateWorker))]
    [HarmonyPatch(nameof(MentalStateWorker.StateCanOccur))]
    public static class Patch_StateCanOccur
    {
        public static void Postfix(ref bool __result, MentalStateWorker __instance, Pawn pawn)
        {
            if (!__result) { return; }

            Logging.Warning("About to start" + __instance.def);
        }
    }
}
