using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Dragon
{
    public class RedDragon:BaseDragonModell
    {
        public RedDragon()
        {
            Description = "Story Red Dragon";
            Name = "Red Dragon";
            ElementType = CardType.Fire;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}