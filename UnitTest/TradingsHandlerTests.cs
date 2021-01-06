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
    public class TradingsHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

       
        [Test]
        public void GetTrades_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};
            var list = new List<TradingEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetAllTradingDeals()).Returns(list);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void GetTrades_failed_NoTrades()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};
            var list = new List<TradingEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetAllTradingDeals()).Returns(list);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.NotFound);
        }
        
        [Test]
        public void GetTrades_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};
            var list = new List<TradingEntity>();
            
            list.Add(new TradingEntity());

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.GET);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetAllTradingDeals()).Returns(list);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
        
        [Test]
        public void PostTrade_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void PostTrade_failed_EmptyBody()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};

            request.SetupGet(x => x.HttpBody).Returns("");
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void PostTrade_failed_NoDamage()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};
            var entity = new TradingEntity() { };

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(entity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.NotFound);
        }
        
        [Test]
        public void PostTrade_Add_trade_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};
            var entity = new TradingEntity() { WantMinDamage = 1};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(entity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.AddTrade(It.IsAny<TradingEntity>())).Returns(true);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Created);
        }
        
        [Test]
        public void PostTrade_Add_trade_nosuccess()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};
            var entity = new TradingEntity() { WantMinDamage = 1};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(entity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.AddTrade(It.IsAny<TradingEntity>())).Returns(false);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.InternalServerError);
        }
        
        [Test]
        public void PostTrade_trade_noId()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings",""};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject("TradeID"));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.AddTrade(It.IsAny<TradingEntity>())).Returns(true);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.NotFound);
        }
        
        [Test]
        public void PostTrade_trade_failed_db()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings","TradeID"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject("TradeID"));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.TradeCards(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<UserEntity>())).Returns(false);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.InternalServerError);
        }
        
        [Test]
        public void PostTrade_trade_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings","TradeID"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject("TradeID"));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.TradeCards(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<UserEntity>())).Returns(true);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
        
        [Test]
        public void DeleteTrade_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity retEntity = null;
            var httprequest = new List<string> {"tradings"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void DeleteTrade_failed_missingParameter()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        [Test]
        public void DeleteTrade_failed_missingParameter2()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings",""};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        [Test]
        public void DeleteTrade_failed_db()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings","tradeID"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.RemoveTradeByUserRequest(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.InternalServerError);
        }
        
        [Test]
        public void DeleteTrade_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"tradings","tradeID"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.DELETE);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.RemoveTradeByUserRequest(It.IsAny<string>(), It.IsAny<int>())).Returns(true);

            var handler = new TradingsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
        
    }
}