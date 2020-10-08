using System;

namespace MTCG.Model.BaseClass
{
    public class BaseDragonModell : MonsterCardModell
    {

        public BaseDragonModell()
        {
            MonsterType = MonsterType.Dragon;
        }

        public override double CalculateDamge()
        {
            throw new NotImplementedException();
        }
    }
}
