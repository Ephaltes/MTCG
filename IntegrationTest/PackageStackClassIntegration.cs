using MTCG.Model;
using MTCG.Model.BaseClass;
using NUnit.Framework;

namespace IntegrationTest
{
    public class PackageStackClassIntegration
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OpenPackage_AddToStack()
        {
            var package = new PackageListModell();
            var stack =  new StackModell();

            var cardList = package.Open();
            stack.Add(cardList);
            
            Assert.That(stack.GetStack().Count == Constant.MAXCARDSPERPACKAGE);
        }
        
        [Test]
        public void OpenPackage2x_AddToStack()
        {
            var package = new PackageListModell();
            var stack =  new StackModell();

            var cardList = package.Open();
            stack.Add(cardList);
            cardList = package.Open();
            stack.Add(cardList);
            
            Assert.That(stack.GetStack().Count == Constant.MAXCARDSPERPACKAGE);
        }
    }
}