using MTCG.Model.BaseClass;
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

        [Test]
        public void BaseDragon_MonsterType_IsDragon()
        {
            //Arrange
            MonsterCardModell monster = new BaseDragonModell();
            //Act
            bool result = monster.MonsterType == MonsterType.Dragon;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseGoblin_MonsterType_IsGoblin()
        {
            //Arrange
            MonsterCardModell monster = new BaseGoblinModell();
            //Act
            bool result = monster.MonsterType == MonsterType.Goblin;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseWizard_MonsterType_IsWizard()
        {
            //Arrange
            MonsterCardModell monster = new BaseWizardModell();
            //Act
            bool result = monster.MonsterType == MonsterType.Wizard;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseKnight_MonsterType_IsKnight()
        {
            //Arrange
            MonsterCardModell monster = new BaseKnightModell();
            //Act
            bool result = monster.MonsterType == MonsterType.Knight;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseFireElv_MonsterType_IsFireElve()
        {
            //Arrange
            MonsterCardModell monster = new BaseFireElveModell();
            //Act
            bool result = monster.MonsterType == MonsterType.FireElve;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseKraken_MonsterType_IsKraken()
        {
            //Arrange
            MonsterCardModell monster = new BaseKrakenModell();
            //Act
            bool result = monster.MonsterType == MonsterType.Kraken;
            //Assert
            Assert.That(result, Is.True);
        }
        [Test]
        public void BaseOrc_MonsterType_IsOrc()
        {
            //Arrange
            MonsterCardModell monster = new BaseOrcModell();
            //Act
            bool result = monster.MonsterType == MonsterType.Orc;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseFireSpell_ElementType_IsFire()
        {
            //Arrange
            SpellCardModell spell = new BaseFireSpellModell();
            //Act
            bool result = spell.ElementType == CardType.Fire;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseFireSpell_WeakAgainst_Water()
        {
            //Arrange
            SpellCardModell spell = new BaseFireSpellModell();
            //Act
            bool result = spell.WeakAgainst == CardType.Water;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseWaterSpell_ElementType_IsWater()
        {
            //Arrange
            SpellCardModell spell = new BaseWaterSpellModell();
            //Act
            bool result = spell.ElementType == CardType.Water;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseWaterSpell_WeakAgainst_Normal()
        {
            //Arrange
            SpellCardModell spell = new BaseWaterSpellModell();
            //Act
            bool result = spell.WeakAgainst == CardType.Normal;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseNormalSpell_ElementType_IsNormal()
        {
            //Arrange
            SpellCardModell spell = new BaseNormalSpellModell();
            //Act
            bool result = spell.ElementType == CardType.Normal;
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void BaseNormalSpell_WeakAgainst_Fire()
        {
            //Arrange
            SpellCardModell spell = new BaseNormalSpellModell();
            //Act
            bool result = spell.WeakAgainst == CardType.Fire;
            //Assert
            Assert.That(result, Is.True);
        }

    }
}