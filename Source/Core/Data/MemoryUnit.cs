using System;
using System.Collections.Generic;
using Pavlovs.Core.Components;
using Pavlovs.Prefs;
using Pavlovs.Tools;
using RimWorld;
using Verse;

namespace Pavlovs.Memories
{
    public class MemoryUnit : IExposable
    {
        private int updateTick = 0;

        private const int HOUR = 2500;
        private const int DAY = 60000;

        private bool created = false;
        private int id;

        public int ID => id;

        public int CurTick => Find.TickManager.TicksGame;
        public bool Created => created;

        public ThingComp_MemoryComp Component => pawn?.TryGetComp<ThingComp_MemoryComp>();

        public Pawn pawn;

        public List<Memory> nodes = new List<Memory>();

        public static List<Centroid> centroids = new List<Centroid>();
        public static HashSet<MentalStateDef> events = new HashSet<MentalStateDef>();

        public void DoTrackingUpdate()
        {
            var t = CurTick;

            if (pawn == null) { return; }

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].messurerTime.Add(t);

                if (pawn.RaceProps.EatsFood)
                    nodes[i].hungerRates.Add(pawn.needs.food.CurLevelPercentage);
                nodes[i].painRates.Add(pawn.health.hediffSet.PainTotal);
            }

            if (updateTick++ % 3 != id % 3) { return; }

            for (int i = 0; i < nodes.Count; i++)
            {
                if (!events.Contains(nodes[i].eventDef))
                {
                    var centroid = new Centroid
                    {
                        eventDef = nodes[i].eventDef,
                        count = 0
                    };

                    events.Add(nodes[i].eventDef);
                    centroids.Add(centroid);
                }

                if (!nodes[i].registered)
                {
                    nodes[i].registered = true;
                    centroids.Find(a => a.eventDef == nodes[i].eventDef).count++;
                }
            }
        }

        public void DoNodesUpdate()
        {
            var removeList = new List<int>();
            var t = CurTick;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (Math.Abs(t - nodes[i].t_0) >= PavlovsPawnsPrefs.ForgetInDays * DAY + PavlovsPawnsPrefs.ForgetInHours * HOUR)
                {
                    removeList.Add(i);
#if DEBUG
                    Logging.Line(Math.Abs(t - nodes[i].t_0) + "");
#endif
                }
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

            //Scribe_Collections.Look(ref centroids, "uAllCentroids", LookMode.Deep);
            //Scribe_Collections.Look(ref events, "uAllEvents", LookMode.Def);
        }
    }
}
