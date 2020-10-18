using MTCG.Model.BaseClass;
using MTCG.Model.MonsterTypes.Dragon;
using MTCG.Model.MonsterTypes.FireElf;
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

    public class BaseClassTests
    {
        [SetUp]
        public void Setup()
        {
        }
#region no logic
        [Test]
        public void BaseDragon_MonsterType_IsDragon()
        {
            //Arrange
            MonsterCardModell monster = new RedDragon();
            //Act
            bool result = monster.Race == Race.Dragon;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseGoblin_MonsterType_IsGoblin()
        {
            //Arrange
            MonsterCardModell monster = new GoblinLackey();
            //Act
            bool result = monster.Race == Race.Goblin;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseWizard_MonsterType_IsWizard()
        {
            //Arrange
            MonsterCardModell monster = new FireWizard();
            //Act
            bool result = monster.Race == Race.Wizard;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseKnight_MonsterType_IsKnight()
        {
            //Arrange
            MonsterCardModell monster = new GalaxyKnight();
            //Act
            bool result = monster.Race == Race.Knight;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseFireElv_MonsterType_IsFireElve()
        {
            //Arrange
            MonsterCardModell monster = new WingEggElf();
            //Act
            bool result = monster.Race == Race.FireElf;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseKraken_MonsterType_IsKraken()
        {
            //Arrange
            MonsterCardModell monster = new DemonKraken();
            //Act
            bool result = monster.Race == Race.Kraken;
            //Assert
            Assert.That(result, Is.True);
        }
        [Test]
        public void BaseOrc_MonsterType_IsOrc()
        {
            //Arrange
            MonsterCardModell monster = new OrcWarrior();
            //Act
            bool result = monster.Race == Race.Orc;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseFireSpell_ElementType_IsFire()
        {
            //Arrange
            SpellCardModell spell = new Fireball();
            //Act
            bool result = spell.ElementType == ElementType.Fire;
            //Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void BaseFireSpell_ElementType_IsWater()
        {
            //Arrange
            SpellCardModell spell = new WaterGun();
            //Act
            bool result = spell.ElementType == ElementType.Water;
            //Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void BaseFireSpell_ElementType_IsNormal()
        {
            //Arrange
            SpellCardModell spell = new Light();
            //Act
            bool result = spell.ElementType == ElementType.Normal;
            //Assert
            Assert.That(result, Is.True);
        }
        
#endregion
    }
}