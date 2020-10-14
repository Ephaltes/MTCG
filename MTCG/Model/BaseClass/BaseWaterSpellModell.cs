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
            WeakAgainst = CardType.Normal;
            Damage = 20;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
