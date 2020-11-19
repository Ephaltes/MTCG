namespace MTCG.Interface
{
    public interface ICard
    {
        public CardEntity Entity { get; set; }

        public double CalculateDamge(ICard enemyCard);

        public bool EnemyIsWeakAgainstThisElement(ICard enemyCard);

        public void GenerateRandomId();
    }
}