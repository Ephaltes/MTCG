using System.Collections.Generic;
using MTCG.Model;
using MTCG.Model.BaseClass;
using MTCG.Model.MonsterTypes.Dragon;
using MTCG.Model.MonsterTypes.Goblin;
using MTCG.Model.MonsterTypes.Orc;
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
            var card = new RedDragon();
            //Act
            stack.Add(card);
            var stackList = stack.GetDeck();
            var result = ((MonsterCardModell) stackList[0]).Race == Race.Dragon;
            //Assert
            Assert.That(result);
        }
        
        [Test]
        public void AddByList_ToDeck()
        {
            //Arrange
            var list = new List<CardModell>();
            //Act
            list.Add(new RedDragon());
            list.Add(new GoblinLackey());
            list.Add(new OrcWarrior());
            stack.Add(list);
            var stackList = stack.GetDeck();
            //Assert
            Assert.That(stackList.Count == 3 );
        }
    }
}