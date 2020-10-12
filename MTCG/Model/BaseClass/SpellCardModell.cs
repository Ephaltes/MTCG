namespace MTCG.Model.BaseClass
{
    public abstract class SpellCardModell : CardModell
    {
        public CardType WeakAgainst { get; set; } = CardType.Fire;
        
        public abstract override double CalculateDamge(CardModell enemyCard);
    }
}
