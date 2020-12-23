using System;
using MTCG.Model.BaseClass;

namespace MTCG
{
    public enum CardPlace
    {
        Stack=1,
        Deck=2,
        Transaction=3,
    }
    public class CardEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }
        public string Description { get; set; }
        public ElementType ElementType { get; set; } = ElementType.Normal;
        public CardType CardType { get; set; } = CardType.MonsterCard;
        public Race Race { get; set; } = Race.Unknow;
    }
}