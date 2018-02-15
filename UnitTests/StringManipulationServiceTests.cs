using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace UnitTests
{
    [TestClass]
    public class StringManipulationServiceTests
    {
        private readonly IStringManipulationService _service;       

        public StringManipulationServiceTests()
        {
            _service = new StringManipulationService();
        }

        [TestMethod]
        public void UppercaseTest()
        {
            var expected = "HELLO WORLD";
            var result = _service.UppercaseAllLettersInString("Hello world");
            
            Assert.IsTrue(expected == result);
        }

        [TestMethod]
        public void ReverseTest()
        {
            var expected = "dlrow olleh";
            var result = _service.ReverseString("hello world");

            Assert.IsTrue(expected == result);
        }
    }
}
