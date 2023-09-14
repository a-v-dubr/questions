namespace Presentation.Helper
{
    /// <summary>
    /// Contains methods for user input validation
    /// </summary>
    internal static class Validator
    {
        internal static bool AnswerTextsAreUnique(List<string> texts)
        {
            return texts.Count == texts.Distinct().ToList().Count;
        }

        internal static bool UserInputIsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
