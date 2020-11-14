using System.Collections.Generic;
using MTCG;
using MTCG.Model;
using MTCG.Model.BaseClass;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]

    public class StackClassTests
    {
        StackModell stack;

        [SetUp]
        public void Setup()
        {
            stack = new StackModell();
        }

        [Test]
        public void Add_BaseDragon_ToDeck()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,Race = Race.Dragon,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            //Act
            stack.Add(card1);
            var stackList = stack.GetStack();
            var result = stackList[0].Entity.Race == Race.Dragon;
            //Assert
            Assert.That(result);
        }
        
        [Test]
        public void AddByList_ToDeck()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            var list = new List<CardModell>();
            //Act
            list.Add(card1);
            list.Add(card2);
            list.Add(card1);
            stack.Add(list);
            var stackList = stack.GetStack();
            //Assert
            Assert.That(stackList.Count == 3 );
        }
    }
}