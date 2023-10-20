namespace Presentation.Helper
{
    /// <summary>
    /// Contains string constants and patterns for naming and placeholder texts of the form controls
    /// </summary>
    internal static class ControlMessages
    {
        public static class ButtonTexts
        {
            public const string CheckAnswer = "Проверить ответ";
            public const string AddNewQuestion = "Добавить новый вопрос";
            public const string ReturnToMenu = "Вернуться в меню";
            public const string ChangeCategory = "Переместить вопрос в другую категорию";
            public const string AnswerAllQuestionsFromCategory = "Вопросы этой категории";
            public const string AnswerNextQuestion = "Ответить на следующий вопрос";

            public const string ChooseNewCategory = "Выбрать категорию для вопроса";
            public const string ContinueCreatingQuestions = "Продолжить создание вопросов";
            public const string AcceptQuestionText = "Сохранить вопрос";
            public const string AcceptAnswerText = "Сохранить вариант ответа";
            public const string FinishAddingAnswers = "Завершить ввод ответов";
            public const string AcceptCorrectAnswerInput = "Сохранить правильный ответ";

            public const string AcceptAvailableCategory = "Выбрать эту категорию";
            public const string CreateNewCategory = "Создать новую категорию";
            public const string AcceptCategoryTitle = "Сохранить название категории";
        }

        public static class LabelTexts
        {
            public const string AvailableCategoriesAndQuestions = "Доступные вопросы:";
            public const string NoQuestionsAvailable = "Доступных вопросов нет. Вы можете добавить новый вопрос.";
            public readonly static string ChooseAnswer = "Укажите ответ на вопрос:\n{0}";
            public readonly static string NoMoreAvailableQuestionsInCategory = "\n\nВы ответили на все доступные вопросы в категории \"{0}\".";
            public const string CorrectAnswer = "Вы дали правильный ответ!";
            public const string WrongAnswer = "Вы дали неправильный ответ!";
            public readonly static string WhenQuestionIsAvailable = "\nВопрос будет доступен {0}.";

            public const string TypeQuestionText = "Введите текст вопроса:";
            public readonly static string DisplayQuestionWhileAddingAnswers = "Введите варианты ответа на вопрос:\n{0}";
            public readonly static string ChooseCorrectAnswer = "Укажите правильный ответ на вопрос:\n{0}";
            public static readonly string QuestionIsSavedAndAvailable = "Вопрос \"{0}\" сохранён и помещён в доступные вопросы.";

            public const string AvailableCategories = "Доступные категории:";
            public const string NoCategoriesAvailable = "Доступных категорий нет. Вы можете создать новую категорию.";
            public readonly static string ActualQuestionCategory = "Для вопроса \"{0}\" установлена категория \"{1}\".";
            public readonly static string TypeCategoryTitle = "Введите название новой категории для вопроса:\n{0}";
        }

        public static class ListBoxTexts
        {
            public readonly static string AvailableCategories = "{0} (доступно вопросов: {1})";
        }
    }
}
