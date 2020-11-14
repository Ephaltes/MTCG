using MTCG;
using MTCG.Helpers;
using MTCG.Model;
using MTCG.Model.BaseClass;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]

    public class GameModellTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Fight_Player1_Wins()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            var card = new MonsterCardModell(card1Entity);
            
            //Arrange
            CardEntity card2Entity = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            var card2 = new MonsterCardModell(card2Entity);
            
            DeckModell deck1 = new DeckModell();
            DeckModell deck2 = new DeckModell();
            
            deck1.DeckList.Add(card);
            deck1.DeckList.Add(card);
            deck1.DeckList.Add(card);
            deck1.DeckList.Add(card);
            
            deck2.DeckList.Add(card2);
            deck2.DeckList.Add(card2);
            deck2.DeckList.Add(card2);
            deck2.DeckList.Add(card2);

            var result = GameModell.Fight(deck1, deck2);
            
            //Assert
            Assert.That(result.Item1 == GameEnd.Player1);
        }
        
        [Test]
        public void Fight_Player2_Wins()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            var card = new MonsterCardModell(card1Entity);
            
            //Arrange
            CardEntity card2Entity = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            var card2 = new MonsterCardModell(card2Entity);
            
            DeckModell deck1 = new DeckModell();
            DeckModell deck2 = new DeckModell();
            
            deck1.DeckList.Add(card2);
            deck1.DeckList.Add(card2);
            deck1.DeckList.Add(card2);
            deck1.DeckList.Add(card2);
            
            deck2.DeckList.Add(card);
            deck2.DeckList.Add(card);
            deck2.DeckList.Add(card);
            deck2.DeckList.Add(card);

            var result = GameModell.Fight(deck1, deck2);
            
            //Assert
            Assert.That(result.Item1 == GameEnd.Player2);
        }
        
        [Test]
        public void Fight_Draw()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            var card = new MonsterCardModell(card1Entity);
            
            //Arrange
            CardEntity card2Entity = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            var card2 = new MonsterCardModell(card2Entity);
            
            DeckModell deck1 = new DeckModell();
            DeckModell deck2 = new DeckModell();
            
            deck1.DeckList.Add(card2);
            deck1.DeckList.Add(card2);
            deck1.DeckList.Add(card2);
            deck1.DeckList.Add(card2);
            
            deck2.DeckList.Add(card2);
            deck2.DeckList.Add(card2);
            deck2.DeckList.Add(card2);
            deck2.DeckList.Add(card2);

            var result = GameModell.Fight(deck1, deck2);
            
            //Assert
            Assert.That(result.Item1 == GameEnd.Draw);
        }
    }
}