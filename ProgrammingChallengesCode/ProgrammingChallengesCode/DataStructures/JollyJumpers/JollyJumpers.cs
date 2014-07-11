using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallengesCode.DataStructures.JollyJumpers
{
    public class JollyJumpers
    {
        public static bool IsJollyJummper(int[] input)
        {
            if(input == null || input.Length == 0) throw new ArgumentNullException("input");
            if (input.Length == 1 && input[0] == 0) return false;
            if (input.Length == 1) return true;
            int max = FindMax(input);
            bool[] status =  new bool[max-1];
            for (int i = 1; i < input.Length; i++)
            {
                int difference = Math.Abs(input[i] - input[i - 1]);
                if (difference > max - 1) return false;
                status[difference - 1] = true;
            }
            return CheckStatus(status);
        }

        private static bool CheckStatus(bool[] statusArray)
        {
            for (int i = 0; i < statusArray.Length; i++)
            {
                if (!statusArray[i]) return false;
            }
            return true;
        }

        private static int FindMax(int[] input)
        {
            if(input.Length < 2) throw new ArgumentException("invalid length");
            int max = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > max) max = input[i];
            }
            return max;
        }
    }
}
