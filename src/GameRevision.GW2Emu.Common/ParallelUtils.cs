using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRevision.GW2Emu.Common
{
    ///<summary>
    ///Class taken from Stackoverflow: http://stackoverflow.com/questions/8671771/whats-the-best-way-of-achieving-a-parallel-infinite-loop
    ///</summary>
    public class ParallelUtils
    {
        public static void While(Func<bool> condition, Action body) 
        {
            Parallel.ForEach(IterateUntilFalse(condition), new Action<bool>(delegate
            {
                body.Invoke();
            })); 
        }

        private static IEnumerable<bool> IterateUntilFalse(Func<bool> condition) 
        {
            while (condition())
            {
                yield return true;
            }
        }
    }
}

