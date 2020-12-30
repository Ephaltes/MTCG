using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
    public class TransactionHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void Transaction_failed_missingParameter()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"transaction"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TransactionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void Transaction_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"transaction","packages"};

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new TransactionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void Transaction_failed_NoCoins()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test"};
            var httprequest = new List<string> {"transaction","packages"};
            List<IPackage> packages = new List<IPackage>();
            
            packages.Add(new PackageModell(database.Object));
            
            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetPackages()).Returns(packages);

            var handler = new TransactionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void Transaction_open_noCards()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test",Coins = 20};
            var httprequest = new List<string> {"transaction","packages"};
            List<IPackage> packages = null;
            
            //packages.Add(new PackageModell(database.Object){Entity = new PackageEntity(){Amount = 2}});
            
            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetPackages()).Returns(packages);
            database.Setup(x => x.OpenPackage(It.IsAny<PackageEntity>(), It.IsAny<UserEntity>())).Returns(false);

            var handler = new TransactionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.NotFound);
        }
        
        [Test]
        public void Transaction_open_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            var retEntity = new UserEntity {Username = "wrong", Description = "test",Coins = 20};
            var httprequest = new List<string> {"transaction","packages"};
            List<IPackage> packages = new List<IPackage>();
            List<CardEntity> cardEntities = new List<CardEntity>() {new CardEntity()};
            
            packages.Add(new PackageModell(database.Object){Entity = new PackageEntity(){Amount = 2,CardsInPackage = cardEntities}});
            
            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(retEntity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.GetPackages()).Returns(packages);
            database.Setup(x => x.OpenPackage(It.IsAny<PackageEntity>(), It.IsAny<UserEntity>())).Returns(true);

            var handler = new TransactionsHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.OK);
        }
    }
}