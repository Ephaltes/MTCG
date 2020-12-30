using System.Collections.Generic;
using Moq;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class ScoreClassTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void LoadScoreBoard_success()
        {
            var userEntity = new UserEntity()
            {
                DisplayName = "test"
            };
            var UserEntityList = new List<UserEntity>();
            var database = new Mock<IDatabase>();
            database.Setup(x => x.LoadScoreBoard()).Returns(UserEntityList);

            UserEntityList.Add(userEntity);
            
            var model = new ScoreModell(database.Object);
            var score = model.ScoreBoard;
            
            
            Assert.That(score.Count > 0);
        }
        
        [Test]
        public void LoadScoreBoard_fail()
        {
            var UserEntityList = new List<UserEntity>();
            var database = new Mock<IDatabase>();
            database.Setup(x => x.LoadScoreBoard()).Returns(UserEntityList);

            
            var model = new ScoreModell(database.Object);
            var score = model.ScoreBoard;
            
            
            Assert.That(score == null);
        }
    }
}