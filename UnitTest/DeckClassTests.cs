using MTCG;
using MTCG.Helpers;
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

            deck.DeckList.Add(card);
            var deckList = deck.DeckList;
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
            deck.DeckList.Add(card);
            deck.DeckList.Remove(card);
            var result = deck.DeckList;
            //Assert
            Assert.That(result.Count == 0);
        }

        [Test]
        public void CopyDeck()
        {
            DeckModell deck = new DeckModell();
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard , ElementType = ElementType.Water};   
            var card = new CardModell(card1Entity);
            deck.DeckList.Add(card);

            var deck2 = deck.CloneJson();
            deck2.DeckList.Add(card);
            
            Assert.That(deck.DeckList.Count == 1 && deck2.DeckList.Count==2);
        }
    }
}