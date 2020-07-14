using System;
using HugsLib;
using Pavlovs.Memories;
using Pavlovs.Data;
using Pavlovs.Tools;
using Verse;

namespace Pavlovs.Core.Components
{
    public class ThingComp_MemoryComp : ThingComp
    {
        public MemoryUnit memoryUnit;

        public Pawn Pawn => parent as Pawn;

        public ThingComp_MemoryComp()
        {

        }

        public void CompCall()
        {
            if (this.memoryUnit == null) { return; }

            if (!this.memoryUnit?.Created ?? false)
            {
                this.memoryUnit.Create();
            }

            this.memoryUnit.DoNodesUpdate();
            this.memoryUnit.DoTrackingUpdate();
        }


        public void Register()
        {
            if (Finder.GameMemories.IsTracked(Pawn, out MemoryUnit value))
            {
                this.memoryUnit = value;
            }
            else
            {
                this.memoryUnit = new MemoryUnit
                {
                    pawn = parent as Pawn
                };

                this.memoryUnit.Create();
                Finder.GameMemories.AddMemory(this.memoryUnit);
            }
        }

        public void Unregister()
        {
            Finder.GameMemories.RemoveMemory(memoryUnit);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.Register();

            HugsLibController.Instance.DistributedTicker.RegisterTickability(CompCall, Prefs.PavlovsPawnsPrefs.MessureInterval, parent);
        }

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            base.PostDestroy(mode, previousMap);
            this.Unregister();
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
        }
    }
}
