using Moq;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class UserClassTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LogIn_IsTrue()
        {
            var token = "username-mtcgToken";
            var database = new Mock<IDatabase>();

            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(new UserEntity());

            var user = new UserModell(database.Object);
            var result = user.VerifyToken(token);

            Assert.That(result);
        }

        [Test]
        public void LogIn_IsFalse()
        {
            var token = "username-mtcToken";
            var database = new Mock<IDatabase>();

            var user = new UserModell(database.Object);

            var isValid = user.VerifyToken(token);

            Assert.That(!isValid);
        }

        [Test]
        public void CreateTokenForUser()
        {
            var username = "username";
            var suffix = "-mtcgToken";
            var database = new Mock<IDatabase>();
            database.Setup(x => x.CreateUser(It.IsAny<UserEntity>())).Returns(true);
            var user = new UserModell(database.Object);
            var token = user.CreateTokenForUser(username, "password");

            var result = token == username + suffix;

            Assert.That(result);
        }
    }
}