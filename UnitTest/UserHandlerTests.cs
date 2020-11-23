using System.Collections.Generic;
using Moq;
using MTCG.API;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using NUnit.Framework;
using WebServer;
using WebServer.Interface;

namespace UnitTest
{
    [TestFixture]

    public class UserHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateUserFromRequest_Success()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();

            string jsonRequest = "{\"Username\":\"test\", \"Password\":\"daniel\"}";

            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpBody).Returns(jsonRequest);
            database.Setup(x => x.CreateUser(It.IsAny<UserEntity>())).Returns(true);
            database.Setup(x => x.UserExists(It.IsAny<string>())).Returns(false);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();
            
            Assert.That(response.StatusCode==StatusCodes.Created);
        }
        
        [Test]
        public void CreateUserFromRequest_Failed()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();

            string jsonRequest = "{\"Username\":\"test\", \"Password\":\"daniel\"}";

            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpBody).Returns(jsonRequest);
            database.Setup(x => x.CreateUser(It.IsAny<UserEntity>())).Returns(false);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();
            
            Assert.That(response.StatusCode==StatusCodes.BadRequest);
        }
        
        [Test]
        public void GetUserInformation_Success()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            List<string> userrequest = new List<string>(){"users","testuser"};
            UserEntity retEntity = new UserEntity()
            {
                Username = userrequest[1],
                Description = "description",
                Image =  ";)"
            };
            
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(userrequest);
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(retEntity);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();

            var entity = response.ResponseMessage[0].Object as UserEntity;

            Assert.That(entity == retEntity);
        }
        
        [Test]
        public void GetUserInformation_Failed()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            List<string> userrequest = new List<string>(){"users","testuser"};
            UserEntity test = null;
            
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(userrequest);
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(test);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();

            var entity = response.ResponseMessage[0].Object as UserEntity;

            Assert.That(entity == null);
        }
    }
}
        