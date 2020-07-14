using System;
using System.Collections.Generic;
using Pavlovs.Core.Components;
using Pavlovs.Prefs;
using Pavlovs.Tools;
using RimWorld;
using Verse;

namespace Pavlovs.Memories
{
    public class Centroid : IExposable
    {
        public MentalStateDef eventDef;

        public int count;

        public void ExposeData()
        {
            Scribe_Defs.Look(ref eventDef, "cEventDef");
            Scribe_Values.Look(ref count, "cTimes");
        }
    }
}
