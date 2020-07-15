using System;
using System.Collections.Generic;
using System.Linq;
using Pavlovs.Tools;
using Pavlovs.Utilities;
using Verse;

namespace Pavlovs.Data
{
    public class MarkovNode : IExposable
    {
        public MarkovChain parent;

        public string State;
        public Dictionary<string, int> next = new Dictionary<string, int>();

        private List<string> keys;
        private List<int> values;

        public void ExposeData()
        {
            keys?.Clear();
            values?.Clear();

            Scribe_Collections.Look(ref next, "next", keyLookMode: LookMode.Value, valueLookMode: LookMode.Value, ref keys, ref values);
            Scribe_Values.Look(ref State, "state");
        }
    }
}
