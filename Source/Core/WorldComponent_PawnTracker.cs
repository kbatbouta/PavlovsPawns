using System;
using System.Collections.Generic;
using FLS;
using FLS.Rules;
using JetBrains.Annotations;
using Pavlovs.Data;
using Pavlovs.Tools;
using RimWorld.Planet;
using Verse;

namespace Pavlovs.Core
{
    public class WorldComponent_PawnTracker : WorldComponent
    {
        private List<Pawn> pawns;
        private List<MarkovChain> chains;

        public Dictionary<Pawn, MarkovChain> chainsForPawns = new Dictionary<Pawn, MarkovChain>();

        public WorldComponent_PawnTracker(World world) : base(world)
        {
            Finder.PawnTracker = this;
        }

        public override void ExposeData()
        {
            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                this.pawns?.Clear();
                this.chains?.Clear();
            }

            chainsForPawns.RemoveAll(t => (t.Key?.Destroyed ?? true) || (t.Key.Dead));

            Scribe_Collections.Look(ref chainsForPawns, "chainsForPawns", keyLookMode: LookMode.Reference, valueLookMode: LookMode.Deep, ref pawns, ref chains);
        }
    }
}
