using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public enum GameEnd
    {
        Draw,
        Player1,
        Player2
    }
    public class GameModell
    {
        public static ReportEntity Fight(List<CardEntity> player1Deck, List<CardEntity> player2Deck)
        {
            string log="Fight started:\n\n";
            int i = 0;
            while (player1Deck.Count > 0 && player2Deck.Count > 0 && i < Constant.MAXROUND)
            {
                
                //Get Random Card in Deck
                var player1Card = player1Deck.OrderBy(x => Guid.NewGuid()).ToList()[0];
                var player2Card = player2Deck.OrderBy(x => Guid.NewGuid()).ToList()[0];

                var player1dmg = CalculateDamge(player1Card,player2Card);
                var player2dmg = CalculateDamge(player2Card,player1Card);

                log += $"Round {i + 1}: \n";
                log += $"Player1 dealing: {player1dmg}\n";
                log += $"Player2 dealing: {player2dmg}\n";

                if (player1dmg > player2dmg)
                {
                    player1Deck.Add(player2Card);
                    player2Deck.Remove(player2Card);
                    log += $"Player1 won the round dealing {player1dmg} Damage\n";
                }
                else if (player2dmg > player1dmg)
                {
                    player2Deck.Add(player1Card);
                    player1Deck.Remove(player1Card);
                    log += $"Player2 won the round dealing {player2dmg} Damage\n";
                }

                log += "\n";
                i++;
            }

            if (player1Deck.Count == 0)
            {
                log += "END: Winner Player2";
                return new ReportEntity(){GameEnd = GameEnd.Player2,Log = log};
            }
            if (player2Deck.Count == 0)
            {
                log += "END: Winner Player1";
                return new ReportEntity(){GameEnd = GameEnd.Player1,Log = log};
            }
            
            log += "END: Draw";
            return new ReportEntity(){GameEnd = GameEnd.Draw,Log = log};

        }
        
        public static double CalculateDamge(CardEntity attackingCard,CardEntity defendingCard)
        {
            if (defendingCard.CardType == CardType.MonsterCard && attackingCard.CardType == CardType.MonsterCard)
            {
                if (attackingCard.Race == Race.Goblin && defendingCard.Race == Race.Dragon)
                {
                    return 0;
                }
                
                if (attackingCard.Race == Race.Orc && defendingCard.Race == Race.Wizard)
                {
                    return 0;
                }
                
                if (attackingCard.Race == Race.Dragon && defendingCard.Race == Race.FireElf)
                {
                    return 0;
                }
            }

            if (attackingCard.CardType == CardType.SpellCard && defendingCard.CardType == CardType.MonsterCard)
            {
                if (defendingCard.Race == Race.Kraken)
                {
                    return 0;
                }
            
                if (defendingCard.Race == Race.Knight)
                {
                    return 9999;
                }
            }
            
            if (attackingCard.CardType == CardType.MonsterCard && defendingCard.CardType == CardType.SpellCard 
                || attackingCard.CardType == CardType.SpellCard && defendingCard.CardType == CardType.MonsterCard)
            {
                var weak = WeakAgainstCardElement(attackingCard, defendingCard);

                switch (weak)
                {
                    case WeakAgainst.AttackingCard:
                        return attackingCard.Damage * 0.5;
                    case WeakAgainst.DefendingCard:
                        return attackingCard.Damage * 2.0;
                }
                
            }
            return attackingCard.Damage;
        }

        public enum WeakAgainst
        {
            Normal,
            AttackingCard,
            DefendingCard
        }
        public static WeakAgainst WeakAgainstCardElement(CardEntity attackingCard, CardEntity defendingCard)
        {
            if (attackingCard.ElementType == ElementType.Fire && defendingCard.ElementType == ElementType.Normal)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.ElementType == ElementType.Water && defendingCard.ElementType == ElementType.Fire)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.ElementType == ElementType.Normal && defendingCard.ElementType == ElementType.Water)
                return WeakAgainst.DefendingCard;
            
            if (attackingCard.ElementType == ElementType.Fire && defendingCard.ElementType == ElementType.Water)
                return WeakAgainst.AttackingCard;
            
            if (attackingCard.ElementType == ElementType.Water && defendingCard.ElementType == ElementType.Normal)
                return WeakAgainst.AttackingCard;
            
            if (attackingCard.ElementType == ElementType.Normal && defendingCard.ElementType == ElementType.Fire)
                return WeakAgainst.AttackingCard;
            
            return WeakAgainst.Normal;
        }

    }
}