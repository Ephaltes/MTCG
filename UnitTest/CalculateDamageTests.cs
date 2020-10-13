using System.Collections.Generic;
using MTCG.Model;
using MTCG.Model.BaseClass;
using MTCG.Model.MonsterTypes.Dragon;
using MTCG.Model.MonsterTypes.FireElve;
using MTCG.Model.MonsterTypes.Goblin;
using MTCG.Model.MonsterTypes.Knight;
using MTCG.Model.MonsterTypes.Kraken;
using MTCG.Model.MonsterTypes.Orc;
using MTCG.Model.MonsterTypes.Wizard;
using MTCG.Model.SpellCards.Water;
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

        #region specialCase
        [Test]
        public void Goblin_Attack_Dragon()
        {
            //Arrange
            var goblin = new GoblinLackey();
            var dragon = new RedDragon();
            //Act
            var result = goblin.CalculateDamge(dragon);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Orc_Attack_Wizard()
        {
            //Arrange
            var orc = new OrcWarrior();
            var wizard = new FireWizard();
            //Act
            var result = orc.CalculateDamge(wizard);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void WaterSpell_Attack_Knight()
        {
            //Arrange
            var waterspell = new WaterGun();
            var knight = new GalaxyKnight();
            //Act
            var result = waterspell.CalculateDamge(knight);
            //Assert
            Assert.That(result >= 9999);
        }
        
        [Test]
        public void Spell_Attack_Kraken()
        {
            //Arrange
            var waterspell = new WaterGun();
            var kraken = new DemonKraken();
            //Act
            var result = waterspell.CalculateDamge(kraken);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Dragon_Attack_Elf()
        {
            //Arrange
            var elf = new WingEggElf();
            var dragon = new RedDragon();
            //Act
            var result = dragon.CalculateDamge(elf);
            //Assert
            Assert.That(result <= 0);
        }
        #endregion
        
        
    }
}