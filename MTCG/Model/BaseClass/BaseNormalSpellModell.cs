﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseNormalSpellModell:SpellCardModell
    {
        public BaseNormalSpellModell()
        {
            ElementType = CardType.Normal;
            WeakAgainst = CardType.Fire;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
