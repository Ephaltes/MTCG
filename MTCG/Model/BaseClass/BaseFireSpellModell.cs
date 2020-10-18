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
        
    }
}
