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
            database.Setup(x => x.AddPackage(It.IsAny<PackageEntity>())).Returns(true);
            //Act
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            list.Add(cardEntity);
            var packageModell = new PackageModell(database.Object);
            var package = packageModell.AddPackage(entity);
            //Assert
            Assert.That(package == 0 && packageModell.Entity.CardsInPackage.Count == list.Count);
        }
        [Test]
        public void PackageModell_Create_Empty_Fails()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, Id = Guid.NewGuid().ToString("N"), CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            database.Setup(x => x.AddPackage(It.IsAny<PackageEntity>())).Returns(true);
            //Act
            var packageModell = new PackageModell(database.Object);
            var package = packageModell.AddPackage(entity);
            //Assert
            Assert.That(package == 1);
        }
        
        [Test]
        public void PackageModell_Create_NoType()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            database.Setup(x => x.AddPackage(It.IsAny<PackageEntity>())).Returns(true);
            //Act
            
            entity.CardsInPackage.Add(cardEntity);
            
            var packageModell = new PackageModell(database.Object);
            var package = packageModell.AddPackage(entity);
            //Assert
            Assert.That(package == 2);
        }
        
        [Test]
        public void PackageModell_Create_failed()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, Race = Race.Dragon , CardType = CardType.MonsterCard};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, Id = Guid.NewGuid().ToString("N"), CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddPackage(It.IsAny<PackageEntity>())).Returns(false);
            //Act
            
            entity.CardsInPackage.Add(cardEntity);
            
            var packageModell = new PackageModell(database.Object);
            var package = packageModell.AddPackage(entity);
            //Assert
            Assert.That(package == 3);
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
            var package = new PackageModell(database.Object);
            package.Entity = entity;
            var result = package.Open(userEntity);
            //Assert
            Assert.That(result.Count == package.Entity.CardsInPackage.Count);
        }
        
        [Test]
        public void PackageModell_Open_failed()
        {
            //Arrange
            var cardEntity = new CardEntity {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon};
            var list = new List<CardEntity>();
            var entity = new PackageEntity {Amount = 1, Id = Guid.NewGuid().ToString("N"), CardsInPackage = list};
            var database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardEntity>>())).Returns(true);
            database.Setup(x => x.OpenPackage(It.IsAny<PackageEntity>(), It.IsAny<UserEntity>())).Returns(false);
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
            var package = new PackageModell(database.Object);
            package.Entity = entity;
            var result = package.Open(userEntity);
            //Assert
            Assert.That(result == null);
        }
    }
}