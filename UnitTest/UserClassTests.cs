using Moq;
using MTCG.Entity;
using MTCG.Helpers;
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
        public void VerifyToken_IsTrue()
        {
            var token = "username-mtcgToken";
            var database = new Mock<IDatabase>();

            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(new UserEntity());

            var user = new UserModell(database.Object);
            var result = user.VerifyToken(token);

            Assert.That(result);
        }

        [Test]
        public void VerifyToken_IsFalse()
        {
            var token = "username-mtcToken";
            var database = new Mock<IDatabase>();

            var user = new UserModell(database.Object);

            var isValid = user.VerifyToken(token);

            Assert.That(!isValid);
        }
        
        [Test]
        public void VerifyToken_IsFalse2()
        {
            var token = "username-mtcgToken";
            var database = new Mock<IDatabase>();
            UserEntity t = null;
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(t);
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
        
        [Test]
        public void CreateTokenForUser_UserExists()
        {
            var username = "username";
            var suffix = "-mtcgToken";
            var database = new Mock<IDatabase>();
            database.Setup(x => x.UserExists(It.IsAny<string>())).Returns(true);
            var user = new UserModell(database.Object);
            var token = user.CreateTokenForUser(username, "password");

            Assert.That(token == null);
        }

        [Test]
        public void GetUserByUsername()
        {
            UserEntity test = new UserEntity() {Username = "test"};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(test);

            var user = new UserModell(database.Object).GetUserByUsername("test");
            
            Assert.That(user.Username == test.Username);

        }
        
        [Test]
        public void VerifyLogin_true()
        {
            UserEntity test = new UserEntity() {Username = "test",Password = "1JkCZss1iMcyeACsRsfwY3ErmtJMBWTVtDCu0U+wvDaZZB1BDTrj6NbitpYQuUMQSzLnhFkz/e+fZRIjv8WaHvvJGmMcyUD06pGVRYeyHk0Jdb+cL6QKm8EzBhOaiSGKq7K8yVpCxPjkSI9/oVTs8WAN6vcgmG1RhttZEInLVwIQQNETCjOwEmiAPvI+0GSSgc3OiKZRczfzPjyJccBPrsM2uo5lSei76dep252r7y4YIthSzhZC8gDnuQDbKMZnqJ5q12kRW64bCEfvImo+PYKm/eYiORlsrBh0XImvC1qBIamQm65JeKfy2xXl8kdKeUccdrL81+A5HwajarYAUw=="
                ,Salt = "zIfkLYdQME2A72f8+fzUTnH04LK2HJC8NAYIAVdA3ma+Lnv7J5eRSsSVuWGW+//q+Z/Dn0ur229M9T6Lw2ynyl49fGeQnScin+plUVFkxot5WdHKF1X/ywYQ5w4a8P61ucK6NrKd8tSdPcWMgliQbx8WrHxWSBTFLNKgj3taOYE="};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(test);

            var model = new UserModell(database.Object);
            model.UserEntity = new UserEntity() {Username = "test", Password = "test"};
            var ret = model.VerifyLogin();
            
            
            Assert.That(ret);

        }
        
        [Test]
        public void VerifyLogin_false()
        {
            UserEntity test = new UserEntity() {Username = "test",Password = "1JkCZss1iMcyeACsRsfwY3ErmtJMBWTVtDCu0U+wvDaZZB1BDTrj6NbitpYQuUMQSzLnhFkz/e+fZRIjv8WaHvvJGmMcyUD06pGVRYeyHk0Jdb+cL6QKm8EzBhOaiSGKq7K8yVpCxPjkSI9/oVTs8WAN6vcgmG1RhttZEInLVwIQQNETCjOwEmiAPvI+0GSSgc3OiKZRczfzPjyJccBPrsM2uo5lSei76dep252r7y4YIthSzhZC8gDnuQDbKMZnqJ5q12kRW64bCEfvImo+PYKm/eYiORlsrBh0XImvC1qBIamQm65JeKfy2xXl8kdKeUccdrL81+A5HwajarYAUw=="
                ,Salt = "zIfkLYdQME2A72f8+fzUTnH04LK2HJC8NAYIAVdA3ma+Lnv7J5eRSsSVuWGW+//q+Z/Dn0ur229M9T6Lw2ynyl49fGeQnScin+plUVFkxot5WdHKF1X/ywYQ5w4a8P61ucK6NrKd8tSdPcWMgliQbx8WrHxWSBTFLNKgj3taOYE="};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(test);

            var model = new UserModell(database.Object);
            model.UserEntity = new UserEntity() {Username = "test", Password = "tes2t"};
            var ret = model.VerifyLogin();
            
            
            Assert.That(!ret);

        }
    }
}