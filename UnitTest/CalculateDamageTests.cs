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
using MTCG.Model.SpellCards.Fire;
using MTCG.Model.SpellCards.Normal;
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
        
        
        #region Dragon 
        [Test]
        public void Dragon_Attack_Dragon()
        {
            //Arrange
            var dragon = new RedDragon();
            var dragon2 = new RedDragon();
            //Act
            var result = dragon.CalculateDamge(dragon2);
            //Assert
            Assert.That(result > 0);
        }
        
        public void Dragon_Attack_FireElf()
        {
            //Arrange
            var dragon = new RedDragon();
            var dragon2 = new WingEggElf();
            //Act
            var result = dragon.CalculateDamge(dragon2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Dragon_Attack_Goblin()
        {
            //Arrange
            var dragon = new RedDragon();
            var goblin = new GoblinLackey();
            //Act
            var result = dragon.CalculateDamge(goblin);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void Dragon_Attack_Knight()
        {
            //Arrange
            var dragon = new RedDragon();
            var knight = new GalaxyKnight();
            //Act
            var result = dragon.CalculateDamge(knight);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Dragon_Attack_Kraken()
        {
            //Arrange
            var dragon = new RedDragon();
            var kraken = new DemonKraken();
            //Act
            var result = dragon.CalculateDamge(kraken);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Dragon_Attack_Orc()
        {
            //Arrange
            var dragon = new RedDragon();
            var orc = new OrcWarrior();
            //Act
            var result = dragon.CalculateDamge(orc);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Dragon_Attack_Wizard()
        {
            //Arrange
            var dragon = new RedDragon();
            var wizard = new FireWizard();
            //Act
            var result = dragon.CalculateDamge(wizard);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Dragon_Attack_FireSpell()
        {
            //Arrange
            var dragon = new RedDragon();
            var fireball = new Fireball();
            //Act
            var result = dragon.CalculateDamge(fireball);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Dragon_Attack_NormalSpell()
        {
            //Arrange
            var dragon = new RedDragon();
            var light = new Light();
            //Act
            var result = dragon.CalculateDamge(light);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Dragon_Attack_WaterSpell()
        {
            //Arrange
            var dragon = new RedDragon();
            var watergun = new WaterGun();
            //Act
            var result = dragon.CalculateDamge(watergun);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region Elf 
        [Test]
        public void Elf_Attack_Dragon()
        {
            //Arrange
            var dragon = new WingEggElf();
            var dragon2 = new RedDragon();
            //Act
            var result = dragon.CalculateDamge(dragon2);
            //Assert
            Assert.That(result > 0);
        }
        
        public void FireElf_Attack_FireElf()
        {
            //Arrange
            var dragon = new WingEggElf();
            var dragon2 = new WingEggElf();
            //Act
            var result = dragon.CalculateDamge(dragon2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void FireElf_Attack_Goblin()
        {
            //Arrange
            var dragon = new WingEggElf();
            var goblin = new GoblinLackey();
            //Act
            var result = dragon.CalculateDamge(goblin);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void FireElf_Attack_Knight()
        {
            //Arrange
            var dragon = new WingEggElf();
            var knight = new GalaxyKnight();
            //Act
            var result = dragon.CalculateDamge(knight);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void FireElf_Attack_Kraken()
        {
            //Arrange
            var dragon = new WingEggElf();
            var kraken = new DemonKraken();
            //Act
            var result = dragon.CalculateDamge(kraken);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void FireElf_Attack_Orc()
        {
            //Arrange
            var dragon = new WingEggElf();
            var orc = new OrcWarrior();
            //Act
            var result = dragon.CalculateDamge(orc);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void FireElf_Attack_Wizard()
        {
            //Arrange
            var dragon = new WingEggElf();
            var wizard = new FireWizard();
            //Act
            var result = dragon.CalculateDamge(wizard);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void FireElfAttack_FireSpell()
        {
            //Arrange
            var dragon = new WingEggElf();
            var fireball = new Fireball();
            //Act
            var result = dragon.CalculateDamge(fireball);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void FireElfAttack_NormalSpell()
        {
            //Arrange
            var dragon = new WingEggElf();
            var light = new Light();
            //Act
            var result = dragon.CalculateDamge(light);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void FireElfAttack_WaterSpell()
        {
            //Arrange
            var dragon = new WingEggElf();
            var watergun = new WaterGun();
            //Act
            var result = dragon.CalculateDamge(watergun);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region Goblin
        [Test]
        public void Goblin_Attack_Dragon()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Goblin_Attack_FireElf()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Goblin_Attack_Goblin()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void Goblin_Attack_Knight()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Goblin_Attack_Kraken()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Goblin_Attack_Orc()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Goblin_Attack_Wizard()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Goblin_Attack_FireSpell()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Goblin_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Goblin_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new GoblinLackey();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region Knight
        [Test]
        public void Knight_Attack_Dragon()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Knight_Attack_FireElf()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Knight_Attack_Goblin()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void Knight_Attack_Knight()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Knight_Attack_Kraken()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Knight_Attack_Orc()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Knight_Attack_Wizard()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Knight_Attack_FireSpell()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Knight_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Knight_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new GalaxyKnight();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region Kraken
        [Test]
        public void Kraken_Attack_Dragon()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Kraken_Attack_FireElf()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Kraken_Attack_Goblin()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void Kraken_Attack_Knight()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Kraken_Attack_Kraken()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Kraken_Attack_Orc()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Kraken_Attack_Wizard()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Kraken_Attack_FireSpell()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Kraken_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Kraken_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new DemonKraken();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region Orc
        [Test]
        public void Orc_Attack_Dragon()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Orc_Attack_FireElf()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Orc_Attack_Goblin()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void Orc_Attack_Knight()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Orc_Attack_Kraken()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Orc_Attack_Orc()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Orc_Attack_Wizard()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Orc_Attack_FireSpell()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Orc_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Orc_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new OrcWarrior();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region Wizard
        [Test]
        public void Wizard_Attack_Dragon()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Wizard_Attack_FireElf()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void Wizard_Attack_Goblin()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void Wizard_Attack_Knight()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Wizard_Attack_Kraken()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Wizard_Attack_Orc()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Wizard_Attack_Wizard()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void Wizard_Attack_FireSpell()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Wizard_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void Wizard_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new FireWizard();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region FireSpell
        [Test]
        public void FireSpell_Attack_Dragon()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void FireSpell_Attack_FireElf()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void FireSpell_Attack_Goblin()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void FireSpell_Attack_Knight()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void FireSpell_Attack_Kraken()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void FireSpell_Attack_Orc()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void FireSpell_Attack_Wizard()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void FireSpell_Attack_FireSpell()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void FireSpell_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void FireSpell_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new Fireball();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region NormalSpell
        [Test]
        public void NormalSpell_Attack_Dragon()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void NormalSpell_Attack_FireElf()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void NormalSpell_Attack_Goblin()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void NormalSpell_Attack_Knight()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void NormalSpell_Attack_Kraken()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void NormalSpell_Attack_Orc()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void NormalSpell_Attack_Wizard()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void NormalSpell_Attack_FireSpell()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void NormalSpell_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void NormalSpell_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new Light();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
        #region WaterSpell
        [Test]
        public void WaterSpell_Attack_Dragon()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new RedDragon();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void WaterSpell_Attack_FireElf()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new WingEggElf();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        
        [Test]
        public void WaterSpell_Attack_Goblin()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new GoblinLackey();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        
        [Test]
        public void WaterSpell_Attack_Knight()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new GalaxyKnight();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result >= 9999);
        }
        [Test]
        public void WaterSpell_Attack_Kraken()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new DemonKraken();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void WaterSpell_Attack_Orc()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new OrcWarrior();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void WaterSpell_Attack_Wizard()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new FireWizard();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result > 0);
        }
        [Test]
        public void WaterSpell_Attack_FireSpell()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new Fireball();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void WaterSpell_Attack_NormalSpell()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new Light();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <= 0);
        }
        [Test]
        public void WaterSpell_Attack_WaterSpell()
        {
            //Arrange
            var card1 = new WaterGun();
            var card2 = new WaterGun();
            //Act
            var result = card1.CalculateDamge(card2);
            //Assert
            Assert.That(result <=0);
        }
        #endregion
        
    }
}