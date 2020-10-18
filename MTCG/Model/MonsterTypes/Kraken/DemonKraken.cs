using System;
using MTCG.Model.BaseClass;

namespace MTCG.Model.MonsterTypes.Kraken
{
    public class DemonKraken:BaseKrakenModell
    {
        public DemonKraken()
        {
            Description = "Story Demon Kraken";
            Name = "Demon Kraken";
            ElementType = CardType.Water;
        }
    }
}