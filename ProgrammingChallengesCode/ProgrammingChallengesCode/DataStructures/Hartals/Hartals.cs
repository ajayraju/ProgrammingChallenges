using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallengesCode.DataStructures.Hartals
{
    public class Hartals
    {
        /*
         * Each party has its own hartal parameter, which is indicative
         * of the frequency with which they call for a strike.
         * 
         * No hartals are allowed on Fridays or Saturdays.
         * 
         * Aim/Objective :  Given the hartal parameter of different parties,
         * find the number of days lost to hartals.
         */

        public static int Check(int days, int parties, int[] hartalParams)
        {
            int daysLost = 0;
            for (int i = 1; i <= days; i++)
            {
                if (i%7 == 6 || i%7 == 0) continue;
                for (int j = 0; j < parties; j++)
                {
                    if (i%hartalParams[j] == 0)
                    {
                        daysLost++;
                        break;
                    }
                }
            }
            return daysLost;
        }
    }
}
