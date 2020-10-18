using System;

namespace MTCG.Model.BaseClass
{
    public abstract class SpellCardModell : CardModell
    {
        public SpellCardModell()
        {
            CardType = CardType.SpellCard;
            ElementType = ElementType.Normal;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            
            if (enemyCard.CardType == CardType.MonsterCard && ((MonsterCardModell) enemyCard).Race == Race.Kraken)
            {
                return 0;
            }
            
            if (enemyCard.CardType == CardType.MonsterCard && ((MonsterCardModell) enemyCard).Race == Race.Knight)
            {
                return 9999;
            }

            if (EnemyIsWeakAgainstThisElement(enemyCard))
            {
                return Damage * DnDDiceRoll() * Constant.WEAKMULTIPLIER;
            }

            return Damage * DnDDiceRoll();
        }
    }
}
