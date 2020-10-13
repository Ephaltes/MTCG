using System;

namespace MTCG.Model.BaseClass
{
    public abstract class BaseGoblinModell : MonsterCardModell
    {
        public BaseGoblinModell()
        {
            MonsterType = MonsterType.Goblin;
        }

        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
