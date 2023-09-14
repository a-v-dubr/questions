using Domain;
using Infrastructure;

namespace BusinessLogic
{
    /// <summary>
    /// Initializes a new instance of QuestionRepetitionsHandler class
    /// </summary>
    public class QuestionRepetitionsHandler
    {
        private static readonly Dictionary<Question.Repetitions, DateTime> _repetitionsAndAvailabilityDateTime = new()
        {
            { Question.Repetitions.DisplayInDay, DateTime.Now.AddDays(1) },
            { Question.Repetitions.DisplayInWeek, DateTime.Now.AddDays(7) },
            { Question.Repetitions.DisplayInMonth, DateTime.Now.AddMonths(1) },
            { Question.Repetitions.DisplayInYear, DateTime.Now.AddYears(1) }
        };

        private readonly QuestionRepository _repo;
        private readonly Question _question;

        public QuestionRepetitionsHandler(Question question, QuestionRepository repo)
        {
            ValidateNotNullInstances(repo, question);
            ValidateIfRepositoryContainsQuestion(repo, question);
            _repo = repo;
            _question = question;
        }

        /// <summary>
        /// Reassigns properties of the question instance if the answer is correct
        /// </summary>
        public void ReduceRepetitions()
        {
            _question.ReduceRepetitions();
            _question.SetDateTimeWhenQuestionIsAvailable(_repetitionsAndAvailabilityDateTime[_question.RepeateInPeriod]);
            _repo.UpdateQuestionRepetitionsInDb(_question);
        }

        /// <summary>
        /// Reassigns properties of the question instance if the answer is wrong
        /// </summary>
        public void ResetRepetitions()
        {
            _question.ResetRepetitions();
            _question.SetDateTimeWhenQuestionIsAvailable(_repetitionsAndAvailabilityDateTime[_question.RepeateInPeriod]);
            _repo.UpdateQuestionRepetitionsInDb(_question);
        }

        /// <summary>
        /// Returns nearest DateTime to display question by repetitions count value
        /// </summary>
        /// <param name="repetitionsCount"></param>
        /// <returns></returns>
        public static DateTime WhenQuestionIsAvailable(Question.Repetitions repeateInPeriod)
        {
            if (repeateInPeriod != Question.Repetitions.Disable)
            {
                return _repetitionsAndAvailabilityDateTime[repeateInPeriod];
            }
            else
            {
                return default;
            }
        }

        private static void ValidateNotNullInstances(params object[] objects)
        {
            foreach (var o in objects)
            {
                if (o is null)
                {
                    throw new ArgumentException($"The instance cannot be null");
                }
            }
        }

        private static void ValidateIfRepositoryContainsQuestion(QuestionRepository repo, Question question)
        {
            if (!repo.Contains(question))
            {
                throw new ArgumentException($"Cannot create handler because {nameof(repo)} instance doesn't contain {nameof(question)} instance");
            }
        }

    }
}