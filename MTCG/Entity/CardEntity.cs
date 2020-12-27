namespace MTCG
{
    public enum CardPlace
    {
        Stack = 1,
        Deck = 2,
        Transaction = 3,
        Pack = 4
    }

    public enum ElementType
    {
        Normal = 0,
        Fire = 1,
        Water = 2
    }

    public enum CardType
    {
        Unknown,
        MonsterCard = 1,
        SpellCard = 2
    }

    public enum Race
    {
        Unknow = 0,
        Dragon = 1,
        FireElf = 2,
        Goblin = 3,
        Knight = 4,
        Kraken = 5,
        Orc = 6,
        Wizard = 7
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
        public CardPlace CardPlace { get; set; } = CardPlace.Stack;
    }
}