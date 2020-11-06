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
            MonsterCardModell modell = new MonsterCardModell(cardEntity);
            List<CardModell> list = new List<CardModell>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString(),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            //Act
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            PackageModell package = new PackageModell(entity,database.Object);
            //Assert
            Assert.That(package.CardCount == list.Count);
        }
        
        [Test]
        public void PackageModell_Created_Failed()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            MonsterCardModell modell = new MonsterCardModell(cardEntity);
            List<CardModell> list = new List<CardModell>();
            PackageEntity entity = new PackageEntity(){Amount = 1 ,CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            //Act
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            //Assert
            Assert.That(() => new PackageModell(entity,database.Object) , Throws.Exception);
        }
        
        [Test]
        public void PackageModell_AddCard()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            MonsterCardModell modell = new MonsterCardModell(cardEntity);
            List<CardModell> list = new List<CardModell>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString(),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            int expectedCount = 6;
            //Act
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            PackageModell package = new PackageModell(entity,database.Object);
            package.AddCardToPackage(modell);
            //Assert
            Assert.That(package.CardCount == expectedCount);
        }
        
        [Test]
        public void PackageModell_AddCards()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            MonsterCardModell modell = new MonsterCardModell(cardEntity);
            List<CardModell> list = new List<CardModell>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString(),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            int expectedCount = 10;
            //Act
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
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
            MonsterCardModell modell = new MonsterCardModell(cardEntity);
            List<CardModell> list = new List<CardModell>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString(),CardsInPackage = list};
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            //Act
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            PackageModell package = new PackageModell(entity,database.Object);
            var result = package.Open();
            //Assert
            Assert.That(string.IsNullOrWhiteSpace(modell.Id));
            Assert.That(result.Count == package.CardCount);
            Assert.That(!string.IsNullOrWhiteSpace(result[0].Id));
        }

    } 
}