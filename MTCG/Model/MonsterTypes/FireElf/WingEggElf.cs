using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.FireElf
{
    public class WingEggElf:MonsterCardModell
    {
        public WingEggElf()
        {
            Description = "Story Elf";
            Name = "Wing Egg Elf";
            ElementType = ElementType.Normal;
            Health = 125;
            Damage = 5;
            AttackSpeed = 3;
            Race = Race.FireElf;
            CardId = CardId.WingEggElf;
        }
        
       
    }
}