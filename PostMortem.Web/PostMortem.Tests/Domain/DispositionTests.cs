namespace PostMortem.Tests.Domain
{
    using Moq;
    using PostMortem.Domain.Comments;
    using PostMortem.Domain.Voters;
    using Xunit;
    using Zatoichi.Common.UnitTest;

    public class DispositionTests : BaseTest
    {

        [Fact]
        public void Disposition_Is_Implicitly_Cast_To_Boolean()
        {
            var voterId = new Mock<IVoterId>();
            var like = new Like(voterId.Object);
            Assert.True(like);
        }
    }
}