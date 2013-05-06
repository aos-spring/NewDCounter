using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace DCounterCommunication
{
    class Counter
    {
        public string Key;
        public long Value;

    }
    public static class Core
    {
        static Dictionary<string, Counter> counter = new Dictionary<string, Counter>(100);

        static Core()
        {
            counter.Add("test", new Counter { Key = "test", Value = 0 });
            counter.Add("testa", new Counter { Key = "testa", Value = 0 });
            counter.Add("testb", new Counter { Key = "testb", Value = 0 });
        }

        static public bool SetCounter(string key, long count)
        {
            Counter value = null;
            if (!counter.TryGetValue(key, out value))
            {
                return false;
            }
            value.Value = count;
            return true;
        }
        static public bool AddCounter(string key)
        {
            Counter value = null;
            if (counter.TryGetValue(key, out value))
            {
                return false;
            }
            counter[key] = new Counter { Key = key, Value = 0 };
            return true;
        }

        static public long GetCounter(string key)
        {

            Counter value = null;
            if (!counter.TryGetValue(key, out value))
            {
                return -1;
            }
            return Interlocked.Add(ref value.Value, 1);
        }
    }
}
