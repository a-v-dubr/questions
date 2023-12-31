﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Validators.PropertiesValidator;

namespace Domain
{
    /// <summary>
    /// Initializes a new instance of Answer class
    /// </summary>
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public string Text { get; private set; }
        public bool IsCorrect { get; private set; }
        public int QuestionId { get; private set; }
        public Question ActualQuestion { get; private set; }
        public bool Enabled { get; private set; }

#pragma warning disable
        private Answer() { }
#pragma warning restore

        public Answer(Question actualQuestion, string text, bool isCorrect = false)
        {
            ValidateNotNullOrWhiteSpaceText(text);
            ActualQuestion = actualQuestion;
            Text = text;
            IsCorrect = isCorrect;
            Enabled = true;
        }

        /// <summary>
        /// Sets actual answer as correct
        /// </summary>
        public void SetCorrectAnswer()
        {
            if (!ActualQuestion.Answers.Any(a => a.IsCorrect))
            {
                IsCorrect = true;
            }
        }

        /// <summary>
        /// Sets actual answer as incorrect
        /// </summary>
        public void SetIncorrectAnswer()
        {
            IsCorrect = false;
        }

        /// <summary>
        /// Sets new value for answer text property
        /// </summary>
        /// <param name="text"></param>
        public void UpdateAnswerText(string text)
        {
            ValidateNotNullOrWhiteSpaceText(text);
            Text = text;
        }

        /// <summary>
        /// Sets Enabled property to false
        /// </summary>
        public void Disable()
        {
            Enabled = false;
        }
    }
}
