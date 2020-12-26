using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MTCG;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using MTCG.Model.BaseClass;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]

    public class PackageClassTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PackageModell_Created_Successful()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            List<CardEntity> list = new List<CardEntity>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString("N"),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            PackageModell package = new PackageModell(entity,database.Object);
            //Assert
            Assert.That(package.CardCount == list.Count);
        }
        
        [Test]
        public void PackageModell_Created_Failed()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            List<CardEntity> list = new List<CardEntity>();
            PackageEntity entity = new PackageEntity(){Amount = 1 ,CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            //Assert
            Assert.That(() => new PackageModell(entity,database.Object) , Throws.Exception);
        }
        
        [Test]
        public void PackageModell_AddCard()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            List<CardEntity> list = new List<CardEntity>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString("N"),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            int expectedCount = 6;
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            PackageModell package = new PackageModell(entity,database.Object);
            package.AddCardToPackage(cardEntity);
            //Assert
            Assert.That(package.CardCount == expectedCount);
        }
        
        [Test]
        public void PackageModell_AddCards()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            List<CardEntity> list = new List<CardEntity>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString("N"),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            int expectedCount = 10;
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            PackageModell package = new PackageModell(entity,database.Object);
            package.AddCardsToPackage(list);
            //Assert
            Assert.That(package.CardCount == expectedCount);
        }
        
        [Test]
        public void PackageModell_Open()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            List<CardEntity> list = new List<CardEntity>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString("N"),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            database.Setup(x => x.OpenPackage(It.IsAny<PackageEntity>(), It.IsAny<UserEntity>())).Returns(true);
            UserEntity userEntity = new UserEntity()
            {
                Coins = 30,
                Token = "test-mtcgToken"
            };
            
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            PackageModell package = new PackageModell(entity,database.Object);
            var result = package.Open(userEntity);
            //Assert
            Assert.That(result.Count == package.CardCount);
        }

    } 
}