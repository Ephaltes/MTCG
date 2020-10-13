using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Goblin
{
    public class GoblinLackey:BaseGoblinModell
    {
        public GoblinLackey()
        {
            Description = "Story Goblin Lackey";
            Name = "Goblin Lackey";
            ElementType = CardType.Normal;
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}