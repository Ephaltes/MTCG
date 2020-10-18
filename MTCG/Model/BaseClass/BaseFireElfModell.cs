using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseFireElfModell:MonsterCardModell
    {
        public BaseFireElfModell()
        {
            Health = 125;
            Damage = 5;
            AttackSpeed = 3;
        }
        
    }
}
