using System.Collections.Generic;
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
            CardEntity card = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            
            //Arrange
            CardEntity card2 = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            List<CardEntity> deck1 = new List<CardEntity>();
            List<CardEntity> deck2 = new List<CardEntity>();
            
            deck1.Add(card);
            deck1.Add(card);
            deck1.Add(card);
            deck1.Add(card);
            
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);

            var result = GameModell.Fight(deck1, deck2);
            
            //Assert
            Assert.That(result.GameEnd == GameEnd.Player1);
        }
        
        [Test]
        public void Fight_Player2_Wins()
        {
            //Arrange
            CardEntity card = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            
            //Arrange
            CardEntity card2 = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            List<CardEntity> deck1 = new List<CardEntity>();
            List<CardEntity> deck2 = new List<CardEntity>();
            
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
                 
            deck2.Add(card);
            deck2.Add(card);
            deck2.Add(card);
            deck2.Add(card);

            var result = GameModell.Fight(deck1, deck2);
            
            //Assert
            Assert.That(result.GameEnd == GameEnd.Player2);
        }
        
        [Test]
        public void Fight_Draw()
        {
            //Arrange
            CardEntity card1 = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};

            //Arrange
            CardEntity card2 = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};
            List<CardEntity> deck1 = new List<CardEntity>();
            List<CardEntity> deck2 = new List<CardEntity>();
            
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);

            var result = GameModell.Fight(deck1, deck2);
            
            //Assert
            Assert.That(result.GameEnd == GameEnd.Draw);
        }
    }
}