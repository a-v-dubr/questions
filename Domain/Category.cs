using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Validators.PropertiesValidator;

namespace Domain
{
    /// <summary>
    /// Initializes a new instance of Category class
    /// </summary>
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; private set; }
        public List<Question> Questions { get; private set; } = new();
        public bool Enabled { get; private set; }

#pragma warning disable
        public Category() { }
#pragma warning restore

        public Category(string title)
        {
            ValidateNotNullOrWhiteSpaceText(title);
            Title = title;
            Enabled = true;
        }

        /// <summary>
        /// Sets Enabled property to false
        /// </summary>
        public void Disable()
        {
            Enabled = false;
        }

        /// <summary>
        /// Sets new category title
        /// </summary>
        /// <param name="newTItle"></param>
        public void ChangeCategoryTitle(string newTItle)
        {
            ValidateNotNullOrWhiteSpaceText(newTItle);
            Title = newTItle;
        }

        /// <summary>
        /// Sets new questions for the category
        /// </summary>
        /// <param name="newQuestions"></param>
        public void UpdateCategoryQuestions(List<Question> newQuestions)
        {
            if (newQuestions is not null)
            {
                Questions = newQuestions;
            }
        }

        /// <summary>
        /// Adds unique question to the questions' list
        /// </summary>
        /// <param name="question"></param>
        public void TryAddQuestion(Question question)
        {
            if (question is not null && question.QuestionCategory.Id == Id && !Questions.Contains(question))
            {
                Questions.Add(question);
            }
        }

        /// <summary>
        /// Removes the question from category
        /// </summary>
        /// <param name="question"></param>
        public void RemoveQuestion(Question question)
        {
            if (question is not null && Questions.Contains(question))
            {
                Questions.Remove(question);
            }
        }
    }
}










