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
        public int Id { get; private set; }
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
    }
}
