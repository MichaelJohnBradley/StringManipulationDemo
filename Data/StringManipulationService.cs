using System.Linq;

namespace Services
{
    public class StringManipulationService : IStringManipulationService
    {
        public string UppercaseAllLettersInString(string phrase)
        {
            return phrase.ToUpper();
        }

        public string ReverseString(string phrase)
        {
            return new string(Enumerable.Range(1, phrase.Length).Select(i => phrase[phrase.Length - i]).ToArray());
        }
    }
}
