using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.SpellCards.Fire
{
    public class Fireball:SpellCardModell
    {
        public Fireball()
        {
            Description = "Story Fireball";
            Name = "Fireball";
            ElementType = ElementType.Fire;
            Damage = 50;
            CardId = CardId.Fireball;
        }
    }
}