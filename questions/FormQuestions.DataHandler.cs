using BusinessLogic;
using Domain;
using static Presentation.Helper.Validator;

namespace Presentation
{
    public partial class FormQuestions
    {
        /// <summary>
        /// Returns categories which contain any available questions
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        private List<Category> GetAvailableCategories(List<Question> questions)
        {
            var result = new List<Category>();
            if (questions.Any())
            {
                foreach (var c in _categories)
                {
                    if (c is not null && questions.Any(q => q.CategoryId == c.Id))
                    {
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Creates category with defined title if user title input is correct and adds it to available categories' list
        /// </summary>
        /// <param name="title"></param>
        private bool TryCreateCategory(string title)
        {
            if (UserInputIsValid(title))
            {
                _selectedCategory = new Category(title);
                _categories.Add(_selectedCategory);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates selected question's properties according to parameter's properties
        /// </summary>
        /// <param name="question"></param>
        private void EditSelectedQuestion(Question question)
        {
            if (_selectedQuestion is not null)
            {
                _editor = new QuestionsEditor(_repo, _selectedQuestion);
                for (int i = 0; i < _selectedQuestion.Answers.Count; i++)
                {
                    if (question.Answers[i].IsCorrect)
                    {
                        _editor.UpdateCorrectAnswer(i);
                    }
                    _editor.UpdateAnswerText(_selectedQuestion.Answers[i].Id, question.Answers[i].Text);
                }
                _editor.UpdateQuestionTextAndSaveToDb(question.Text);
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
    }
}
