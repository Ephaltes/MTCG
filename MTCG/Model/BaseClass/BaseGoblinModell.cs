using System;

namespace MTCG.Model.BaseClass
{
    public class BaseGoblinModell : MonsterCardModell
    {
        public BaseGoblinModell()
        {
            MonsterType = MonsterType.Goblin;
        }
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new NotImplementedException();
        }
    }
}
