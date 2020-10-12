namespace MTCG.Model.BaseClass
{
    public enum MonsterType
    {
        None,
        Goblin,
        Dragon,
        Wizard,
        Knight,
        Kraken,
        FireElve,
        Orc
    }

    public abstract class MonsterCardModell : CardModell
    {
        public double Health=100;
        public MonsterType MonsterType { get; set; } = MonsterType.None;

        public abstract override double CalculateDamge(CardModell enemyCard);

    }
}
