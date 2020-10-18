using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseKnightModell : MonsterCardModell
    {
        public BaseKnightModell()
        {
            Health = 150;
            Damage = 5;
            AttackSpeed = 2;
        }
    }
}
