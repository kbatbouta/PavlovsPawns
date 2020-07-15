using System;
using HarmonyLib;
using Pavlovs.Data;
using Pavlovs.Tools;
using Verse;
using Verse.AI;

namespace Pavlovs.HPatches.Patch_JobGiver
{
    [HarmonyPatch(typeof(ThinkNode_JobGiver))]
    [HarmonyPatch(nameof(ThinkNode_JobGiver.TryIssueJobPackage))]
    public static class Patch_JobGiver
    {
        public static void Postfix(ThinkResult __result, Pawn pawn)
        {
            if (__result == ThinkResult.NoJob) { return; }

            if (__result.Job.workGiverDef != null)
            {
                if (Finder.PawnTracker.chainsForPawns.TryGetValue(pawn, out MarkovChain chain))
                {
                    chain.AddNext(__result.Job.workGiverDef.ToString());
                }
                else
                {
                    Finder.PawnTracker.chainsForPawns.Add(pawn, new MarkovChain());
                    Finder.PawnTracker.chainsForPawns[pawn].AddNext(__result.Job.workGiverDef.ToString());
                }
            }
        }
    }
}
