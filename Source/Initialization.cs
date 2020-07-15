using System;
using Pavlovs.HPatches;
using Verse;

namespace Pavlovs
{
    [StaticConstructorOnStartup]
    public static class Initialization
    {
        static Initialization()
        {
            Patcher.Initialize();
        }
    }
}