using System;
using Pavlovs.Data;
using Verse;

namespace Pavlovs.Core.Trackers
{
    public class ThingComp_PawnTrack : ThingComp
    {
        public Pawn Pawn => parent as Pawn;

        public MarkovChain chain;

        public ThingComp_PawnTrack()
        {

        }
    }
}
