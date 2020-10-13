using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseDragonModell : MonsterCardModell
    {

        public BaseDragonModell()
        {
            MonsterType = MonsterType.Dragon;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
