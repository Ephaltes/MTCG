using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseKnightModell : MonsterCardModell
    {
        public BaseKnightModell()
        {
            MonsterType = MonsterType.Knight;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
