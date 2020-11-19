using System.Collections.Generic;

namespace MTCG.Interface
{
    public interface IStack
    {
        public bool Add(ICard card);

        public bool Add(List<ICard> cards);

        public List<ICard> GetStack();
    }
}