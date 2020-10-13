using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Orc
{
    public class OrcWarrior:BaseOrcModell
    {
        public OrcWarrior()
        {
            Description = "Story Orc Warrior";
            Name = "Orc Warrior";
            ElementType = CardType.Normal;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}