using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Orc
{
    public class OrcWarrior:MonsterCardModell
    {
        public OrcWarrior()
        {
            Description = "Story Orc Warrior";
            Name = "Orc Warrior";
            ElementType = BaseClass.ElementType.Normal;
            Health = 120;
            Damage = 2.5;
            AttackSpeed = 5;
            Race = Race.Orc;
            CardId = CardId.OrcWarrior;
        }
    }
}