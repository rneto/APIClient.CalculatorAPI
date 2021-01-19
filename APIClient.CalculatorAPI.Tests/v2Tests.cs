using APIClient.CalculatorAPI.v2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APIClient.CalculatorAPI.Tests
{
    [TestClass]
    public class v2Tests
    {
        private string MathJSURL = "https://api.mathjs.org/v4/";

        [TestMethod]
        public void Add_operation_success()
        {
            int a = 123;
            int b = 321;
            var client = new v2.Client(MathJSURL);
            var rs = client.Send(new Add(a, b));

            Assert.AreEqual(a + b, rs.Value);
        }

        [TestMethod]
        public void Divide_operation_success()
        {
            int a = 12345;
            int b = 321;
            var client = new v2.Client(MathJSURL);
            var rs = client.Send(new Divide(a, b));

            Assert.AreEqual(System.Math.Round((decimal)a / b, 0), rs.Value);
        }

        [TestMethod]
        public void Multiply_operation_success()
        {
            int a = 123;
            int b = 123;
            var client = new v2.Client(MathJSURL);
            var rs = client.Send(new Multiply(a, b));

            Assert.AreEqual(a * b, rs.Value);
        }

        [TestMethod]
        public void Subtract_operation_success()
        {
            int a = 54321;
            int b = 12345;
            var client = new v2.Client(MathJSURL);
            var rs = client.Send(new Subtract(a, b));

            Assert.AreEqual(a - b, rs.Value);
        }
    }
}
