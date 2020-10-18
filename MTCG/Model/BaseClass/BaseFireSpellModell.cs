using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.Model.BaseClass
{
    public class BaseFireSpellModell:SpellCardModell
    {
        public BaseFireSpellModell()
        {
            ElementType = CardType.Fire;
            Damage = 50;
        }

        public new  double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();
            
            double basedmg =  base.CalculateDamge(enemyCard);

            if (basedmg <= 0)
                return basedmg;

            
            if ( ElementType == CardType.Fire &&  enemyCard.ElementType == CardType.Normal)
            {
                return Damage*Constant.SPELLMULTIPLIER*rand.NextDouble();
            }

            return basedmg;
        }
    }
}
