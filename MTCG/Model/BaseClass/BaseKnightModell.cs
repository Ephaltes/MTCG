using System;

namespace MTCG.Model.BaseClass
{
    public class BaseKnightModell : MonsterCardModell
    {
        public BaseKnightModell()
        {
            MonsterType = MonsterType.Knight;
        }
        public override double CalculateDamge()
        {
            throw new NotImplementedException();
        }
    }
}
