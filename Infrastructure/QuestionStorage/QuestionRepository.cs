using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Infrastructure
{
    /// <summary>
    /// Initializes a new instance of QuestionRepository class
    /// </summary>
    public class QuestionRepository : IEnumerable<Question>
    {
        public const string DbName = "QuestionsDatabase.db";

        public const string DefautCategoryTitle = "Вопросы без категории";
        public const int DefautCategoryId = 1;
        public static Category? DefaultCategory { get; private set; }

        public readonly List<Category> Categories = new();
        private readonly List<Question> _questions = new();

        /// <summary>
        /// Adds question to the repository
        /// </summary>
        /// <param name="question"></param>
        private void AddQuestionToRepository(Question question)
        {
            if (!_questions.Any(q => q.Id == question.Id) && question.Answers.Any(a => a.IsCorrect))
            {
                _questions.Add(question);
                if (!Categories.Any(c => c.Id == question.QuestionCategory.Id))
                {
                    Categories.Add(question.QuestionCategory);
                }
            }
        }

        /// <summary>
        /// Adds question's properties to the database
        /// </summary>
        /// <param name="question"></param>
        public void AddQuestionToDb(Question question)
        {
            AddQuestionToRepository(question);

            if (_questions.Contains(question))
            {
                using var context = new QuestionDbContext();
                context.Database.Migrate();
                context.Database.EnsureCreated();

                context.Categories.Include(c => c.Questions).ThenInclude(c => c.Answers);

                if (context.Categories.Local.SingleOrDefault(c => c.Id == question.QuestionCategory.Id) is null)
                {
                    context.Categories.Attach(question.QuestionCategory);
                }

                context.Questions.Add(question);
                context.Answers.AddRange(question.Answers.Where(a => a.Enabled));
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves questions from the database and rewrites question repository
        /// </summary>
        public void RetrieveQuestionsFromDb()
        {
            using var context = new QuestionDbContext();
            context.Database.Migrate();

            var categories = context.Categories.Include(c => c.Questions).ThenInclude(c => c.Answers).ToList();
            TrySetDefaultCategory(categories);

            Categories.Clear();
            _questions.Clear();

            foreach (var c in categories)
            {
                if (c.Enabled)
                {
                    Categories.Add(c);
                }
                foreach (var q in c.Questions)
                {
                    if (q.Enabled)
                    {
                        _questions.Add(q);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a question instance by Id value or null if an instance isn't found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question? GetQuestionById(int id)
        {
            return _questions.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Returns questions which are available for user
        /// </summary>
        /// <returns></returns>
        public List<Question> GetAvailableQuestions()
        {
            return _questions.Where(q => q.AvailableAt <= DateTime.Now).ToList();
        }

        /// <summary>
        /// Updates all question's properties according to new data
        /// </summary>
        /// <param name="question"></param>
        public void UpdateQuestionInDb(Question question)
        {
            if (_questions.Contains(question))
            {
                using var context = new QuestionDbContext();
                context.Database.Migrate();

                int id = question.Id;
                var updatingQuestion = context.Questions.Find(id);

                if (updatingQuestion is not null)
                {
                    updatingQuestion.ChangeCategory(question.QuestionCategory);
                    updatingQuestion.UpdateRepetitionProperties(question);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates question's and answers' texts in DB according to new data
        /// </summary>
        /// <param name="question"></param>
        public void UpdateQuestionRepetitionsInDb(Question question)
        {
            if (_questions.Contains(question))
            {
                using var context = new QuestionDbContext();
                context.Database.Migrate();
                int id = question.Id;

                var updatingQuestion = context.Questions.Find(id);
                if (updatingQuestion is not null)
                {
                    updatingQuestion.UpdateRepetitionProperties(question);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Disables data of question index in the database
        /// </summary>
        /// <param name="question"></param>
        public void DisableQuestionInDb(Question question)
        {
            if (question is not null && _questions.Contains(question))
            {
                question.SetDateTimeWhenQuestionIsAvailable(default);
                question.SetRepetitions(Question.Repetitions.Disable);
            }
        }

        /// <summary>
        /// Returns question by selected repository index or null if an instance isn't found
        /// </summary>
        /// <param name="repositoryIndex"></param>
        /// <returns></returns>
        public Question? GetQuestionByRepositoryIndex(int repositoryIndex)
        {
            if (repositoryIndex < _questions.Count)
            {
                return _questions[repositoryIndex];
            }
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var q in _questions)
            {
                yield return q;
            }
        }

        IEnumerator<Question> IEnumerable<Question>.GetEnumerator()
        {
            foreach (var q in _questions)
            {
                yield return q;
            }
        }

        private static void TrySetDefaultCategory(List<Category> categories)
        {
            if (DefaultCategory is null)
            {
                DefaultCategory = categories.SingleOrDefault(c => c.Id == DefautCategoryId);
                DefaultCategory ??= new(DefautCategoryTitle);
            }
        }
    }
}