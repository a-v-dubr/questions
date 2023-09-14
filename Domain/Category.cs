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

#pragma warning disable
        public Category() { }
#pragma warning restore

        public Category(string title)
        {
            ValidateNotNullOrWhiteSpaceText(title);
            Title = title;
        }
    }
}
