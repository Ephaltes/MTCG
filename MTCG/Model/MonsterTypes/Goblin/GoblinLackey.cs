using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Goblin
{
    public class GoblinLackey:MonsterCardModell
    {
        public GoblinLackey()
        {
            Description = "Story Goblin Lackey";
            Name = "Goblin Lackey";
            ElementType = ElementType.Normal;
            Health = 100;
            Damage = 5;
            AttackSpeed = 5;
            Race = Race.Goblin;
            CardId = CardId.GoblinLackey;

        }
        
      
    }
}