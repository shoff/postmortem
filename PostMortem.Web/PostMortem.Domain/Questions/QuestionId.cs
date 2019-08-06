﻿namespace PostMortem.Domain.Questions
{
    using System;
    using Newtonsoft.Json;

    public class QuestionId : IQuestionId
    {
        public QuestionId(Guid id)
        {
            this.Id = id;
        }
        [JsonProperty]
        public Guid Id { get; private set; }

    }
}