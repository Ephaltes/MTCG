using System;

namespace MTCG.Model.BaseClass
{
    public class BaseFireElveModell:MonsterCardModell
    {
        public BaseFireElveModell()
        {
            MonsterType = MonsterType.FireElve;
        }

        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new NotImplementedException();
        }
    }
}
