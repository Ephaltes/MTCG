using System.Collections.Generic;

namespace MTCG.Interface
{
    public interface IDeck
    {
        public List<ICard> DeckList { get; set; }
    }
}