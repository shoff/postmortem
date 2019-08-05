namespace PostMortem.Tests.Domain
{
    using System;
    using System.Collections.Generic;
    using AutoFixture;
    using MediatR;
    using Moq;
    using PostMortem.Domain.Comments;
    using PostMortem.Domain.Comments.Commands;
    using PostMortem.Domain.Questions;
    using PostMortem.Domain.Voters;
    using Xunit;
    using Zatoichi.Common.UnitTest;

    public class CommentTests : BaseTest
    {
        private readonly QuestionOptions options;
        private readonly Comment comment;
        private readonly Question question;
        private readonly Mock<IMediator> mediator;

        public CommentTests()
        {
            this.options = this.fixture.Create<QuestionOptions>();
            this.mediator = new Mock<IMediator>();
            this.question = new Question(this.mediator.Object)
            {
                Options = this.options
            };
            this.comment = new Comment(this.question);
        }

        [Fact]
        public void Vote_Like_Increases_Like_Count()
        {
            var voterId = new Mock<IVoterId>();
            voterId.SetupGet(i => i.Id).Returns(Guid.NewGuid().ToString());

            var like = new Like(voterId.Object);
            this.comment.Vote(like);
            Assert.Equal(1, this.comment.Likes);
        }

        [Fact]
        public void Voter_May_Not_Exceed_Allowed_Likes_For_Any_Given_Comment()
        {

            var voterId = new Mock<IVoterId>();
            var id = Guid.NewGuid().ToString();
            voterId.SetupGet(i => i.Id).Returns(id);

            // now try to like up to times and demonstrate that it only allows a configured number
            for (int i = 0; i < this.options.MaximumLikesPerCommentPerVoter; i++)
            {
                var like = new Like(voterId.Object);
                this.comment.Vote(like);
            }
            Assert.Equal(this.options.MaximumLikesPerCommentPerVoter, this.comment.Likes);
        }
        
        [Fact]
        public void Voter_May_Not_Exceed_Allowed_Dislikes_For_Any_Given_Comment()
        {
            var voterId = new Mock<IVoterId>();
            var id = Guid.NewGuid().ToString();
            voterId.SetupGet(i => i.Id).Returns(id);

            // now try to like up to times and demonstrate that it only allows a configured number
            for (int i = 0; i < this.options.MaximumDisLikesPerCommentPerVoter; i++)
            {
                var dislike = new DisLike(voterId.Object);
                this.comment.Vote(dislike);
            }
            Assert.Equal(this.options.MaximumDisLikesPerCommentPerVoter, this.comment.Dislikes);
        }

    }
}