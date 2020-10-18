using System;

namespace MTCG.Model.BaseClass
{

    public enum Race
    {
        Unknow,
        Dragon,
        FireElf,
        Goblin,
        Knight,
        Kraken,
        Orc,
        Wizard
    }
    
    public abstract class MonsterCardModell : CardModell
    {
        public double Health=100;
        public double AttackSpeed = 1;
        public Race Race = Race.Unknow;

        public MonsterCardModell()
        {
            ElementType = ElementType.Normal;
            CardType = CardType.MonsterCard;
        }
        public override double CalculateDamge(CardModell enemyCard)
        {
            Random rand = new Random();

            if (enemyCard.CardType == CardType.SpellCard && EnemyIsWeakAgainstThisElement(enemyCard))
            {
                return Damage * AttackSpeed * Constant.WEAKMULTIPLIER * DnDDiceRoll();
            }

            if (enemyCard.CardType == CardType.MonsterCard)
            {
                if (Race == Race.Goblin && ((MonsterCardModell)enemyCard).Race == Race.Dragon)
                {
                    return 0;
                }
                
                if (Race == Race.Orc && ((MonsterCardModell)enemyCard).Race == Race.Wizard)
                {
                    return 0;
                }
                
                if (Race == Race.Dragon && ((MonsterCardModell)enemyCard).Race == Race.FireElf)
                {
                    return 0;
                }
            }
            return Damage * AttackSpeed * DnDDiceRoll();
        }
    }
}
