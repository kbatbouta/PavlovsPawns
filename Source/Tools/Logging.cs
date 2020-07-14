using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace Pavlovs.Tools
{
    public static class Logging
    {
        private static StringBuilder buffer = new StringBuilder();

        private static StringBuilder Header
        {
            get
            {
                Logging.buffer.Clear();
                return Logging.buffer.Append(
                    "[").Append(
                    Finder.packageID.ToLower()).Append(
                    "]: ");
            }
        }

        public static void List(IEnumerable<Object> aList, string title = "List", bool force = false)
        {
            Header.Append(title);

            foreach (Object obj in aList) { buffer.AppendInNewLine(obj.ToString()); }

            if (force) { Log.Message(buffer.ToString()); }
            else
            {
#if DEBUG
                Log.Message(buffer.ToString());
#endif
            }
        }

        public static void Line(string message, bool force = false)
        {
            Header.Append(message);

            if (force)
            {
                Log.Message(buffer.ToString());
            }
            else
            {
#if DEBUG
                Log.Message(buffer.ToString());
#endif
            }
        }

        public static void Warning(string message)
        {
            Logging.Line((new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name,
                force: true);
            Log.Warning(Header.Append(message).ToString());
        }

        public static void Error(string message)
        {
            Logging.Line((new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name,
                force: true);
            Log.Error(Header.Append(message).ToString());
        }
    }
}
