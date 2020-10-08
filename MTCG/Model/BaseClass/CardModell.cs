namespace MTCG.Model.BaseClass
{
    public enum CardType
    {
        Fire,
        Water,
        Normal
    }

    public abstract class CardModell
    {
        public string Name { get; set; }
        public double Damage { get; set; }
        public string Description { get; set; }

        public CardType ElementType { get; set; } = CardType.Normal;

        public abstract double CalculateDamge();
    }
}
