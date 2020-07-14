using System;
using Pavlovs.Core.Components;

namespace Pavlovs.Tools
{
    public static class Finder
    {
        public static readonly string packageID = "krkr.PavlovsPawns";

        public static GameComponent_MemoryTracker GameMemories { get; internal set; }
    }
}
