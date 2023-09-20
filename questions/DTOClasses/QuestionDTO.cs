using BusinessLogic;
using Domain;

namespace Presentation.DTOClasses
{
    /// <summary>
    /// Initializes a new instance of QuestionDTO class
    /// </summary>
    internal class QuestionDTO
    {        
        public string? QuestionText { get; set; }
        public DateTime AvailableAt { get; set; }
        public List<Answer> Answers { get; set; } = new();
        public int CorrectAnswerIndex { get; set; }
        public List<string> AnswersTexts { get; set; } = new();
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
    }
}