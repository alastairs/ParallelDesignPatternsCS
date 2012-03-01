using System;
using System.Linq;

namespace ParallelDesignPatterns
{
    class MinValueFinder : AbstractDividAndConquer<Problem, int>
    {
        private readonly Func<int, int> function;

        public MinValueFinder(Func<int, int> function)
        {
            this.function = function;
        }

        internal int FindMinimum(int a, int b)
        {
            return Solve(new Problem(function, a, b));
        }

        protected override Problem[] Split(Problem problem)
        {
            int rangeOfInput = problem.UpperBound - problem.LowerBound;
            int boundary;
            if (rangeOfInput % 2 == 0)
            {
                boundary = rangeOfInput / 2;
            }
            else
            {
                // Handle the truncation.
                boundary = (rangeOfInput / 2) + 1;
            }

            return new[]
                       {
                           new Problem(function, problem.LowerBound, problem.LowerBound + boundary-1),
                           new Problem(function, problem.UpperBound - boundary, problem.UpperBound)
                       };
        }

        protected override bool IsBaseCase(Problem problem)
        {
            // Naive implementation creates a new thread for every division of the list
            // until the list can be divided no more.
            return problem.UpperBound == problem.LowerBound;
        }

        protected override int BaseCaseSolve(Problem problem)
        {
            // Doesn't matter whether we return LowerBound or UpperBound here, as they
            // should be equal.
            return problem.LowerBound;
        }

        protected override int Merge(int[] subSolutions)
        {
            return subSolutions.Min();
        }
    }

    class Problem
    {
        public Problem(Func<int, int> function, int lowerBound, int upperBound)
        {
            Function = function;
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        protected Func<int, int> Function { get; private set; }

        public int LowerBound { get; private set; }

        public int UpperBound { get; private set; }
    }
}
