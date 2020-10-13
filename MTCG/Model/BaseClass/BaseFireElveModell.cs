using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseFireElveModell:MonsterCardModell
    {
        public BaseFireElveModell()
        {
            MonsterType = MonsterType.FireElve;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
