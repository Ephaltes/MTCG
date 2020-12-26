using System.Collections.Generic;
using MTCG;
using MTCG.Model;
using MTCG.Model.BaseClass;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]

    public class CalculateDamageTests
    {

        [SetUp]
        public void Setup()
        {
        }
        
        
        [Test]
        public void Dragon_Attack_FireElf()
        {
            //Arrange
            CardEntity dragonEntity = new CardEntity() { Damage = 10,Race = Race.Dragon,CardType = CardType.MonsterCard};   
            CardEntity elfEntity = new CardEntity() { Damage = 10,Race = Race.FireElf,CardType = CardType.MonsterCard};   
            //Act
            var result = GameModell.CalculateDamge(dragonEntity, elfEntity);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Goblin_Attack_Dragon()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,Race = Race.Goblin,CardType = CardType.MonsterCard};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Dragon,CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Orc_Attack_Wizard()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Wizard,CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void FireSpell_Attack_Kraken()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void NormalSpell_Attack_Kraken()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Normal};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void WaterSpell_Attack_Knight()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Knight,CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 9999);
        }
        [Test]
        public void WaterSpell_Attack_Kraken()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void WaterSpell_Attack_WeakOrc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Fire};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 20);
        }
        
        [Test]
        public void WaterSpell_Attack_Orc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 20,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Normal};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 10);
        }
        
        [Test]
        public void FireSpell_Attack_WeakOrc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Fire};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Normal};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 20);
        }
        
        [Test]
        public void NormalSpell_Attack_WeakOrc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Normal};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Water};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 20);
        }
      
        
    }
}