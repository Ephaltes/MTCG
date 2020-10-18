using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.SpellCards.Normal
{
    public class Light:SpellCardModell
    {
        public Light()
        {
            Description = "Story Light";
            Name = "Light";
            ElementType = ElementType.Normal;
            Damage = 30;
            CardId = CardId.Light;
        }
    }
}