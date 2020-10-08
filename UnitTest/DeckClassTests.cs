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
        public void Add_BaseDragon_ToDeck()
        {
            //Arrange
            var card = new BaseDragonModell();
            //Act
            deck.Add(card);
            var deckList = deck.GetDeck();
            var result = deckList[0].GetType();
            //Assert
            Assert.That(result == typeof(BaseDragonModell));
        }
    }
}