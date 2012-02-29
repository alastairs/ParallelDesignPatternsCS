using System;
using System.Linq;

namespace ParallelDesignPatterns
{
    class MinValueFinder
    {
        private readonly Func<int, int> f;

        public MinValueFinder(Func<int, int> f)
        {
            this.f = f;
        }

        internal int FindMinimum(int a, int b)
        {
            int numberOfInputs = b - a + 1;
            return Enumerable.Range(a, numberOfInputs).Select(f).Min();
        }
    }
}
