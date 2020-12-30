using MTCG;
using MTCG.Entity;
using MTCG.Model;
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
            var dragonEntity = new CardEntity {Damage = 10, Race = Race.Dragon, CardType = CardType.MonsterCard};
            var elfEntity = new CardEntity {Damage = 10, Race = Race.FireElf, CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(dragonEntity, elfEntity);
            //Assert
            Assert.That(result <= 0);
        }

        [Test]
        public void Goblin_Attack_Dragon()
        {
            //Arrange
            var card1Entity = new CardEntity {Damage = 10, Race = Race.Goblin, CardType = CardType.MonsterCard};
            var card2Entity = new CardEntity {Damage = 10, Race = Race.Dragon, CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }

        [Test]
        public void Orc_Attack_Wizard()
        {
            //Arrange
            var card1Entity = new CardEntity {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard};
            var card2Entity = new CardEntity {Damage = 10, Race = Race.Wizard, CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }

        [Test]
        public void FireSpell_Attack_Kraken()
        {
            //Arrange
            var card1Entity = new CardEntity {Damage = 10, CardType = CardType.SpellCard};
            var card2Entity = new CardEntity {Damage = 10, Race = Race.Kraken, CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }

        [Test]
        public void NormalSpell_Attack_Kraken()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Normal};
            var card2Entity = new CardEntity {Damage = 10, Race = Race.Kraken, CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }

        [Test]
        public void WaterSpell_Attack_Knight()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Water};
            var card2Entity = new CardEntity {Damage = 10, Race = Race.Knight, CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 9999);
        }

        [Test]
        public void WaterSpell_Attack_Kraken()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Water};
            var card2Entity = new CardEntity {Damage = 10, Race = Race.Kraken, CardType = CardType.MonsterCard};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result <= 0);
        }

        [Test]
        public void WaterSpell_Attack_WeakOrc()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Water};
            var card2Entity = new CardEntity
                {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard, ElementType = ElementType.Fire};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 20);
        }

        [Test]
        public void WaterSpell_Attack_Orc()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 20, CardType = CardType.SpellCard, ElementType = ElementType.Water};
            var card2Entity = new CardEntity
                {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard, ElementType = ElementType.Normal};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 10);
        }

        [Test]
        public void FireSpell_Attack_WeakOrc()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Fire};
            var card2Entity = new CardEntity
                {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard, ElementType = ElementType.Normal};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 20);
        }

        [Test]
        public void NormalSpell_Attack_Orc()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Normal};
            var card2Entity = new CardEntity
                {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard, ElementType = ElementType.Normal};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 10);
        }
        
        [Test]
        public void NormalOrc_Attack_WeakWaterSpell()
        {
            //Arrange
            var card2Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Water};
            var card1Entity = new CardEntity
                {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard, ElementType = ElementType.Normal};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 20);
        }
        
        [Test]
        public void FireOrc_Attack_WaterSpell()
        {
            //Arrange
            var card2Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Water};
            var card1Entity = new CardEntity
                {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard, ElementType = ElementType.Fire};
            //Act
            var result = GameModell.CalculateDamge(card1Entity, card2Entity);
            //Assert
            Assert.That(result >= 5);
        }
        
        [Test]
        public void NormalOrc_Attack_FireSpell()
        {
            //Arrange
            var card1Entity = new CardEntity
                {Damage = 10, CardType = CardType.SpellCard, ElementType = ElementType.Fire};
            var card2Entity = new CardEntity
                {Damage = 10, Race = Race.Orc, CardType = CardType.MonsterCard, ElementType = ElementType.Normal};
            //Act
            var result = GameModell.CalculateDamge(card2Entity,card1Entity);
            //Assert
            Assert.That(result >= 5);
        }
        
        
    }
}