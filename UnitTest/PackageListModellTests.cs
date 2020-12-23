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
        public void PackageListModell_Created_Successful()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            CardModell modell = new CardModell(cardEntity);
            List<ICard> list = new List<ICard>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString("N"),CardsInPackage = list};
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<ICard>>())).Returns(true);
            PackageModell package = new PackageModell(entity,database.Object);
            int expectedCount = 1;

            PackageListModell packageList = new PackageListModell();
            packageList.AddPackageToList(package);
            
            //Assert
            Assert.That(packageList.PackageModellList.Count == expectedCount);
        }
        
        [Test]
        public void PackageListModell_Open_Successful()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            CardModell modell = new CardModell(cardEntity);
            List<ICard> list = new List<ICard>();
            PackageEntity entity = new PackageEntity(){Amount = 3,Id = Guid.NewGuid().ToString("N"),CardsInPackage = list};
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<ICard>>())).Returns(true);
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
        public void PackageListModell_Open_RemovePacakge()
        {
            //Arrange
            CardEntity cardEntity = new CardEntity(){Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon};
            CardModell modell = new CardModell(cardEntity);
            List<ICard> list = new List<ICard>();
            PackageEntity entity = new PackageEntity(){Amount = 1,Id = Guid.NewGuid().ToString("N"),CardsInPackage = list};
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            list.Add(modell);
            Mock<IDatabase> database = new Mock<IDatabase>();
            database.Setup(x => x.AddCardsToDatabase(It.IsAny<List<ICard>>())).Returns(true);
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