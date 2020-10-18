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
            Damage = 50;
        }
    }
}
