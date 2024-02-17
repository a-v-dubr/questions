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

        internal static bool AnswerTextsAreUnique(List<TextBox> textsboxes)
        {
            var texts = new List<string>();
            foreach (var tb in textsboxes)
            {
                texts.Add(tb.Text);
            }

            return texts.Count == texts.Distinct().ToList().Count;
        }

        internal static bool UserInputIsValid(params string[] input)
        {
            if (!input.Any(string.IsNullOrWhiteSpace))
            {
                return true;
            }
            return false;
        }
    }
}
