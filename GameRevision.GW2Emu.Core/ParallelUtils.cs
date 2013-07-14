using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRevision.GW2Emu.Core
{
    ///<summary>
    ///Class taken from Stackoverflow: http://stackoverflow.com/questions/8671771/whats-the-best-way-of-achieving-a-parallel-infinite-loop
    ///</summary>
    public class ParallelUtils
    {
        public static void While(Func<bool> condition, Action body) 
        { 
            Parallel.ForEach(IterateUntilFalse(condition), (noparama) => body()); 
        }

        private static IEnumerable<bool> IterateUntilFalse(Func<bool> condition) 
        { 
            while (condition()) yield return true; 
        }
    }
}

