using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using MTCG.Entity;

namespace MTCG.Model
{
    public enum GameEnd
    {
        Draw,
        Player1,
        Player2,
    }
    
    public class GameModell
    {
        public enum WeakAgainst
        {
            Normal,
            AttackingCard,
            DefendingCard
        }

        private static readonly object _lockobject = new object();

        protected UserModell _player;

        public GameModell(UserModell player)
        {
            PlayerList.Add(player);
            _player = player;
        }

        protected static ConcurrentBag<UserModell> PlayerList { get; set; } =
            new ConcurrentBag<UserModell>();

        protected static ConcurrentDictionary<string, ReportEntity> LogList { get; set; } =
            new ConcurrentDictionary<string, ReportEntity>();

        public ReportEntity GetLog()
        {
            ReportEntity report = null;
            while (!LogList.TryRemove(_player.UserEntity.Username, out report))
            {
                Thread.Sleep(1000);

                TryFight();
            }

            return report;
        }

        protected void TryFight()
        {
            UserModell player1 = null, player2 = null;
            lock (_lockobject)
            {
                if (PlayerList.Count < 2) return;

                if (PlayerList.TryTake(out player1))
                {
                    if (!PlayerList.TryTake(out player2))
                    {
                        //Sollte eig. nicht passieren
                        PlayerList.Add(player1);
                        return;
                    }
                }
            }

            Fight(player1, player2);
        }

        protected void Fight(UserModell player1, UserModell player2)
        {
            var player1Deck = player1.Deck;
            var player2Deck = player2.Deck;


            var log = "Fight started:\n\n";
            var i = 0;
            bool suddendeath = false;
            Random rand = new Random();
            while (player1Deck?.Count > 0 && player2Deck?.Count > 0 && i < Constant.MAXROUND)
            {
                //Get Random Card in Deck
                var player1Card = player1Deck.OrderBy(x => Guid.NewGuid()).ToList()[0];
                var player2Card = player2Deck.OrderBy(x => Guid.NewGuid()).ToList()[0];

                double[] playerdmg = new double[2];
                playerdmg[0] = CalculateDamge(player1Card, player2Card);
                playerdmg[1] = CalculateDamge(player2Card, player1Card);

                if (i + 1 == Constant.SUDDENDEATH)
                {
                    suddendeath = true;
                }

                if (suddendeath)
                {
                    if ((i + 1) % 2 == 0)
                        playerdmg[rand.Next(2)] = 9999;
                }

                log += $"Round {i + 1}: \n";
                log += $"Player1 dealing: {playerdmg[0]}\n";
                log += $"Player2 dealing: {playerdmg[1]}\n";

                if (playerdmg[0] > playerdmg[1])
                {
                    player1Deck.Add(player2Card);
                    player2Deck.Remove(player2Card);
                    log += $"Player1 won the round dealing {playerdmg[0]} Damage\n";
                }
                else if (playerdmg[1] > playerdmg[0])
                {
                    player2Deck.Add(player1Card);
                    player1Deck.Remove(player1Card);
                    log += $"Player2 won the round dealing {playerdmg[1]} Damage\n";
                }

                log += "\n";
                i++;
            }

            ReportEntity report = null;
            if (player1Deck?.Count == 0)
            {
                log += "END: Winner Player2";
                report = new ReportEntity
                {
                    GameEnd = GameEnd.Player2, Log = log, Player1 = player1.UserEntity.DisplayName,
                    Player2 = player2.UserEntity.DisplayName, Winner = player2.UserEntity.DisplayName
                };

                player1.LostFightAgainst(player2.UserEntity);
                player2.WonFightAgainst(player1.UserEntity);
            }

            if (player2Deck?.Count == 0)
            {
                log += "END: Winner Player1";
                report = new ReportEntity
                {
                    GameEnd = GameEnd.Player1, Log = log, Player1 = player1.UserEntity.DisplayName,
                    Player2 = player2.UserEntity.DisplayName,
                    Winner = player1.UserEntity.DisplayName
                };

                player1.WonFightAgainst(player2.UserEntity);
                player2.LostFightAgainst(player1.UserEntity);
            }

            if (player1Deck?.Count > 0 && player2Deck?.Count > 0)
            {
                log += "END: Draw";
                report = new ReportEntity
                {
                    GameEnd = GameEnd.Draw, Log = log, Player1 = player1.UserEntity.DisplayName,
                    Player2 = player2.UserEntity.DisplayName
                };
                //No Elo change when Draw
                player1.DrawFight();
                player2.DrawFight();
            }

            LogList.TryAdd(player1.UserEntity.Username, report);
            LogList.TryAdd(player2.UserEntity.Username, report);
        }

        public static double CalculateDamge(CardEntity attackingCard, CardEntity defendingCard)
        {
            if (defendingCard.CardType == CardType.MonsterCard && attackingCard.CardType == CardType.MonsterCard)
            {
                if (attackingCard.Race == Race.Goblin && defendingCard.Race == Race.Dragon) return 0;

                if (attackingCard.Race == Race.Orc && defendingCard.Race == Race.Wizard) return 0;

                if (attackingCard.Race == Race.Dragon && defendingCard.Race == Race.FireElf) return 0;
            }

            if (attackingCard.CardType == CardType.SpellCard && defendingCard.CardType == CardType.MonsterCard)
            {
                if (defendingCard.Race == Race.Kraken) return 0;

                if (defendingCard.Race == Race.Knight) return 9999;
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