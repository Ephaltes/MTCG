using System;
using System.Collections.Generic;
using Moq;
using MTCG;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
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
            var cardEntity = new CardEntity {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, Id = Guid.NewGuid().ToString("N"), CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            var package = new PackageModell(entity, database.Object);
            //Assert
            Assert.That(package.CardCount == list.Count);
        }

        [Test]
        public void PackageModell_Created_Failed()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            //Assert
            Assert.That(() => new PackageModell(entity, database.Object), Throws.Exception);
        }

        [Test]
        public void PackageModell_AddCard()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, Id = Guid.NewGuid().ToString("N"), CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            var expectedCount = 6;
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            var package = new PackageModell(entity, database.Object);
            package.AddCardToPackage(cardEntity);
            //Assert
            Assert.That(package.CardCount == expectedCount);
        }

        [Test]
        public void PackageModell_AddCards()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, Id = Guid.NewGuid().ToString("N"), CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            var expectedCount = 10;
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            var package = new PackageModell(entity, database.Object);
            package.AddCardsToPackage(list);
            //Assert
            Assert.That(package.CardCount == expectedCount);
        }

        [Test]
        public void PackageModell_Open()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, Id = Guid.NewGuid().ToString("N"), CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            database.Setup(x => x.OpenPackage(It.IsAny<PackageEntity>(), It.IsAny<UserEntity>())).Returns(true);
            var userEntity = new UserEntity
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
            var package = new PackageModell(entity, database.Object);
            var result = package.Open(userEntity);
            //Assert
            Assert.That(result.Count == package.CardCount);
        }
    }
}