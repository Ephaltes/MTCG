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
    public class DeckHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetDeck_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void GetDeck_failed_noCards()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.NotFound);
        }
        
        [Test]
        public void GetDeck_success_normal()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();
            cardList.Add(new CardEntity());

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
        
        [Test]
        public void GetDeck_success_plain()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck","plain"};
            var cardList = new List<CardEntity>();
            cardList.Add(new CardEntity());

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
        
        [Test]
        public void PutDeck_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void PutDeck_failed_EmptyBody()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns("");
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void PutDeck_failed_rest()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(httprequest));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);
            database.Setup(x => x.SetDeckByCardIds(It.IsAny<List<string>>(),It.IsAny<UserEntity>()))
                .Returns(false);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.InternalServerError);
        }
        
        [Test]
        public void PutDeck_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(httprequest));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.PUT);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);
            database.Setup(x => x.SetDeckByCardIds(It.IsAny<List<string>>(),It.IsAny<UserEntity>()))
                .Returns(true);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
        
        [Test]
        public void PostDeck_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void PostDeck_failed_EmptyBody()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns("");
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void PostDeck_failed_rest()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject("string"));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);
            database.Setup(x => x.AddCardToDeckByCardId(It.IsAny<string>(),It.IsAny<UserEntity>()))
                .Returns(false);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.InternalServerError);
        }
        
        [Test]
        public void PostDeck_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject("string"));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);
            database.Setup(x => x.AddCardToDeckByCardId(It.IsAny<string>(),It.IsAny<UserEntity>()))
                .Returns(true);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
        
        [Test]
        public void DeleteDeck_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void DeleteDeck_failed_EmptyBody()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns("");
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void DeleteDeck_failed_rest()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject("string"));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);
            database.Setup(x => x.RemoveCardFromDeckByCardId(It.IsAny<string>(),It.IsAny<UserEntity>()))
                .Returns(false);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.InternalServerError);
        }
        
        [Test]
        public void DeleteDeck_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "test", Description = "test"};
            var httprequest = new List<string> {"deck"};
            var cardList = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject("string"));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetDeckFromUser(It.IsAny<UserEntity>())).Returns(cardList);
            database.Setup(x => x.RemoveCardFromDeckByCardId(It.IsAny<string>(),It.IsAny<UserEntity>()))
                .Returns(true);

            var handler = new DeckHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
    }
}