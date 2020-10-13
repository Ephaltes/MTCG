using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Knight
{
    public class GalaxyKnight:BaseKnightModell
    {
        public GalaxyKnight()
        {
            Description = "Story Galaxy Knight";
            Name = "Galaxy Knight";
            ElementType = CardType.Normal;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}