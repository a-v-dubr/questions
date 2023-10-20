using BusinessLogic;
using Domain;
using Infrastructure;

namespace Presentation
{
    /// <summary>
    /// Initializes a new instance of DataHandler class
    /// </summary>
    public class DataHandler
    {
        public readonly QuestionRepository Repo = new();
        public readonly List<Category> Categories = new();

        public QuestionDTO? QuestionDTO { get; private set; }
        public Question? SelectedQuestion { get; private set; }
        public Category? SelectedCategory { get; private set; }

        private QuestionRepetitionsHandler? _handler;
        private QuestionsEditor? _editor;

        public DataHandler()
        {
            Repo.RetrieveQuestionsFromDb();
            Categories = Repo.Categories;
        }

        /// <summary>
        /// Returns categories which contain any available questions
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public List<Category> GetAvailableCategories()
        {
            var result = new List<Category>();
            Repo.RetrieveQuestionsFromDb();
            var availableQuestions = Repo.GetAvailableQuestions();

            if (availableQuestions.Any())
            {
                foreach (var c in Categories)
                {
                    if (c is not null && availableQuestions.Any(q => q.CategoryId == c.Id))
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
        public void EditSelectedQuestion(Question question)
        {
            if (SelectedQuestion is not null && question is not null && Repo.Contains(question))
            {
                _editor = new QuestionsEditor(Repo, SelectedQuestion);
                _editor.UpdateQuestionProperties(question);
            }
        }

        /// <summary>
        /// Sets null value for the selected category
        /// </summary>
        public void ResetSelectedCategoryToDefault()
        {
            SelectedCategory = default;
        }

        public void ResetSelectedQuestionToDefault()
        {
            SelectedQuestion = default;
        }

        public void ResetQuestionDTOToDefault()
        {
            QuestionDTO = default;
        }

        public void SetNewSelectedCategory(string title)
        {
            SelectedCategory = new(title);
        }

        public void SetNewSelectedCategory(Category category)
        {
            if (category is not null)
            {
                SelectedCategory = category;
            }
        }

        public void SetQuestionDTO(QuestionDTO dto)
        {
            if (dto is not null)
            {
                QuestionDTO = dto;
            }
        }

        public void SetSelectedQuestion(Question question)
        {
            if (Repo.Contains(question))
            {
                SelectedQuestion = question;
            }
        }

        /// <summary>
        /// Sets the selected category as a category for the selected question
        /// </summary>
        public void EditSelectedQuestionCategory()
        {
            if (SelectedQuestion is not null && SelectedCategory is not null)
            {
                _editor = new QuestionsEditor(Repo, SelectedQuestion);
                _editor.UpdateCategory(SelectedCategory);
            }
        }

        /// <summary>
        /// Adds new question to the repository and database
        /// </summary>
        public void AddNewQuestion(Question question)
        {
            if (question is not null)
            {
                Repo.AddQuestionToDb(question);
            }
        }

        /// <summary>
        /// Returns true if the question exists and the chosen answer is correct, otherwise, false
        /// </summary>
        /// <returns></returns>
        public bool IsAnswerCorrect(RadioButton? _answerInput)
        {
            if (SelectedQuestion is not null && _answerInput is not null)
            {
                return SelectedQuestion.Answers.FirstOrDefault(a => a.Text == _answerInput.Text && a.IsCorrect) is not null;
            }
            return default;
        }

        /// <summary>
        ///  Reduces or resets next time for displaying question according to correct or incorrect user answer
        /// </summary>
        public void CheckAnswer(bool isAnswerCorrect)
        {
            if (SelectedQuestion is not null)
            {
                _handler = new(SelectedQuestion, Repo);
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
