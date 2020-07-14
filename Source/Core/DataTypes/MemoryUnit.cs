using System;
using System.Collections.Generic;
using Pavlovs.Core.Components;
using RimWorld;
using Verse;

namespace Pavlovs.DataTypes
{
    public class MemoryUnit : IExposable
    {
        private const int FORGETINHOURS = 2;
        private const int FORGETINDAYS = 2;

        private const int HOUR = 2500;
        private const int DAY = 2500 * 24;

        private bool created = false;
        private int id;

        public bool Created => created;

        public ThingComp_MemoryComp Component => pawn?.TryGetComp<ThingComp_MemoryComp>();

        public Pawn pawn;

        public class Memory : IExposable
        {
            public MentalStateDef mentalStateDef;

            public int t_0;

            public void ExposeData()
            {
                Scribe_Defs.Look(ref mentalStateDef, "uEventDef");
                Scribe_Values.Look(ref t_0, "uTimeOf");
            }
        }

        public List<Memory> nodes = new List<Memory>();

        public MemoryUnit()
        {

        }

        public void DoTick()
        {

        }

        public void DoUpdate()
        {
            var removeList = new List<int>();
            var t = Find.TickManager.TicksGame;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (t - nodes[i].t_0 >= FORGETINDAYS * DAY + FORGETINHOURS * HOUR) { removeList.Add(i); }
            }
            foreach (int i in removeList) { nodes.RemoveAt(i); }
        }

        public void Create()
        {
            this.created = true;
            this.id = pawn.thingIDNumber;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref id, "uMemoryThingID");
            Scribe_References.Look(ref pawn, "uMemoryPawn");

            Scribe_Collections.Look(ref nodes, false, "uMemoryNodes", LookMode.Deep);
        }
    }
}
