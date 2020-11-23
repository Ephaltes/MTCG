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
            var dragon = new CardModell(dragonEntity);
            var elf = new CardModell(elfEntity);
            //Act
            var result = dragon.CalculateDamge(elf);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Goblin_Attack_Dragon()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,Race = Race.Goblin,CardType = CardType.MonsterCard};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Dragon,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Orc_Attack_Wizard()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Wizard,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void FireSpell_Attack_Kraken()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void NormalSpell_Attack_Kraken()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Normal};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void WaterSpell_Attack_Knight()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Knight,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result >= 9999);
        }
        [Test]
        public void WaterSpell_Attack_Kraken()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Kraken,CardType = CardType.MonsterCard};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void WaterSpell_Attack_WeakOrc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water,WeakDamage = 20};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Fire};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result >= 20);
        }
        
        [Test]
        public void WaterSpell_Attack_Orc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Water,WeakDamage = 20};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Normal};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result >= 10);
        }
        
        [Test]
        public void FireSpell_Attack_WeakOrc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Fire,WeakDamage = 20};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Normal};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result >= 20);
        }
        
        [Test]
        public void NormalSpell_Attack_WeakOrc()
        {
            //Arrange
            CardEntity card1Entity = new CardEntity() { Damage = 10,CardType = CardType.SpellCard , ElementType = ElementType.Normal,WeakDamage = 20};   
            CardEntity card2Entity = new CardEntity() { Damage = 10,Race = Race.Orc,CardType = CardType.MonsterCard , ElementType = ElementType.Water};   
            var card1 = new CardModell(card1Entity);
            var card2 = new CardModell(card2Entity);
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result >= 20);
        }
      
        
    }
}