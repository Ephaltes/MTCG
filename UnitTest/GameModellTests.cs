using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MTCG;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using NUnit.Framework;

namespace UnitTest
{
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
            var card = new CardEntity
                {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon, ElementType = ElementType.Water};

            //Arrange
            var card2 = new CardEntity
                {Damage = 1, CardType = CardType.MonsterCard, Race = Race.Dragon, ElementType = ElementType.Water};
            var deck1 = new List<CardEntity>();
            var deck2 = new List<CardEntity>();

            var database = new Mock<IDatabase>();
            var player1 = new UserModell(database.Object);
            var player2 = new UserModell(database.Object);

            var game1 = new GameModell(player1);
            var game2 = new GameModell(player2);

            deck1.Add(card);
            deck1.Add(card);
            deck1.Add(card);
            deck1.Add(card);

            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);


            player1.UserEntity = new UserEntity {Username = "Player1", DisplayName = "Player1"};
            player2.UserEntity = new UserEntity {Username = "Player2", DisplayName = "Player2"};

            database.Setup(x => x.GetDeckFromUser(player1.UserEntity)).Returns(deck1);
            database.Setup(x => x.GetDeckFromUser(player2.UserEntity)).Returns(deck2);

            var result = game1.GetLog();

            //Assert
            Assert.That(result.Winner == player1.UserEntity.Username);
        }

        [Test]
        public void Fight_Player2_Wins()
        {
            //Arrange
            var card = new CardEntity
                {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon, ElementType = ElementType.Water};

            //Arrange
            var card2 = new CardEntity
                {Damage = 1, CardType = CardType.MonsterCard, Race = Race.Dragon, ElementType = ElementType.Water};
            var deck1 = new List<CardEntity>();
            var deck2 = new List<CardEntity>();

            var database = new Mock<IDatabase>();
            var player1 = new UserModell(database.Object);
            var player2 = new UserModell(database.Object);

            var game1 = new GameModell(player1);
            var game2 = new GameModell(player2);

            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);

            deck2.Add(card);
            deck2.Add(card);
            deck2.Add(card);
            deck2.Add(card);

            player1.UserEntity = new UserEntity {Username = "Player1", DisplayName = "Player1"};
            player2.UserEntity = new UserEntity {Username = "Player2", DisplayName = "Player2"};

            database.Setup(x => x.GetDeckFromUser(player1.UserEntity)).Returns(deck1);
            database.Setup(x => x.GetDeckFromUser(player2.UserEntity)).Returns(deck2);

            var result = game1.GetLog();

            //Assert
            Assert.That(result.Winner == player2.UserEntity.Username);
        }

        [Test]
        public void Fight_Draw()
        {
            //Arrange
            var card1 = new CardEntity
                {Damage = 10, CardType = CardType.MonsterCard, Race = Race.Dragon, ElementType = ElementType.Water};

            //Arrange
            var card2 = new CardEntity
                {Damage = 1, CardType = CardType.MonsterCard, Race = Race.Dragon, ElementType = ElementType.Water};
            var deck1 = new List<CardEntity>();
            var deck2 = new List<CardEntity>();

            var database = new Mock<IDatabase>();
            var player1 = new UserModell(database.Object);
            var player2 = new UserModell(database.Object);

            var game1 = new GameModell(player1);
            var game2 = new GameModell(player2);

            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);

            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);

            player1.UserEntity = new UserEntity {Username = "Player1", DisplayName = "Player1"};
            player2.UserEntity = new UserEntity {Username = "Player2", DisplayName = "Player2"};

            database.Setup(x => x.GetDeckFromUser(player1.UserEntity)).Returns(deck1);
            database.Setup(x => x.GetDeckFromUser(player2.UserEntity)).Returns(deck2);


            var result = game1.GetLog();

            //Assert
            Assert.That(result.GameEnd == GameEnd.Draw);
        }

        [Test]
        public void TryFightNotEnoughPlayer()
        {
            UserEntity userEntity = new UserEntity(){Username = "test",DisplayName = "test"};
            UserEntity userEntity2 = new UserEntity(){Username = "test2",DisplayName = "test2"};
            Mock<IDatabase> database = new Mock<IDatabase>();
            UserModell userModell = new UserModell(database.Object);
            GameModell gameModell = new GameModell(userModell);
            ReportEntity report1 = null;
            ReportEntity report2 = null;
            
            userModell.UserEntity = userEntity;
            Task.Run(() =>
            {
                GameModell gameModell = new GameModell(new UserModell(database.Object){UserEntity = userEntity2});
                report1 = gameModell.GetLog();
            });
            report2 = gameModell.GetLog();
            
            Assert.That(report1 == report2);
        }
    }
}