using System;
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
            Random rand = new Random();

            if ( enemyCard.GetType().IsSubclassOf(typeof(MonsterCardModell))  
                 && enemyCard.ElementType == CardType.Fire)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }

            if (enemyCard.GetType().IsSubclassOf(typeof(SpellCardModell)) &&
                ((SpellCardModell) enemyCard).WeakAgainst == ElementType)
            {
                return 9999;
            }
            
            return Damage * rand.NextDouble();
            
        }
    }
}