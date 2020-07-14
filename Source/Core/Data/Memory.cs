using System;
using System.Collections.Generic;
using Pavlovs.Core.Components;
using Pavlovs.Prefs;
using Pavlovs.Tools;
using RimWorld;
using Verse;

namespace Pavlovs.Memories
{
    public class Memory : IExposable
    {
        public bool registered = false;

        public MentalStateDef eventDef;

        public List<int> messurerTime =
            new List<int>();

        public List<float> painRates =
            new List<float>();
        public List<float> hungerRates =
            new List<float>();

        public int t_0;

        public void ExposeData()
        {
            Scribe_Defs.Look(ref eventDef, "uEventDef");
            Scribe_Values.Look(ref t_0, "uTimeOf");
            Scribe_Values.Look(ref registered, "uRegistered");

            Scribe_Collections.Look(ref messurerTime, "uMessurerTime", LookMode.Value);
            Scribe_Collections.Look(ref painRates, "uPainRates", LookMode.Value);
            Scribe_Collections.Look(ref hungerRates, "uHungerRates", LookMode.Value);
        }
    }
}
