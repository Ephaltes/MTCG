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
    public class BattlesHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StartBattle_failed_NoCards()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"battles"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new BattlesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void StartBattle_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"battles"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new BattlesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void StartBattle_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test", DisplayName = "test"};
            var retEntity2 = new UserEntity {Username = "test2", Description = "test2", DisplayName = "test2"};
            var httprequest = new List<string> {"battles"};
            var cardList = new List<CardEntity>();
            
            cardList.Add(new CardEntity());
            cardList.Add(new CardEntity());
            cardList.Add(new CardEntity());
            cardList.Add(new CardEntity());

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.SetupSequence(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity).Returns(retEntity2);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            Task.Run(() =>
            {
                var handler = new BattlesHandler(request.Object, database.Object);
                handler.Handle();
            });
            
            var handler = new BattlesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
    }
}