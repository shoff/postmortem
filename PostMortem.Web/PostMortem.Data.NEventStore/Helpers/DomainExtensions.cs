using System;
using System.Collections.Generic;
using System.Text;
using PostMortem.Data.NEventStore.Config;

namespace PostMortem.Data.NEventStore.Helpers
{
    using DomainProject=Domain.Projects.Project;
    using DomainQuestion=Domain.Questions.Question;
    using DomainComment=Domain.Comments.Comment;

    public static class DomainExtensions
    {
        public static string GetBucketName(this DomainProject project) => Constants.PROJECT_BUCKET;
        public static string GetBucketName(this DomainQuestion question) => Constants.QUESTION_BUCKET;
        public static string GetBucketName(this DomainComment comment) => Constants.COMMENT_BUCKET;
        public static string GetStreamName(this DomainProject project) => $"project-{project.ProjectId}";
        public static string GetStreamName(this DomainQuestion question) => $"{question.ProjectId}-{question.QuestionId}";
        public static string GetStreamName(this DomainComment comment) => $"{comment.QuestionId}-{comment.CommentId}";
    }
}
