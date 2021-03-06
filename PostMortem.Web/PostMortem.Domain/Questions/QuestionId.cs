﻿using System;
using System.Collections.Generic;
using System.Text;
using PostMortem.Infrastructure;

namespace PostMortem.Domain.Questions
{
    public class QuestionId : EntityId<Guid>
    {
        private const string Prefix = "Question-";

        public QuestionId(Guid id) : base(id)
        {
        }

        public QuestionId(string idString) : this(ExtractGuid(idString))
        {

        }

        private static Guid ExtractGuid(string idString) =>
            Guid.Parse(idString.StartsWith(Prefix) ? idString.Substring(Prefix.Length) : idString);

        public override string AsIdString() => $"{Prefix}{Id}";
        public static readonly QuestionId Empty = new QuestionId(Guid.Empty);
        public static QuestionId NewQuestionId() => new QuestionId(Guid.NewGuid());
        public static implicit operator QuestionId(Guid id) => new QuestionId(id);
        public static implicit operator QuestionId(string id) => new QuestionId(id);
    }
}


