using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MTCG.API;
using MTCG.Entity;
using MTCG.Interface;
using Newtonsoft.Json;
using NUnit.Framework;
using WebServer;
using WebServer.Interface;

namespace UnitTest
{
    [TestFixture]
    public class SessionsHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Login_failed_EmptyBody()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity retEntity = null;
            var httprequest = new List<string> {"sessions"};

            request.SetupGet(x => x.HttpBody).Returns("");
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new SessionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void Login_failed_missingParameter()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity retEntity = new UserEntity();
            var httprequest = new List<string> {"sessions"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new SessionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void Login_failed_wrongPW()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity db = new UserEntity() {Username = "test",Password = "1JkCZss1iMcyeACsRsfwY3ErmtJMBWTVtDCu0U+wvDaZZB1BDTrj6NbitpYQuUMQSzLnhFkz/e+fZRIjv8WaHvvJGmMcyUD06pGVRYeyHk0Jdb+cL6QKm8EzBhOaiSGKq7K8yVpCxPjkSI9/oVTs8WAN6vcgmG1RhttZEInLVwIQQNETCjOwEmiAPvI+0GSSgc3OiKZRczfzPjyJccBPrsM2uo5lSei76dep252r7y4YIthSzhZC8gDnuQDbKMZnqJ5q12kRW64bCEfvImo+PYKm/eYiORlsrBh0XImvC1qBIamQm65JeKfy2xXl8kdKeUccdrL81+A5HwajarYAUw=="
                ,Salt = "zIfkLYdQME2A72f8+fzUTnH04LK2HJC8NAYIAVdA3ma+Lnv7J5eRSsSVuWGW+//q+Z/Dn0ur229M9T6Lw2ynyl49fGeQnScin+plUVFkxot5WdHKF1X/ywYQ5w4a8P61ucK6NrKd8tSdPcWMgliQbx8WrHxWSBTFLNKgj3taOYE="};
            UserEntity user = new UserEntity() {Username = "test", Password = "te2st"};
            
            
            var httprequest = new List<string> {"sessions"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(user));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(db);

            var handler = new SessionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void Login_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity db = new UserEntity() {Username = "test",Password = "1JkCZss1iMcyeACsRsfwY3ErmtJMBWTVtDCu0U+wvDaZZB1BDTrj6NbitpYQuUMQSzLnhFkz/e+fZRIjv8WaHvvJGmMcyUD06pGVRYeyHk0Jdb+cL6QKm8EzBhOaiSGKq7K8yVpCxPjkSI9/oVTs8WAN6vcgmG1RhttZEInLVwIQQNETCjOwEmiAPvI+0GSSgc3OiKZRczfzPjyJccBPrsM2uo5lSei76dep252r7y4YIthSzhZC8gDnuQDbKMZnqJ5q12kRW64bCEfvImo+PYKm/eYiORlsrBh0XImvC1qBIamQm65JeKfy2xXl8kdKeUccdrL81+A5HwajarYAUw=="
                ,Salt = "zIfkLYdQME2A72f8+fzUTnH04LK2HJC8NAYIAVdA3ma+Lnv7J5eRSsSVuWGW+//q+Z/Dn0ur229M9T6Lw2ynyl49fGeQnScin+plUVFkxot5WdHKF1X/ywYQ5w4a8P61ucK6NrKd8tSdPcWMgliQbx8WrHxWSBTFLNKgj3taOYE="};
            UserEntity user = new UserEntity() {Username = "test", Password = "test"};
            
            
            var httprequest = new List<string> {"sessions"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(user));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(db);

            var handler = new SessionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
    }
}