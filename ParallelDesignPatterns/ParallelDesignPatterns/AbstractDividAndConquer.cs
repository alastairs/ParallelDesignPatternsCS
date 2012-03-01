using System;
using System.Threading.Tasks;

namespace ParallelDesignPatterns
{
    abstract class AbstractDividAndConquer<TProblem,TSolution>
    {
        protected abstract TProblem[] Split(TProblem problem);
        protected abstract Boolean IsBaseCase(TProblem problem);
        protected abstract TSolution BaseCaseSolve(TProblem problem);
        protected abstract TSolution Merge(TSolution[] subSolutions);

        protected TSolution Solve(TProblem p)
        {
            if (IsBaseCase(p))
            {
                return BaseCaseSolve(p);
            }

            TProblem[] subProblems = Split(p);
            var subSolutions = new TSolution[subProblems.Length];
            
            int i = 0;
            Parallel.ForEach(subProblems, subProblem =>
            {
                subSolutions[i++] = Solve(subProblem);
            });

            return Merge(subSolutions);
        }
    }
}
