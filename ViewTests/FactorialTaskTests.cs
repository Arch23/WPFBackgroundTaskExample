using BackgroundTaskWPF.Tasks;
using Busines;
using Busines.Interfaces;
using FakeItEasy;

namespace ViewTests
{
    [TestClass]
    public class FactorialTaskTests
    {
        private IFactorialTask? factorialTask;
        private IFactorial? factorialMock;

        [TestInitialize]
        public void SetUp()
        {
            factorialMock = A.Fake<IFactorial>();
            factorialTask = new FactorialTask(factorialMock);
        }

        [TestMethod]
        public void TestCalculate()
        {
            var integerList = new int[]
            {
                1, 5
            };

            var resultList = new int[]
            {
                1, 120
            };

            IProgress<int> progressHandler
                = new Progress<int>((val) => Assert.IsTrue(val >= 0 && val <= 100));
        
            CancellationTokenSource tokenSource
                = new();

            A.CallTo(()
                => factorialMock.Calculate(integerList[0]))
                .Returns(resultList[0]);

            A.CallTo(()
                => factorialMock.Calculate(integerList[1]))
                .Returns(resultList[1]);

            var actual = factorialTask.Calculate(integerList, progressHandler, tokenSource.Token);

            Assert.AreEqual(resultList[0], actual.GetValueOrDefault(integerList[0]));
            Assert.AreEqual(resultList[1], actual.GetValueOrDefault(integerList[1]));
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public void TestCancellation()
        {
            var integerList = new int[] { 1 };

            var resultList = new int[] { -1 };

            IProgress<int> progressHandler
                    = new Progress<int>((val) => Assert.IsTrue(val >= 0 && val <= 100));

            CancellationTokenSource tokenSource
                = new();

            tokenSource.Cancel();

            var actual = factorialTask.Calculate(integerList, progressHandler, tokenSource.Token);

            Assert.AreEqual(resultList[0], actual.GetValueOrDefault(integerList[0]));
        }

        [TestMethod]
        public void TestArgumentException()
        {
            var integerList = new int[] { -1 };

            var resultList = new int[] { -1 };

            IProgress<int> progressHandler
                    = new Progress<int>((val) => Assert.IsTrue(val >= 0 && val <= 100));

            CancellationTokenSource tokenSource
                = new();

            A.CallTo(()
                => factorialMock.Calculate(integerList[0]))
                .Throws(new ArgumentException());

            var actual = factorialTask.Calculate(integerList, progressHandler, tokenSource.Token);

            Assert.AreEqual(resultList[0], actual.GetValueOrDefault(integerList[0]));
        }

        [TestMethod]
        public void TestArithmeticException()
        {
            var integerList = new int[] { 13 };

            var resultList = new int[] { -1 };

            IProgress<int> progressHandler
                    = new Progress<int>((val) => Assert.IsTrue(val >= 0 && val <= 100));

            CancellationTokenSource tokenSource
                = new();

            A.CallTo(()
                => factorialMock.Calculate(integerList[0]))
                .Throws(new ArithmeticException());

            var actual = factorialTask.Calculate(integerList, progressHandler, tokenSource.Token);

            Assert.AreEqual(resultList[0], actual.GetValueOrDefault(integerList[0]));
        }
    }
}