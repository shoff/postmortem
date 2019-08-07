namespace PostMortem.Tests.Domain
{
    using System.Collections.Generic;
    using AutoFixture;
    using Newtonsoft.Json;
    using PostMortem.Domain.Comments.Events;
    using PostMortem.Domain.Questions;
    using PostMortem.Domain.Voters;
    using Xunit;
    using Zatoichi.Common.UnitTest;
    using Zatoichi.EventSourcing;
    using Comment = PostMortem.Domain.Comments.Comment;
    using Question = PostMortem.Domain.Questions.Question;

    public class QuestionTests : BaseTest
    {
        private readonly QuestionOptions options;
        private readonly Question question;

        public QuestionTests()
        {
            this.options = this.fixture.Create<QuestionOptions>();
            this.question = new Question
            {
            };
        }

        [Fact]
        public void Questions_Are_Serializable()
        {
            this.question.AddQuestionText("Woohoo!");
            var json = JsonConvert.SerializeObject(this.question);
            var deserializedQuestion = JsonConvert.DeserializeObject<Question>(json);
            Assert.Equal(this.question.QuestionText, deserializedQuestion.QuestionText);
        }

        [Fact]
        public void Apply_Sets_The_Correct_Like_State()
        {
            var voterId = new VoterId("user");
            var likeEvents = new List<DomainEvent>();
            int i = 0;
            for (; i < 30 && i < this.options.MaximumLikesPerCommentPerVoter; i++)
            {
                var comment =  new Comment(
                    "comment text",
                    "me",
                    this.question.QuestionId);
                likeEvents.Add(new CommentLiked(comment.CommentId.Id, voterId.Id));
            }

            this.question.AddEvents(likeEvents);
            Assert.Equal(i, this.question.Comments.Count);
        }
    }
}