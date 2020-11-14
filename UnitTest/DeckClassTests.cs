using MTCG;
using MTCG.Model;
using MTCG.Model.BaseClass;
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
        public void Add_Monster_ToDeck()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            var card = new CardModell(card1Entity);
            //Act
            deck.Add(card);
            var deckList = deck.GetDeck();
            var result = deckList[0].Entity.Race == Race.Dragon;;
            //Assert
            Assert.That(result);
        }
        
        [Test]
        public void Remove_BaseDragon_FromDeck()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard , ElementType = ElementType.Water};   
            var card = new CardModell(card1Entity);
            //Act
            deck.Add(card);
            deck.Remove(card);
            var result = deck.GetDeck();
            //Assert
            Assert.That(result.Count == 0);
        }
        
        [Test]
        public void AddTemp_Card_ToDeck()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            deck.Add(card1);
            deck.AddTemp(card2);
            var result = deck.GetDeck();
            //Assert
            Assert.That(result.Count == 2);
        }
        
        [Test]
        public void RemoveTemp_BaseDragon_FromDeck()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            deck.Add(card1);
            deck.AddTemp(card2);
            deck.RemoveTempCards();
            var result = deck.GetDeck();
            //Assert
            Assert.That(result.Count == 1);
        }
        
        
    }
}