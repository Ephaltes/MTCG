using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseNormalSpellModell:SpellCardModell
    {
        public BaseNormalSpellModell()
        {
            ElementType = CardType.Normal;
            Damage = 30;
        }
        
        public new  double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();
            
            double basedmg =  base.CalculateDamge(enemyCard);

            if (basedmg <= 0)
                return basedmg;

            if ( ElementType == CardType.Normal && enemyCard.ElementType == CardType.Water)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }

            return basedmg;
        }
        
    }
}
