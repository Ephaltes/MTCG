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
            var card = new BaseDragonModell();
            //Act
            stack.Add(card);
            var stackList = stack.GetDeck();
            var result = stackList[0].GetType();
            //Assert
            Assert.That(result == typeof(BaseDragonModell));
        }
    }
}