using System;
using System.Collections.Generic;
using System.Linq;
using Pavlovs.Tools;
using Verse;

namespace Pavlovs.Data
{
    public class MarkovChain : IExposable
    {
        public string curState = null;

        public List<MarkovNode> nodes = new List<MarkovNode>();

        public List<string> States => index.Keys.ToList();

        private Dictionary<string, MarkovNode> index = new Dictionary<string, MarkovNode>();

        public void AddNext(string state)
        {
            MarkovNode node = null;

            if (!index.TryGetValue(state, out node))
            {
                node = new MarkovNode()
                {
                    State = state,
                    parent = this
                };

                this.index.Add(state, node);
                this.nodes.Add(node);
            }

            if (curState == null)
            {
                curState = state; return;
            }

            // link to current state.
            if (index[curState].next.ContainsKey(state))
            {
                index[curState].next[state] += 1;
            }
            else
            {
                index[curState].next.Add(state, 1);
            }

            // don't double link to self.
            if (curState == state) { return; }

            if (node.next.ContainsKey(curState))
            {
                node.next[curState] += 1;
            }
            else
            {
                node.next.Add(curState, 1);
            }
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref nodes, "nodes", LookMode.Deep);
            Scribe_Values.Look(ref curState, "curState");

            index?.Clear();
            foreach (MarkovNode node in nodes)
            {
                index.Add(node.State, node);
                node.parent = this;
            }
        }
    }
}
