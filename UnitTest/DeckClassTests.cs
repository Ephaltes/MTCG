using MTCG.Model;
using MTCG.Model.BaseClass;
using MTCG.Model.MonsterTypes.Dragon;
using MTCG.Model.MonsterTypes.Knight;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]

    public class DeckClassTests
    {
        public DeckModell deck;

        [SetUp]
        public void Setup()
        {
            deck = new DeckModell();
        }

        [Test]
        public void Add_BaseDragon_ToDeck()
        {
            //Arrange
            var card = new RedDragon();
            //Act
            deck.Add(card);
            var deckList = deck.GetDeck();
            var result = ((MonsterCardModell)deckList[0]).Race == Race.Dragon;;
            //Assert
            Assert.That(result);
        }
        
        [Test]
        public void Remove_BaseDragon_FromDeck()
        {
            //Arrange
            var card = new RedDragon();
            //Act
            deck.Add(card);
            deck.Remove(card);
            var result = deck.GetDeck();
            //Assert
            Assert.That(result.Count == 0);
        }
        
        [Test]
        public void AddTemp_BaseDragon_ToDeck()
        {
            //Arrange
            var card = new GalaxyKnight();
            var card2 = new RedDragon();
            //Act
            deck.Add(card);
            deck.AddTemp(card2);
            var result = deck.GetDeck();
            //Assert
            Assert.That(result.Count == 2);
        }
        
        [Test]
        public void RemoveTemp_BaseDragon_FromDeck()
        {
            //Arrange
            var card = new GalaxyKnight();
            var card2 = new RedDragon();
            //Act
            deck.Add(card);
            deck.AddTemp(card2);
            deck.RemoveTempCards();
            var result = deck.GetDeck();
            //Assert
            Assert.That(result.Count == 1);
        }
        
        
    }
}