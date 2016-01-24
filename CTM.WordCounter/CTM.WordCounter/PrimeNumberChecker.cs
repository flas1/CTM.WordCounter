using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator
{
    public class PrimeNumberChecker : IPrimeNumberChecker
    {
        public bool IsPrime(int number)
        {
            if (number > 1)
            {
                var divisorLimit = (int)Math.Floor(Math.Sqrt(number)); 
                for (int i = 2; i <= divisorLimit; i++)
                {
                    if (number % i == 0)
                        return false;
                }

                return true;
            }

            return false;
        }
    }
}
