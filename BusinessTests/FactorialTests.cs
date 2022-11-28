using Busines;

namespace BusinessTests
{
    [TestClass]
    public class FactorialTests

    {
        private Factorial factorial = new Factorial();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNegative() =>
            factorial.Calculate(-1);
        

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestResultOverflow() =>        
            factorial.Calculate(13);

        [DataTestMethod]
        [DataRow(0,1)]
        [DataRow(1,1)]
        [DataRow(2,2)]
        [DataRow(3,6)]
        [DataRow(4,24)]
        [DataRow(5,120)]
        public void TestFactorialResult(int val, int expected)
        {
            Assert.AreEqual(factorial.Calculate(val), expected);
        }
    }
}