using Domain;
using Infrastructure;
using static BusinessLogic.Validator;

namespace BusinessLogic
{
    /// <summary>
    /// Initializes a new instance of QuestionEditor class
    /// </summary>
    public class QuestionsEditor
    {
        private readonly QuestionRepository _repo;
        private readonly Question _question;

        public QuestionsEditor(QuestionRepository repo, Question question)
        {
            ValidateNotNullInstances(repo, question);
            ValidateIfRepositoryContainsQuestion(repo, question);
            _repo = repo;
            _question = question;
        }

        /// <summary>
        /// Updates all of the question's properties according to new data and saves object to DB
        /// </summary>
        /// <param name="question"></param>
        public void UpdateQuestionProperties(Question question)
        {
            if (question is not null)
            {
                _question.UpdateQuestionText(question.Text);
                _question.ChangeCategory(question.QuestionCategory);
                _question.ChangeAnswers(question.Answers);
                _question.UpdateRepetitionProperties(question);

                _repo.UpdateQuestionInDb(_question);
            }
        }

        public void UpdateCategory(Category category)
        {
            if (category is not null && _question.CategoryId != category.Id && category.Enabled)
            {
                _question.ChangeCategory(category);
                _repo.UpdateQuestionInDb(_question);
            }
        }

        /// <summary>
        /// Updates answer's text without writing changes to DB
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="newText"></param>
        public void UpdateAnswerText(int answerId, string newText)
        {
            if (_question.Answers.Any(a => a.Id == answerId))
            {
                _question.Answers.Single(a => a.Id == answerId).UpdateAnswerText(newText);
            }
        }

        /// <summary>
        /// Sets question's answer defined by index as correct
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newCorrectIndex"></param>
        public void UpdateCorrectAnswer(int index)
        {
            if (_question.Answers.Count > index)
            {
                for (int i = 0; i < _question.Answers.Count; i++)
                {
                    if (i != index)
                    {
                        _question.Answers[i].SetIncorrectAnswer();
                    }
                }
                _question.Answers[index].SetCorrectAnswer();
            }
        }
    }
}
