using MTCG.Model.BaseClass;

namespace MTCG.Model.SpellCards.Normal
{
    public class Light:BaseNormalSpellModell
    {
        public Light()
        {
            Description = "Story Light";
            Name = "Light";
            ElementType = CardType.Normal;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}