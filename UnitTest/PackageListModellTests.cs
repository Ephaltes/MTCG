using System;
using System.Collections.Generic;
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

    public class PackageListModellTests
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
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            PackageModell package = new PackageModell(entity,database.Object);
            int expectedCount = 1;

            PackageListModell packageList = new PackageListModell();
            packageList.AddPackageToList(package);
            
            //Assert
            Assert.That(packageList.PackageModellList.Count == expectedCount);
        }
        
        [Test]
        public void PackageModell_Open_Successful()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            MonsterCardModell modell = new MonsterCardModell(cardEntity);
            List<CardModell> list = new List<CardModell>();
            PackageEntity entity = new PackageEntity(){Amount = 3,Id = Guid.NewGuid().ToString(),CardsInPackage = list};
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            PackageModell package = new PackageModell(entity,database.Object);
            int expectedCount = 1;
            
            package.AddCardToPackage(modell);
            
            PackageListModell packageList = new PackageListModell();
            packageList.AddPackageToList(package);
            packageList.Open();
            
            //Assert
            Assert.That(packageList.PackageModellList.Count == expectedCount);
        }
        
        [Test]
        public void PackageModell_Open_RemovePacakge()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            MonsterCardModell modell = new MonsterCardModell(cardEntity);
            List<CardModell> list = new List<CardModell>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString(),CardsInPackage = list};
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<CardModell>>())).Returns(true);
            PackageModell package = new PackageModell(entity,database.Object);
            int expectedCount = 0;
            
            package.AddCardToPackage(modell);
            
            PackageListModell packageList = new PackageListModell();
            packageList.AddPackageToList(package);
            var cards = packageList.Open();
            
            //Assert
            Assert.That(packageList.PackageModellList.Count == expectedCount);
            Assert.That(cards.Count > 0);
        }
    } 
}