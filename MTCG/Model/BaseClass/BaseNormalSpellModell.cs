using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.Model.BaseClass
{
    public class BaseNormalSpellModell:SpellCardModell
    {
        public BaseNormalSpellModell()
        {
            ElementType = CardType.Normal;
            WeakAgainst = CardType.Fire;
        }
        public override double CalculateDamge()
        {
            throw new NotImplementedException();
        }
    }
}
