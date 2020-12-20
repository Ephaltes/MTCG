namespace MTCG.Interface
{
    public interface ICard
    {
        public CardEntity Entity { get; set; }

        public void GenerateRandomId();
    }
}