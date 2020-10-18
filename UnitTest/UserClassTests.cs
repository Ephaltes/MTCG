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
            string token = "username-mtcgToken";
            
            var user = new UserModell(token);
            
            Assert.That(!string.IsNullOrWhiteSpace(user.Username));
        }
        
        [Test]
        public void LogIn_IsFalse()
        {
            string token = "username-mtcToken";

            Assert.That(() => new UserModell(token) , Throws.Exception);
        }
        [Test]
        public void CreateTokenForUser()
        {
            string username = "username";
            string suffix = "-mtcgToken";
            var token = UserModell.CreateTokenForUser(username, "password");

            bool result = token == username + suffix;
            
            Assert.That(result);
        }
        
    }
}
        