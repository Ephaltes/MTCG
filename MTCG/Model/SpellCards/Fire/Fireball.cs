using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.SpellCards.Fire
{
    public class Fireball:BaseFireSpellModell
    {
        public Fireball()
        {
            Description = "Story Fireball";
            Name = "Fireball";
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();

            if ( enemyCard.GetType().IsSubclassOf(typeof(MonsterCardModell))  && enemyCard.ElementType == CardType.Normal)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }

            if (enemyCard.GetType().IsSubclassOf(typeof(SpellCardModell)) &&
                ((SpellCardModell) enemyCard).WeakAgainst == ElementType)
            {
                return 9999;
            }

            if (enemyCard.GetType().IsSubclassOf(typeof(BaseKrakenModell)))
            {
                return 0;
            }
            
            return Damage * rand.NextDouble();
        }
    }
}