using Domain;
using Infrastructure;

namespace BusinessLogic
{
    /// <summary>
    /// Contains methods for validation the data in the business logic classes
    /// </summary>
    internal static class Validator
    {
        public static void ValidateNotNullInstances(params object[] objects)
        {
            foreach (var o in objects)
            {
                if (o is null)
                {
                    throw new ArgumentException($"The instance cannot be null");
                }
            }
        }

        public static void ValidateIfRepositoryContainsQuestion(QuestionRepository repo, Question question)
        {
            if (repo.GetQuestionById(question.Id) is null)
            {
                throw new ArgumentException($"Cannot create object because {nameof(repo)} instance doesn't contain {nameof(question)} instance");
            }
        }
    }
}
