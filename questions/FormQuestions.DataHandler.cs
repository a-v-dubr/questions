using BusinessLogic;
using Domain;

namespace Presentation
{
    public partial class FormQuestions
    {
        /// <summary>
        /// Returns categories which contain any available questions
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        private List<Category> GetAvailableCategories()
        {
            var result = new List<Category>();
            var availableQustions = _repo.GetAvailableQuestions();

            if (availableQustions.Any())
            {
                foreach (var c in _categories)
                {
                    if (c is not null && availableQustions.Any(q => q.CategoryId == c.Id))
                    {
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Updates selected question's properties according to parameter's properties
        /// </summary>
        /// <param name="question"></param>
        private void EditSelectedQuestion(Question question)
        {
            if (_selectedQuestion is not null && question is not null)
            {
                _editor = new QuestionsEditor(_repo, _selectedQuestion);
                _editor.UpdateQuestionProperties(question);
            }
        }

        /// <summary>
        /// Adds new question to the repository and database
        /// </summary>
        private void AddNewQuestion(Question question)
        {
            if (question is not null)
            {
                _repo.AddQuestionToRepository(question);
                _repo.AddQuestionToDb(question);
            }
        }

        /// <summary>
        /// Returns true if the question exists and the chosen answer is correct, otherwise, false
        /// </summary>
        /// <returns></returns>
        private bool IsAnswerCorrect()
        {
            if (_selectedQuestion is not null && _answerInput is not null)
            {
                return _selectedQuestion.Answers.FirstOrDefault(a => a.Text == _answerInput.Text && a.IsCorrect) is not null;
            }
            return default;
        }

        /// <summary>
        ///  Reduces or resets next time for displaying question according to correct or incorrect user answer
        /// </summary>
        private void CheckAnswer(bool isAnswerCorrect)
        {
            if (_selectedQuestion is not null && _answerInput is not null)
            {
                _handler = new(_selectedQuestion, _repo);
                if (isAnswerCorrect)
                {
                    _handler.ReduceRepetitions();
                }
                else
                {
                    _handler.ResetRepetitions();
                }
            }
        }
    }
}
