using Domain;

namespace Presentation
{
    /// <summary>
    /// Initializes a new instance of QuestionDTO class
    /// </summary>
    public class QuestionDTO
    {
        public string? QuestionText { get; set; }
        public List<Answer> Answers { get; private set; } = new();
        public int CorrectAnswerIndex { get; set; }
        public List<string> AnswersTexts { get; private set; } = new();
        public Category? QuestionCategory { get; set; }

        public Question? MapDTO()
        {
            if (QuestionText is not null && AnswersTexts.Count >= Question.MinAnswersCount && QuestionCategory is not null)
            {
                var q = Question.CreateQuestion(QuestionCategory, QuestionText);

                for (int i = 0; i < AnswersTexts.Count; i++)
                {
                    if (i == CorrectAnswerIndex)
                    {
                        Answers.Add(new(q, AnswersTexts[i], true));
                    }
                    else
                    {
                        Answers.Add(new(q, AnswersTexts[i]));
                    }
                }
                q.AddAnswers(Answers);
                return q;
            }
            return null;
        }

        /// <summary>
        /// Saves an answer text and returns true if it is unique; otherwise, returns false
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TryAddAnswerText(string text)
        {
            if (!AnswersTexts.Contains(text))
            {
                AnswersTexts.Add(text);
                return true;
            }
            return false;
        }
    }
}