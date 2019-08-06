namespace PostMortem.Tests.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using MediatR;
    using Moq;
    using Newtonsoft.Json;
    using PostMortem.Domain.Comments;
    using PostMortem.Domain.Comments.Commands;
    using PostMortem.Domain.Questions;
    using PostMortem.Domain.Voters;
    using Xunit;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.Common.UnitTest;
    using Zatoichi.EventSourcing;

    public class QuestionTests : BaseTest
    {
        private readonly QuestionOptions options;
        private readonly Mock<IMediator> mediator;
        private readonly Question question;

        public QuestionTests()
        {
            this.options = this.fixture.Create<QuestionOptions>();
            this.question = new Question
            {
                Options = this.options
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
            var voterId = new Mock<IVoterId>();
            var likeEvents = new List<LikeCommentCommand>();
            int i = 0;
            for (; i < 30 && i < this.options.MaximumLikesPerCommentPerVoter; i++)
            {
                var comment = new Comment(this.question);
                likeEvents.Add(new LikeCommentCommand(comment, voterId.Object));
            }

            this.question.AddEvents(likeEvents.Map(e => (Event)e).ToList());
            Assert.Equal(i, this.question.Comments.Count);
        }
    }
}