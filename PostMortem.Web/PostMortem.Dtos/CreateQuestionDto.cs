namespace PostMortem.Dtos
{
    using System;

    public class CreateQuestionDto
    {
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
    }
}