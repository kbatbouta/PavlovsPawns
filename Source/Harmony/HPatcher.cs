using System;
using HarmonyLib;
using Pavlovs.Tools;
using Verse;
using Verse.AI;

namespace Pavlovs.HPatches
{
    public static class Patcher
    {
        public static readonly Harmony harmony;

        static Patcher() { harmony = new Harmony(Finder.packageID); }

        public static void Initialize()
        {
            try
            {
                harmony.PatchAll();
            }
            catch (Exception er)
            {
                Logging.Line(er.Message, force: true);
                Logging.Line(er.StackTrace, force: true);

                Logging.Line(er.InnerException.Message, force: true);
                Logging.Line(er.InnerException.StackTrace, force: true);
            }
            finally
            {
                Logging.Line("Finished Patching!!", force: false);
            }
        }
    }
}
