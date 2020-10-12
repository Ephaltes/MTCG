using System;

namespace MTCG.Model.BaseClass
{
    public class BaseOrcModell : MonsterCardModell
    {
        public BaseOrcModell()
        {
            MonsterType = MonsterType.Orc;
        }
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new NotImplementedException();
        }
    }
}
