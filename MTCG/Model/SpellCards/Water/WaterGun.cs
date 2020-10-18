using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.SpellCards.Water
{
    public class WaterGun:SpellCardModell
    {
        public WaterGun()
        {
            Description = "Story Water Gun";
            Name = "Water Gun";
            ElementType = ElementType.Water;
            Damage = 50;
            CardId = CardId.WaterGun;
        }
    }
}