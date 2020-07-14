using System;
using System.Collections.Generic;
using System.Linq;
using Pavlovs.DataTypes;
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
            if (this.memories.Contains(unit)) { return false; }
            memories.Add(unit); return true;
        }

        public bool RemoveMemory(MemoryUnit unit)
        {
            if (!this.memories.Contains(unit)) { return false; }
            this.memories.Remove(unit); return true;
        }

        public bool IsTracked(Pawn pawn)
        {
            if (pawn == null) { return false; }

            var comp = pawn.TryGetComp<ThingComp_MemoryComp>();

            if (comp == null) { return false; }
            if (comp.memoryUnit == null) { return false; }

            return this.memories.Contains(comp.memoryUnit);
        }

        public bool IsTracked(Pawn pawn, out MemoryUnit unit)
        {
            unit = null;

            if (pawn == null) { return false; }

            var comp = pawn.TryGetComp<ThingComp_MemoryComp>();
            unit = comp?.memoryUnit ?? null;

            if (comp == null) { return false; }
            if (comp.memoryUnit == null) { return false; }

            return this.memories.Contains(comp.memoryUnit);
        }

        public bool IsTracked(MemoryUnit unit)
        {
            if (this.memories.Contains(unit)) { return true; }
            return false;
        }

        public bool IsTracked(MemoryUnit unit, out Pawn pawn)
        {
            if (this.memories.Contains(unit))
            {
                pawn = unit.pawn;
                return true;
            }
            pawn = null;
            return false;
        }

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref memories, saveDestroyedThings: false, "uMemories", lookMode: LookMode.Deep);
        }
    }
}
