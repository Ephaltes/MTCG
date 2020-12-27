using System.Collections.Generic;
using Moq;
using MTCG;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
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
            
            Mock<IDatabase> database = new Mock<IDatabase>();
            UserModell player1 = new UserModell(database.Object);
            UserModell player2 = new UserModell(database.Object);
            
            GameModell game1 = new GameModell(player1);
            GameModell game2 = new GameModell(player2);
            
            deck1.Add(card);
            deck1.Add(card);
            deck1.Add(card);
            deck1.Add(card);
            
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);
            

            player1.UserEntity = new UserEntity() {Username = "Player1", DisplayName = "Player1"};
            player2.UserEntity = new UserEntity() {Username = "Player2" , DisplayName = "Player2"};

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
            CardEntity card = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            
            //Arrange
            CardEntity card2 = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};   
            List<CardEntity> deck1 = new List<CardEntity>();
            List<CardEntity> deck2 = new List<CardEntity>();
            
            Mock<IDatabase> database = new Mock<IDatabase>();
            UserModell player1 = new UserModell(database.Object);
            UserModell player2 = new UserModell(database.Object);
            
            GameModell game1 = new GameModell(player1);
            GameModell game2 = new GameModell(player2);
            
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
                 
            deck2.Add(card);
            deck2.Add(card);
            deck2.Add(card);
            deck2.Add(card);

            player1.UserEntity = new UserEntity() {Username = "Player1", DisplayName = "Player1"};
            player2.UserEntity = new UserEntity() {Username = "Player2" , DisplayName = "Player2"};

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
            CardEntity card1 = new CardEntity() { Damage = 10,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};

            //Arrange
            CardEntity card2 = new CardEntity() { Damage = 1,CardType = CardType.MonsterCard,Race = Race.Dragon, ElementType = ElementType.Water};
            List<CardEntity> deck1 = new List<CardEntity>();
            List<CardEntity> deck2 = new List<CardEntity>();
            
            Mock<IDatabase> database = new Mock<IDatabase>();
            UserModell player1 = new UserModell(database.Object);
            UserModell player2 = new UserModell(database.Object);
            
            GameModell game1 = new GameModell(player1);
            GameModell game2 = new GameModell(player2);
            
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            deck1.Add(card2);
            
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);
            deck2.Add(card2);

            player1.UserEntity = new UserEntity() {Username = "Player1", DisplayName = "Player1"};
            player2.UserEntity = new UserEntity() {Username = "Player2" , DisplayName = "Player2"};

            database.Setup(x => x.GetDeckFromUser(player1.UserEntity)).Returns(deck1);
            database.Setup(x => x.GetDeckFromUser(player2.UserEntity)).Returns(deck2);

            
            var result = game1.GetLog();
            
            //Assert
            Assert.That(result.GameEnd == GameEnd.Draw);
        }
    }
}