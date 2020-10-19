using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Knight
{
    public class GalaxyKnight:MonsterCardModell
    {
        public GalaxyKnight()
        {
            Description = "Story Galaxy Knight";
            Name = "Galaxy Knight";
            ElementType = ElementType.Normal;
            Health = 150;
            Damage = 5;
            AttackSpeed = 2;
            Race = Race.Knight;
            CardId = CardId.GalaxyKnight;
        }
        
      
    }
}