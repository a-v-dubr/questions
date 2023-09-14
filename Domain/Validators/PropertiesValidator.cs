namespace Domain.Validators
{
    internal static class PropertiesValidator
    {
        internal static void ValidateNotNullOrWhiteSpaceText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("The property cannot be null or white space");
            }
        }

        internal static void ValidateAnswers(List<Answer> answers)
        {
            if (answers.Count != Question.AnswersCount)
            {
                throw new ArgumentException($"There are questions with {Question.AnswersCount} answers only available");
            }

            if (answers.GroupBy(a => a.Text).Any(t => t.Count() > 1))
            {
                throw new ArgumentException($"Duplicates of answer texts are not allowed");
            }
        }
    }
}

