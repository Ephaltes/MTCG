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
    public class PackageHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void CreatePackage_failed_NotAuthorized()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcToken");
            UserEntity retEntity = new UserEntity();
            var httprequest = new List<string> {"package"};
            PackageEntity entity = null;

            request.SetupGet(x => x.HttpBody).Returns("");
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new PackagesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Unauthorized);
        }
        
        [Test]
        public void CreatePackage_failed_EmptyBody()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity retEntity = new UserEntity();
            var httprequest = new List<string> {"package"};
            PackageEntity entity = null;

            request.SetupGet(x => x.HttpBody).Returns("");
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);

            var handler = new PackagesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void CreatePackage_failed_dbError()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity retEntity = new UserEntity();
            var httprequest = new List<string> {"package"};
            CardEntity card = new CardEntity() {CardType = CardType.SpellCard, Damage = 2};
            PackageEntity entity = new PackageEntity();
            entity.CardsInPackage = new List<CardEntity>();
            entity.CardsInPackage.Add(card);

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(entity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.AddPackage(It.IsAny<PackageEntity>())).Returns(false);

            var handler = new PackagesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.InternalServerError);
        }
        
        [Test]
        public void CreatePackage_failed_NoCards()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity retEntity = new UserEntity();
            var httprequest = new List<string> {"package"};
            PackageEntity entity = new PackageEntity();
            entity.CardsInPackage = new List<CardEntity>();

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(entity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.AddPackage(It.IsAny<PackageEntity>())).Returns(false);

            var handler = new PackagesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.BadRequest);
        }
        
        [Test]
        public void CreatePackage_success()
        {
            var request = new Mock<IRequestContext>();
            var database = new Mock<IDatabase>();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", "Basic user-mtcgToken");
            UserEntity retEntity = new UserEntity();
            var httprequest = new List<string> {"package"};
            CardEntity card = new CardEntity() {CardType = CardType.SpellCard, Damage = 2};
            PackageEntity entity = new PackageEntity();
            entity.CardsInPackage = new List<CardEntity>();
            entity.CardsInPackage.Add(card);

            request.SetupGet(x => x.HttpBody).Returns(JsonConvert.SerializeObject(entity));
            request.SetupGet(x => x.HttpHeader).Returns(header);
            request.SetupGet(x => x.HttpMethod).Returns(HttpMethods.POST);
            request.SetupGet(x => x.HttpRequest).Returns(httprequest);
            database.Setup(x => x.GetUserByToken(It.IsAny<string>())).Returns(retEntity);
            database.Setup(x => x.AddPackage(It.IsAny<PackageEntity>())).Returns(true);

            var handler = new PackagesHandler(request.Object, database.Object);
            var response = handler.Handle();

            Assert.That(response.StatusCode == StatusCodes.Created);
        }
    }
}