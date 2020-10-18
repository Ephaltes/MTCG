using MTCG.Model;
using MTCG.Model.BaseClass;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]

    public class PackageClassTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Open_Package_IsMAXCARDSPERPACKAGE()
        {
            //Arrange
            var package = new PackageModell();
            //Act
            var list = package.Open();
            //Assert
            Assert.That(list.Count == Constant.MAXCARDSPERPACKAGE);
        }
        [Test]
        public void Open_Package_No_Package_Left()
        {
            //Arrange
            var package = new PackageModell();
            //Act
            package.Open();
            var list = package.Open();
            //Assert
            Assert.That(list.Count == 0);
        }
    }
}