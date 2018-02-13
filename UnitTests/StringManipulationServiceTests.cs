using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace UnitTests
{
    [TestClass]
    public class StringManipulationServiceTests
    {
        private readonly StringManipulationService _repository;       

        public StringManipulationServiceTests()
        {
            _repository = new StringManipulationService();
        }

        [TestMethod]
        public void UppercaseTest()
        {
            var expected = "HELLO WORLD";
            var result = _repository.UppercaseAllLettersInString("Hello world");
            
            Assert.IsTrue(expected == result);
        }

        [TestMethod]
        public void ReverseTest()
        {
            var expected = "dlrow olleh";
            var result = _repository.ReverseString("hello world");

            Assert.IsTrue(expected == result);
        }
    }
}
