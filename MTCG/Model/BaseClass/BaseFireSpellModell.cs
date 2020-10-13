using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseFireSpellModell:SpellCardModell
    {
        public BaseFireSpellModell()
        {
            ElementType = CardType.Fire;
            WeakAgainst = CardType.Water;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
