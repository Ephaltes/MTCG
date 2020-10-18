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
            Damage = 30;
        }
        
    }
}
