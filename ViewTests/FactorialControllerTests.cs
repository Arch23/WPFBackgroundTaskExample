using BackgroundTaskWPF.Controller;
using BackgroundTaskWPF.Tasks;
using BackgroundTaskWPF.ViewModels;
using FakeItEasy;

namespace ViewTests
{
    [TestClass]
    public class FactorialControllerTests
    {
        private IFactorialTask? factorialTaskMock;
        private FactorialController? controller;
        private FactorialViewModel? viewModel;

        [TestInitialize]
        public void SetUp()
        {
            viewModel = new FactorialViewModel();
            factorialTaskMock = A.Fake<IFactorialTask>();
            controller = new FactorialController(factorialTaskMock);
        }

        [TestMethod]
        public void TestStart()
        {
            var expectedDict = new Dictionary<int, int>
            {
                { 1, 1 },
                { 3, 6 },
            };

            viewModel.InputTxt = "1;3";

            A.CallTo(() => factorialTaskMock.Calculate(A<IEnumerable<int>>._, A<IProgress<int>>._, A<CancellationToken>._))
                .Returns(expectedDict);

            controller.Start(viewModel, (res) 
                => Assert.IsTrue(expectedDict.Select(keyValue => $"input: {keyValue.Key}\toutput: {keyValue.Value}{Environment.NewLine}").Contains(res)));
        }

        [TestMethod]
        public void TestStartWithCancellation()
        {
            A.CallTo(() => factorialTaskMock.Calculate(A<IEnumerable<int>>._, A<IProgress<int>>._, A<CancellationToken>._))
                .Throws(new OperationCanceledException());

            viewModel.InputTxt = "1";

            controller.Start(viewModel, (res)
                => Assert.AreEqual("Calculation cancelled!", res));
        }
    }
}
