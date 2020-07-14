using System;
using System.Collections.Generic;
using System.Linq;
using Pavlovs.Memories;
using Pavlovs.Data;
using Pavlovs.Tools;
using RimWorld.Planet;
using Verse;

namespace Pavlovs.Core.Components
{
    public class GameComponent_MemoryTracker : WorldComponent
    {
        private List<MemoryUnit> memories;

        public GameComponent_MemoryTracker(World world) : base(world)
        {
            this.memories = new List<MemoryUnit>();
            Finder.GameMemories = this;
        }

        public bool AddMemory(MemoryUnit unit)
        {
            if (this.memories.Any(t => t.ID == unit.ID)) { return false; }
            memories.Add(unit); return true;
        }

        public bool RemoveMemory(MemoryUnit unit)
        {
            if (!this.memories.Any(t => t.ID == unit.ID)) { return false; }
            this.memories.Remove(unit); return true;
        }

        public bool IsTracked(Pawn pawn)
        {
            if (pawn == null) { return false; }

            var comp = pawn.TryGetComp<ThingComp_MemoryComp>();

            if (comp == null) { return false; }
            if (comp.memoryUnit == null) { return false; }

            return this.memories.Any(t => t.ID == comp.memoryUnit.ID);
        }

        public bool IsTracked(Pawn pawn, out MemoryUnit unit)
        {
            unit = null;

            if (pawn == null) { return false; }

            var comp = pawn.TryGetComp<ThingComp_MemoryComp>();
            unit = comp?.memoryUnit ?? null;

            if (comp == null) { return false; }
            if (comp.memoryUnit == null) { return false; }

            return this.memories.Any(t => t.ID == comp.memoryUnit.ID);
        }

        public bool IsTracked(MemoryUnit unit)
        {
            if (this.memories.Any(t => t.ID == unit.ID)) { return true; }
            return false;
        }

        public bool IsTracked(MemoryUnit unit, out Pawn pawn)
        {
            if (this.memories.Any(t => t.ID == unit.ID))
            {
                pawn = unit.pawn;
                return true;
            }
            pawn = null;
            return false;
        }

        public override void ExposeData()
        {
            memories = memories.Where(t => t.pawn != null).ToList();
            Scribe_Collections.Look(ref memories,
                saveDestroyedThings: false,
                "uMemories",
                lookMode: LookMode.Deep);

            Scribe_Collections.Look(ref MemoryUnit.centroids, "uAllCentroids", LookMode.Deep);
            Scribe_Collections.Look(ref MemoryUnit.events, "uAllEvents", LookMode.Def);
        }
    }
}
