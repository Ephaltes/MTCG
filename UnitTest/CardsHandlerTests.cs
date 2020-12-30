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
    public class CardsHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetStack_failed_NoCards()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"cards"};
            List<CardEntity> cardList = null;

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetStackFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new CardsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.NotFound);
        }
        
        [Test]
        public void GetStack_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"cards"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetStackFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new CardsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void GetStack_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test", DisplayName = "test"};
            var httprequest = new List<string> {"cards"};
            var cardList = new List<CardEntity>();
            
            cardList.Add(new CardEntity());

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetStackFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new CardsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
    }
}