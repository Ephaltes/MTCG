using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.Model.BaseClass
{
    public class BaseWaterSpellModell:SpellCardModell
    {
        public BaseWaterSpellModell()
        {
            ElementType = CardType.Water;
            WeakAgainst = CardType.Normal;
        }
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new NotImplementedException();
        }
    }
}
