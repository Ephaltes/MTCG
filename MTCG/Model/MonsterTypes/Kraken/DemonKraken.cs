using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Kraken
{
    public class DemonKraken:MonsterCardModell
    {
        public DemonKraken()
        {
            Description = "Story Demon Kraken";
            Name = "Demon Kraken";
            ElementType = BaseClass.ElementType.Water;
            Health = 175;
            Damage = 7.5;
            AttackSpeed = 1.2;
            Race = Race.Kraken;
            CardId = CardId.DemonKraken;
        }
    }
}