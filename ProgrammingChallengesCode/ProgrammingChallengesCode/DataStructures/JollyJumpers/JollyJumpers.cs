using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallengesCode.DataStructures.JollyJumpers
{
    public enum Status
    {
        Jolly,
        NotJolly
    }

    public class JollyJumpers
    {
        public static Status IsJollyJumper(int[] input)
        {
            if(input == null || input.Length == 0) throw new ArgumentNullException("input");
            if (input.Length == 1 && input[0] == 0) return Status.NotJolly;
            if (input.Length == 1) return Status.Jolly;
            int length = input.Length;
            ISet<int> differences=  new HashSet<int>();
            for (int i = 1; i < length; i++)
            {
                var difference = Math.Abs(input[i] - input[i - 1]);
                if (difference <= length - 1 && difference > 0) differences.Add(difference);
            }
            return differences.Count == length - 1 ? Status.Jolly : Status.NotJolly;
        }
    }
}
