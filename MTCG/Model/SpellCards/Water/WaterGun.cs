using MTCG.Model.BaseClass;

namespace MTCG.Model.SpellCards.Water
{
    public class WaterGun:BaseWaterSpellModell
    {
        public WaterGun()
        {
            Description = "Story Water Gun";
            Name = "Water Gun";
            ElementType = CardType.Water;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}