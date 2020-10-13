using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseOrcModell : MonsterCardModell
    {
        public BaseOrcModell()
        {
            MonsterType = MonsterType.Orc;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
