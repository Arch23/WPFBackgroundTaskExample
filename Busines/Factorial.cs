using Busines.Interfaces;
using System;

namespace Busines
{
    public class Factorial : IFactorial
    {
        public int Calculate(int val)
        {
            if (val < 0)
            {
                throw new ArgumentException("Factorial calculation not defined for negative numbers!");
            }

            if (val > 12)
            {
                throw new ArithmeticException("Factoral result exceeds int32 max values!");
            }

            if (val == 1 || val == 0)
            {
                return 1;
            }

            return val * Calculate(--val);
        }
    }
}
