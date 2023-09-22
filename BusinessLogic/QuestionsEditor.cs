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
        /// Updates question's text and writes new data to DB
        /// </summary>
        /// <param name="newText"></param>
        public void UpdateQuestionTextAndSaveToDb(string newText)
        {
            _question.UpdateQuestionText(newText);
            _repo.UpdateTextsValuesInDb(_question);
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
