using System;
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
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();

            if (enemyCard.GetType().IsSubclassOf(typeof(BaseKrakenModell)))
            {
                return 0;
            }
            
            if ( enemyCard.GetType().IsSubclassOf(typeof(MonsterCardModell))  && enemyCard.ElementType == CardType.Water)
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