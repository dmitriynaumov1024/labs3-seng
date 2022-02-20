using System;
using System.Collections.Generic;
using System.Text;

class NumberModel
{
    public SortedSet<int> GetDivisors (int number) {
        SortedSet<int> result = new SortedSet<int>();
        if (number < 1) return result;
        int numberSqrt = (int)Math.Sqrt(number);
        for (int i=1; i<=numberSqrt; i++) {
            if (number % i == 0) {
                result.Add(i);
                result.Add(number / i);
            }
        }
        return result;
    }
}
