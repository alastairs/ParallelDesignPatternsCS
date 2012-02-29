using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;

namespace ParallelDesignPatterns
{
    

    [TestFixture]
    class MinValueFinderTest
    {
        private Func<int, int>  square = x => x * x;
        private Func<int, int> negative = x => -x;
        private Func<int, int> slowNegative = x => { Thread.Sleep(200); return -x; };

        [Test]
        public void shouldReturnTheFunctionValueForOneElementLongInterval()
        {
            Assert.AreEqual(new MinValueFinder(square).FindMinimum(-1, -1), 1);
        }

        [Test]
        public void givenSqrFunctionAndIntervalFrom2To5ShouldResultIn4()
        {
            Assert.AreEqual(new MinValueFinder(square).FindMinimum(2, 5), 4);
        }

        [Test]
        public void givenSqrFunctionAndIntervalFromMinus4To10ShouldResultIn0()
        {
            Assert.AreEqual(new MinValueFinder(square).FindMinimum(-4, 10), 0);
        }

        [Test]
        public void givenNegFunctionAndIntervalFromMinus4To10ShouldResultInMinus10()
        {
            Assert.AreEqual(new MinValueFinder(negative).FindMinimum(-4, 10), -10);
        }

        [Test, Timeout(1000)]
        public void shouldFinishInTime()
        {
            Assert.AreEqual(new MinValueFinder(slowNegative).FindMinimum(-4, 10), -10);
        }
    }
}


