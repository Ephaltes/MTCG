using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseWaterSpellModell:SpellCardModell
    {
        public BaseWaterSpellModell()
        {
            ElementType = CardType.Water;
            Damage = 50;
        }

        public new double CalculateDamge(CardModell enemyCard)
        {
            Random rand  = new Random();
            if (ElementType == CardType.Water && enemyCard.GetType().IsSubclassOf(typeof(BaseKnightModell)))
            {
                return 9999;
            }
            
            double basedmg =  base.CalculateDamge(enemyCard);

            if (basedmg <= 0)
                return basedmg;
            
            if ( ElementType == CardType.Water &&  enemyCard.ElementType == CardType.Fire)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }

            return basedmg;
        }
    }
}
