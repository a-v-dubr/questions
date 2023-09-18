using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Validators.PropertiesValidator;

namespace Domain
{
    /// <summary>
    /// Initializes a new instanse of Question class
    /// </summary>
    public class Question
    {
        public const int AnswersCount = 4;
        public enum Repetitions
        {
            Disable,
            DisplayInYear,
            DisplayInMonth,
            DisplayInWeek,
            DisplayInDay,
            EnableAsCreated
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public string Text { get; private set; }
        [NotMapped]
        public Repetitions RepeateInPeriod { get; private set; } = Repetitions.EnableAsCreated;
        public DateTime AvailableAt { get; private set; }
        public List<Answer> Answers { get; private set; } = new();
        public int CategoryId { get; private set; }
        public Category QuestionCategory { get; set; }

#pragma warning disable
        private Question() { }

        private Question(Category category, string questionText)
        {
            ValidateNotNullOrWhiteSpaceText(questionText);
            QuestionCategory = category;
            Text = questionText;
            AvailableAt = DateTime.Now;
        }
#pragma warning restore

        public Question(Category category, string questionText, List<Answer> answers, DateTime availableAt)
        {
            ValidateAnswers(answers);
            ValidateNotNullOrWhiteSpaceText(questionText);
            QuestionCategory = category;
            Text = questionText;
            Answers = answers;
            AvailableAt = availableAt;
        }

        /// <summary>
        /// Tries to set answers' options for the question
        /// </summary>
        /// <param name="answers"></param>
        public void AddAnswers(List<Answer> answers)
        {
            ValidateAnswers(answers);
            if (Answers.Count == 0)
            {
                Answers.AddRange(answers);
            }
        }

        /// <summary>
        /// Sets new DateTime when question is available
        /// </summary>
        /// <param name="dateTime"></param>
        public void SetDateTimeWhenQuestionIsAvailable(DateTime dateTime)
        {
            AvailableAt = dateTime;
        }

        /// <summary>
        /// Sets new repetitions count for the question
        /// </summary>
        /// <param name="repetitions"></param>
        public void SetRepetitions(Repetitions repetitions)
        {
            RepeateInPeriod = repetitions;
        }

        /// <summary>
        /// Reduces count of repetitions of the question
        /// </summary>
        public void ReduceRepetitions()
        {
            if ((int)RepeateInPeriod >= (int)Repetitions.DisplayInYear)
            {
                RepeateInPeriod--;
            }
        }

        /// <summary>
        /// Sets count of repetitions of the question to maximum
        /// </summary>
        public void ResetRepetitions()
        {
            RepeateInPeriod = Repetitions.DisplayInDay;
        }

        /// <summary>
        /// Updates the instance repetitions properties according to parameter's properties
        /// </summary>
        /// <param name="question"></param>
        public void UpdateRepetitionProperties(Question question)
        {
            if (question is not null && question.Answers.Count == AnswersCount)
            {
                AvailableAt = question.AvailableAt;
                RepeateInPeriod = question.RepeateInPeriod;
            }
        }

        /// <summary>
        /// Creates question which is available at DateTime.Now
        /// </summary>
        /// <param name="category"></param>
        /// <param name="questionText"></param>
        /// <returns></returns>
        public static Question CreateQuestion(Category category, string questionText)
        {
            return new(category, questionText);
        }
    }

}