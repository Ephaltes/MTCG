﻿using System.Collections.Generic;
using Moq;
using MTCG.API;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using Newtonsoft.Json;
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
            Dictionary<string,string> header = new Dictionary<string, string>();
            header.Add("Authorization","Basic test-mtcgToken");
            
            request.SetupGet(x => x.HttpHeader).Returns(header);
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
            Dictionary<string,string> header = new Dictionary<string, string>();
            header.Add("Authorization","Basic");
            
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(userrequest);
            database.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(test);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();

            var entity = response.ResponseMessage[0].Object as UserEntity;

            Assert.That(entity == null);
        }
        
        [Test]
        public void UpdateUserInformation_failed_token()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            Dictionary<string,string> header = new Dictionary<string, string>();
            header.Add("Authorization","Basic");
            UserEntity retEntity = new UserEntity();

            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();

            Assert.That( response.ResponseMessage[0].Status == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void UpdateUserInformation_failed_verifyToken()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            Dictionary<string,string> header = new Dictionary<string, string>();
            header.Add("Authorization","Basic wrongmtcgToken");
            UserEntity retEntity = null;

            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();

            Assert.That( response.ResponseMessage[0].Status == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void UpdateUserInformation_failed_emptyBody()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            Dictionary<string,string> header = new Dictionary<string, string>();
            header.Add("Authorization","Basic wrong-mtcgToken");
            UserEntity retEntity = new UserEntity();

            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();

            Assert.That( response.ResponseMessage[0].Status == StatusCodes.BadRequest);
        }
        
        [Test]
        public void UpdateUserInformation_success()
        {
            Mock<IRequestContext> request = new Mock<IRequestContext>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            Dictionary<string,string> header = new Dictionary<string, string>();
            header.Add("Authorization","Basic wrong-mtcgToken");
            UserEntity retEntity = new UserEntity()
            {
                Description = "test"
            };

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.UpdateUser(It.IsAny<UserEntity>())).Returns(true);
            
            var handler = new UsersHandler(request.Object,database.Object);
            var response = handler.Handle();

            Assert.That( response.ResponseMessage[0].Status == StatusCodes.OK);
        }
    }
}
        