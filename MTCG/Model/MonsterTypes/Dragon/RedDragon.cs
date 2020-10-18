using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Dragon
{
    public class RedDragon:MonsterCardModell
    {
        public RedDragon()
        {
            Description = "Story Red Dragon";
            Name = "Red Dragon";
            ElementType = BaseClass.ElementType.Fire;
            Race = Race.Dragon;
            Health = 200;
            Damage = 20;
            AttackSpeed = 1;
        }
    }
}