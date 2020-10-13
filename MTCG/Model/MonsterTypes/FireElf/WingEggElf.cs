using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.FireElve
{
    public class WingEggElf:BaseFireElveModell
    {
        public WingEggElf()
        {
            Description = "Story Elf";
            Name = "Wing Egg Elf";
            ElementType = CardType.Fire;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}