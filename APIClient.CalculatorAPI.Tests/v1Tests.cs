using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APIClient.CalculatorAPI.Tests
{
    [TestClass]
    public class v1Tests
    {
        private string DNEOnlineURL = "http://www.dneonline.com/calculator.asmx";

        [TestMethod]
        public void Add_operation_success()
        {
            int a = 123;
            int b = 321;
            var client = new v1.Client(DNEOnlineURL);
            var rq = new v1.Add()
            {
                intA = a,
                intB = b
            };
            var rs = client.Send<v1.AddResponse>(rq);

            Assert.AreEqual(a + b, rs.AddResult);
        }

        [TestMethod]
        public void Divide_operation_success()
        {
            int a = 12345;
            int b = 321;
            var client = new v1.Client(DNEOnlineURL);
            var rq = new v1.Divide()
            {
                intA = a,
                intB = b
            };
            var rs = client.Send<v1.DivideResponse>(rq);

            Assert.AreEqual(a / b, rs.DivideResult);
        }

        [TestMethod]
        public void Multiply_operation_success()
        {
            int a = 123;
            int b = 123;
            var client = new v1.Client(DNEOnlineURL);
            var rq = new v1.Multiply()
            {
                intA = a,
                intB = b
            };
            var rs = client.Send<v1.MultiplyResponse>(rq);

            Assert.AreEqual(a * b, rs.MultiplyResult);
        }

        [TestMethod]
        public void Subtract_operation_success()
        {
            int a = 54321;
            int b = 12345;
            var client = new v1.Client(DNEOnlineURL);
            var rq = new v1.Subtract()
            {
                intA = a,
                intB = b
            };
            var rs = client.Send<v1.SubtractResponse>(rq);

            Assert.AreEqual(a - b, rs.SubtractResult);
        }
    }
}
